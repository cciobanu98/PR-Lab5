using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.IO;
using System.Reflection;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading;
using WebCamera.Core;
using AForge.Video;
using AForge.Video.DirectShow;

namespace WebCamera.Server
{
    class Program
    {
        private static bool isRunning;
        private static Server server;
        private static VideoCaptureDevice videoSource;
        static void Main(string[] args)
        {
            IServiceCollection services = new ServiceCollection();
            var config = LoadConfiguration();

            services.AddLogging(configure => configure.AddConsole())
                    .AddSingleton(config)
                    .AddTransient<IFormatter, BinaryFormatter>()
                    .AddTransient<Server>();


            var provider = services.BuildServiceProvider();
            server = provider.GetRequiredService<Server>();
            var serverOptions = config.GetSection("Server").Get<UdpSocketOptions>();
            server.Initialize(serverOptions.Ip, serverOptions.Port);

            var videoDevices = new FilterInfoCollection(FilterCategory.VideoInputDevice);
            videoSource = new VideoCaptureDevice(videoDevices[0].MonikerString);
            videoSource.VideoResolution = videoSource.VideoCapabilities[3];
            videoSource.NewFrame += new NewFrameEventHandler(video_NewFrame);
            Run();
        }

        private static void video_NewFrame(object sender, NewFrameEventArgs eventArgs)
        {
            var test = eventArgs.Frame;
            byte[] bytes;
            using (var stream = new MemoryStream())
            {
                test.Save(stream, System.Drawing.Imaging.ImageFormat.Png);
                bytes =  stream.ToArray();
                server.Send(new Response { Data = bytes, ResponseType = ResponseType.IMG });
            }
        }

        public static void Run()
        {
            while (true)
            {
                var request = server.Get<Request>();
                if (request != null)
                {
                    if (request.RequestType == RequestType.START_CAPTURE_CAMERA)
                    {
                        if (!isRunning)
                        {
                            isRunning = true;
                            new Thread(new ThreadStart(CaptureImageFromCamera)).Start();
                        }
                    }
                    else if (request.RequestType == RequestType.STOP_CAPTURE_CAMERA)
                    {
                        isRunning = false;
                        videoSource.SignalToStop();
                    }
                }
            }
        }

        private static void CaptureImageFromCamera()
        {
            videoSource.Start();
        }

        private static IConfiguration LoadConfiguration()
        {
            var assembly = Assembly.GetExecutingAssembly();

            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            //.AddUserSecrets(assembly);

            return builder.Build();
        }
    }
}

using Acr.UserDialogs;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MvvmCross.IoC;
using System.IO;
using System.Reflection;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using WebCamera.Client.Core.Client;

namespace WebCamera.Client.Core
{
    public static class SetupIoC
    {
        public static IMvxIoCProvider RegisterDependencies(IMvxIoCProvider provider)
        {
            ILoggerFactory factory = LoggerFactory.Create(x => x.AddConsole());
            var logger = factory.CreateLogger<UdpClient>();

            provider.RegisterSingleton(() => UserDialogs.Instance);
            var config = LoadConfiguration();


            provider.RegisterSingleton<IConfiguration>(config);
            provider.RegisterSingleton(logger);
            provider.RegisterType<IFormatter, BinaryFormatter>();

            provider.LazyConstructAndRegisterSingleton<IUdpClient, UdpClient>();
            return provider;
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

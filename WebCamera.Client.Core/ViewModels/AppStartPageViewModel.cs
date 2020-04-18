using Microsoft.Extensions.Configuration;
using MvvmCross.Base;
using MvvmCross.Commands;
using MvvmCross.Logging;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using System;
using System.IO;
using System.Threading;
using WebCamera.Client.Core.Client;
using WebCamera.Core;
using Xamarin.Forms;

namespace WebCamera.Client.Core.ViewModels
{
    public class AppStartPageViewModel : MvxNavigationViewModel
    {
        private readonly UdpSocketOptions udpSocketOptions;
        private readonly IUdpClient _udpClient;
        private readonly IMvxMainThreadAsyncDispatcher _dispatcher;
        public AppStartPageViewModel(IMvxLogProvider provider, IMvxNavigationService navigationService, IConfiguration configuration,
            IUdpClient udpClient, IMvxMainThreadAsyncDispatcher dispatcher)
           : base(provider, navigationService)
        {
            _udpClient = udpClient;
            _dispatcher = dispatcher;
            udpSocketOptions = configuration.GetSection("Server").Get<UdpSocketOptions>();
            udpClient.Initialize(udpSocketOptions.Ip, udpSocketOptions.Port);
        }

        private ImageSource _image;
        public ImageSource Image
        {
            get => _image;
            set => SetProperty(ref _image, value);
        }

        private bool _isRunning;
        public bool IsRunning
        {
            get => _isRunning;
            set => SetProperty(ref _isRunning, value);
        }

        private IMvxCommand _startStopCommand;
        public IMvxCommand StartStopCommand => _startStopCommand ?? (_startStopCommand = new MvxCommand(StartStop));
        private void GetCameraCapture()
        {
            while (IsRunning)
            {
                var data = _udpClient.Get();
                if (data.ResponseType == ResponseType.IMG)
                {
                    var imgSource = ImageSource.FromStream(() => new MemoryStream(data.Data));
                    _dispatcher.ExecuteOnMainThreadAsync(() => Image = imgSource);
                }
            }
        }
        private void StartStop()
        {
            IsRunning = !IsRunning;
            if (IsRunning == true)
            {
                _udpClient.Send(new Request { RequestType = RequestType.START_CAPTURE_CAMERA});
                new Thread(new ThreadStart(GetCameraCapture)).Start();
            }
            else
            {
                _udpClient.Send(new Request { RequestType = RequestType.STOP_CAPTURE_CAMERA});
                Image = null;
            }
        }
    }
}

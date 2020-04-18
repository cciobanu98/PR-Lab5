using Acr.UserDialogs;
using MvvmCross.Forms.Platforms.Uap.Views;
using Windows.ApplicationModel.Activation;

namespace WebCamera.Client.UWP
{
    /// <summary>
    /// Provides application-specific behavior to supplement the default Application class.
    /// </summary>
    public abstract class UwpApp : MvxWindowsApplication<Setup, WebCamera.Client.Core.App, WebCamera.Client.App, MainPage>
    {
        protected override void OnLaunched(LaunchActivatedEventArgs e)
        {
            global::Xamarin.Forms.Forms.Init(e);
            UserDialogs.Init();
            base.OnLaunched(e);
        }
    }
}

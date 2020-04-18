using MvvmCross.Forms.Platforms.Uap.Core;
using MvvmCross.IoC;
using WebCamera.Client.Core;

namespace WebCamera.Client.UWP
{
    public class Setup : MvxFormsWindowsSetup<WebCamera.Client.Core.App, WebCamera.Client.App>
    {
        protected override IMvxIoCProvider InitializeIoC()
        {
            var provider = base.InitializeIoC();
            return SetupIoC.RegisterDependencies(provider);
        }
    }
}

using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using System.Threading.Tasks;
using WebCamera.Client.Core.ViewModels;

namespace WebCamera.Client.Core
{
    public class AppStart : MvxAppStart
    {
        public AppStart(IMvxApplication application,
                       IMvxNavigationService navigationService) : base(application, navigationService)
        {

        }
        protected override async Task NavigateToFirstViewModel(object hint = null)
        {
            await NavigationService.Navigate<AppStartPageViewModel>();
        }
    }
}

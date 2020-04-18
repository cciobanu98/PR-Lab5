using MvvmCross.Forms.Views;
using WebCamera.Client.Core.ViewModels;
using Xamarin.Forms.Xaml;

namespace WebCamera.Client.UI.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AppStartPage : MvxContentPage<AppStartPageViewModel>
    {
        public AppStartPage()
        {
            InitializeComponent();
        }
    }
}
using SistemaTaxiMobil.ViewModels;

namespace SistemaTaxiMobil.Views
{
    public partial class SplashPage : ContentPage
    {
        public SplashPage(SplashViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = viewModel;
        }
    }
}
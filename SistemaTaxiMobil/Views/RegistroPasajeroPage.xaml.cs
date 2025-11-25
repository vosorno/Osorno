using SistemaTaxiMobil.ViewModels;

namespace SistemaTaxiMobil.Views
{
    public partial class RegistroPasajeroPage : ContentPage
    {
        public RegistroPasajeroPage(RegistroPasajeroViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = viewModel;
        }
    }
}
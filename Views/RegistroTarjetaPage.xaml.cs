using SistemaTaxiMobil.ViewModels;

namespace SistemaTaxiMobil.Views
{
    public partial class RegistroTarjetaPage : ContentPage
    {
        public RegistroTarjetaPage(RegistroTarjetaViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = viewModel;
        }
    }
}
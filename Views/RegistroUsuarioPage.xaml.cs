using SistemaTaxiMobil.ViewModels;

namespace SistemaTaxiMobil.Views
{
    public partial class RegistroUsuarioPage : ContentPage
    {
        public RegistroUsuarioPage(RegistroUsuarioViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = viewModel;
        }
    }
}
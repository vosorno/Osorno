using SistemaTaxiMobil.ViewModels;

namespace SistemaTaxiMobil.Views
{
    public partial class TipoPagoPage : ContentPage
    {
        public TipoPagoPage(TipoPagoViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = viewModel;
        }
    }
}
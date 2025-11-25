using SistemaTaxiMobil.ViewModels;

namespace SistemaTaxiMobil.Views
{
    public partial class ViajeCerradoPage : ContentPage
    {
        private readonly ViajeCerradoViewModel _viewModel;

        public ViajeCerradoPage(ViajeCerradoViewModel viewModel)
        {
            InitializeComponent();
            _viewModel = viewModel;
            BindingContext = viewModel;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await _viewModel.InicializarAsync();
        }
    }
}
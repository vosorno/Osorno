using SistemaTaxiMobil.ViewModels;
using Microsoft.Maui.Controls.Maps;
using Microsoft.Maui.Maps;

namespace SistemaTaxiMobil.Views
{
    public partial class ViajeEnCursoPage : ContentPage
    {
        private readonly ViajeEnCursoViewModel _viewModel;

        public ViajeEnCursoPage(ViajeEnCursoViewModel viewModel)
        {
            InitializeComponent();
            _viewModel = viewModel;
            BindingContext = viewModel;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            await _viewModel.InicializarAsync();

            // Configurar mapa
            var location = new Location(19.4326, -99.1332);
            var mapSpan = MapSpan.FromCenterAndRadius(location, Distance.FromKilometers(2));
            mapa.MoveToRegion(mapSpan);

            // Agregar pins
            var pinOrigen = new Pin
            {
                Label = "Origen",
                Location = location,
                Type = PinType.Place
            };

            var pinTaxi = new Pin
            {
                Label = "Taxi",
                Location = new Location(19.4400, -99.1400),
                Type = PinType.Generic
            };

            mapa.Pins.Add(pinOrigen);
            mapa.Pins.Add(pinTaxi);
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
        }
    }
}
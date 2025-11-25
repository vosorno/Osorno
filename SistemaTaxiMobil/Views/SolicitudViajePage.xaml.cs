using SistemaTaxiMobil.ViewModels;
using Microsoft.Maui.Controls.Maps;
using Microsoft.Maui.Maps;

namespace SistemaTaxiMobil.Views
{
    public partial class SolicitudViajePage : ContentPage
    {
        private readonly SolicitudViajeViewModel _viewModel;

        public SolicitudViajePage(SolicitudViajeViewModel viewModel)
        {
            InitializeComponent();
            _viewModel = viewModel;
            BindingContext = viewModel;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            // Configurar mapa centrado en Ciudad de México
            var location = new Location(19.4326, -99.1332);
            var mapSpan = MapSpan.FromCenterAndRadius(location, Distance.FromKilometers(5));
            mapa.MoveToRegion(mapSpan);

            // Agregar pin de ejemplo
            var pin = new Pin
            {
                Label = "Tu ubicación",
                Location = location,
                Type = PinType.Place
            };
            mapa.Pins.Add(pin);
        }
    }
}
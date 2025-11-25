using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SistemaTaxiMobil.Core.DTOs;
using SistemaTaxiMobil.Core.Interfaces;
using Microsoft.Maui.Controls.Maps;
using Microsoft.Maui.Maps;

namespace SistemaTaxiMobil.ViewModels
{
    public partial class SolicitudViajeViewModel : BaseViewModel
    {
        private readonly IViajeService _viajeService;

        [ObservableProperty]
        private Location? ubicacionOrigen;

        [ObservableProperty]
        private Location? ubicacionDestino;

        [ObservableProperty]
        private string direccionOrigen = "Seleccione origen";

        [ObservableProperty]
        private string direccionDestino = "Seleccione destino";

        [ObservableProperty]
        private decimal distanciaKm;

        [ObservableProperty]
        private decimal duracionMinutos;

        [ObservableProperty]
        private decimal costoEstimado;

        [ObservableProperty]
        private string metodoPago = "Efectivo";

        public SolicitudViajeViewModel(IViajeService viajeService)
        {
            _viajeService = viajeService;
            Title = "Solicitar Viaje";

            var metodoPagoId = Preferences.Get("MetodoPagoId", 1);
            MetodoPago = metodoPagoId == 1 ? "Efectivo" : "Tarjeta";
        }

        [RelayCommand]
        async Task SeleccionarOrigen()
        {
            // Simular selección de ubicación
            // En producción, usar geolocalización o selección en mapa
            UbicacionOrigen = new Location(19.4326, -99.1332);
            DireccionOrigen = "Ciudad de México, CDMX";

            if (UbicacionDestino != null)
            {
                await CalcularRuta();
            }
        }

        [RelayCommand]
        async Task SeleccionarDestino()
        {
            // Simular selección de ubicación
            UbicacionDestino = new Location(19.5000, -99.2000);
            DireccionDestino = "Destino seleccionado";

            if (UbicacionOrigen != null)
            {
                await CalcularRuta();
            }
        }

        private async Task CalcularRuta()
        {
            if (UbicacionOrigen == null || UbicacionDestino == null)
                return;

            IsBusy = true;

            try
            {
                var origen = new UbicacionDto
                {
                    Latitud = UbicacionOrigen.Latitude,
                    Longitud = UbicacionOrigen.Longitude
                };

                var destino = new UbicacionDto
                {
                    Latitud = UbicacionDestino.Latitude,
                    Longitud = UbicacionDestino.Longitude
                };

                var ruta = await _viajeService.CalcularRutaAsync(origen, destino);

                DistanciaKm = ruta.DistanciaKm;
                DuracionMinutos = ruta.DuracionMinutos;
                CostoEstimado = ruta.CostoEstimado;
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Error", $"Error al calcular ruta: {ex.Message}", "OK");
            }
            finally
            {
                IsBusy = false;
            }
        }

        [RelayCommand]
        async Task SolicitarViaje()
        {
            if (IsBusy) return;

            if (UbicacionOrigen == null || UbicacionDestino == null)
            {
                await Shell.Current.DisplayAlert("Error", "Por favor seleccione origen y destino", "OK");
                return;
            }

            IsBusy = true;

            try
            {
                var pasajeroId = Preferences.Get("PasajeroId", 0);
                var metodoPagoId = Preferences.Get("MetodoPagoId", 1);

                var solicitud = new SolicitudViajeDto
                {
                    PasajeroId = pasajeroId,
                    ConductorId = 1, // Simular asignación de conductor
                    MetodoPagoId = metodoPagoId,
                    OrigenLatitud = UbicacionOrigen.Latitude,
                    OrigenLongitud = UbicacionOrigen.Longitude,
                    DestinoLatitud = UbicacionDestino.Latitude,
                    DestinoLongitud = UbicacionDestino.Longitude,
                    DuracionEstimada = DuracionMinutos,
                    DistanciaKm = DistanciaKm
                };

                var viajeId = await _viajeService.CrearViajeAsync(solicitud);

                if (viajeId > 0)
                {
                    Preferences.Set("ViajeActivoId", viajeId);
                    await Shell.Current.GoToAsync("viajeEnCurso");
                }
                else
                {
                    await Shell.Current.DisplayAlert("Error", "No se pudo crear el viaje", "OK");
                }
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Error", $"Error al solicitar viaje: {ex.Message}", "OK");
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}
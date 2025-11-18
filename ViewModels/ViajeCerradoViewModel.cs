using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SistemaTaxiMobil.Core.DTOs;
using SistemaTaxiMobil.Core.Interfaces;

namespace SistemaTaxiMobil.ViewModels
{
    [QueryProperty(nameof(ViajeId), "viajeId")]
    public partial class ViajeCerradoViewModel : BaseViewModel
    {
        private readonly IViajeService _viajeService;

        [ObservableProperty]
        private int viajeId;

        [ObservableProperty]
        private decimal costoTotal;

        public ViajeCerradoViewModel(IViajeService viajeService)
        {
            _viajeService = viajeService;
            Title = "Viaje Finalizado";
        }

        public async Task InicializarAsync()
        {
            // Simular datos del viaje finalizado
            var tiempoTranscurrido = 15m; // 15 minutos
            var distanciaRecorrida = 5.5m; // 5.5 km

            CostoTotal = await _viajeService.CalcularCostoViajeAsync(tiempoTranscurrido, distanciaRecorrida);
        }

        [RelayCommand]
        async Task Aceptar()
        {
            if (IsBusy) return;

            IsBusy = true;

            try
            {
                var finalizacion = new FinalizarViajeDto
                {
                    ViajeId = ViajeId,
                    TiempoTranscurrido = 15m,
                    DistanciaRecorrida = 5.5m,
                    CostoTotal = CostoTotal
                };

                bool exito = await _viajeService.FinalizarViajeAsync(finalizacion);

                if (exito)
                {
                    await Shell.Current.DisplayAlert("Viaje Finalizado",
                        $"Costo total: ${CostoTotal:F2}", "OK");
                    await Shell.Current.GoToAsync("///tipoPago");
                }
                else
                {
                    await Shell.Current.DisplayAlert("Error", "No se pudo finalizar el viaje", "OK");
                }
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Error", $"Error: {ex.Message}", "OK");
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}
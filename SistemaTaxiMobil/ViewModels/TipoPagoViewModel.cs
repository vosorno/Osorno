using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SistemaTaxiMobil.Core.Interfaces;

namespace SistemaTaxiMobil.ViewModels
{
    public partial class TipoPagoViewModel : BaseViewModel
    {
        private readonly IPagoService _pagoService;

        [ObservableProperty]
        private bool esEfectivo = true;

        [ObservableProperty]
        private bool esTarjeta;

        public TipoPagoViewModel(IPagoService pagoService)
        {
            _pagoService = pagoService;
            Title = "Tipo de Pago";
        }

        [RelayCommand]
        void SeleccionarEfectivo()
        {
            EsEfectivo = true;
            EsTarjeta = false;
        }

        [RelayCommand]
        void SeleccionarTarjeta()
        {
            EsEfectivo = false;
            EsTarjeta = true;
        }

        [RelayCommand]
        async Task Continuar()
        {
            if (IsBusy) return;

            IsBusy = true;

            try
            {
                var pasajeroId = Preferences.Get("PasajeroId", 0);
                if (pasajeroId == 0)
                {
                    await Shell.Current.DisplayAlert("Error", "Sesión inválida", "OK");
                    return;
                }

                if (EsTarjeta)
                {
                    // Verificar si ya tiene tarjeta guardada
                    bool tieneTarjeta = await _pagoService.TieneTarjetaGuardadaAsync(pasajeroId);

                    if (!tieneTarjeta)
                    {
                        await Shell.Current.GoToAsync("registroTarjeta");
                    }
                    else
                    {
                        Preferences.Set("MetodoPagoId", 2); // Tarjeta
                        await Shell.Current.GoToAsync("solicitudViaje");
                    }
                }
                else
                {
                    Preferences.Set("MetodoPagoId", 1); // Efectivo
                    await Shell.Current.GoToAsync("solicitudViaje");
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
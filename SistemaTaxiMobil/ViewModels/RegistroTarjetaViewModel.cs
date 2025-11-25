using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SistemaTaxiMobil.Core.DTOs;
using SistemaTaxiMobil.Core.Interfaces;

namespace SistemaTaxiMobil.ViewModels
{
    public partial class RegistroTarjetaViewModel : BaseViewModel
    {
        private readonly IPagoService _pagoService;

        [ObservableProperty]
        private string numeroTarjeta = string.Empty;

        [ObservableProperty]
        private string nombreTitular = string.Empty;

        [ObservableProperty]
        private string fechaVencimiento = string.Empty;

        [ObservableProperty]
        private string cvv = string.Empty;

        public RegistroTarjetaViewModel(IPagoService pagoService)
        {
            _pagoService = pagoService;
            Title = "Registro de Tarjeta";
        }

        [RelayCommand]
        async Task GuardarTarjeta()
        {
            if (IsBusy) return;

            // Validaciones
            if (string.IsNullOrWhiteSpace(NumeroTarjeta) || string.IsNullOrWhiteSpace(NombreTitular) ||
                string.IsNullOrWhiteSpace(FechaVencimiento) || string.IsNullOrWhiteSpace(Cvv))
            {
                await Shell.Current.DisplayAlert("Error", "Por favor complete todos los campos", "OK");
                return;
            }

            if (NumeroTarjeta.Length != 16)
            {
                await Shell.Current.DisplayAlert("Error", "El número de tarjeta debe tener 16 dígitos", "OK");
                return;
            }

            if (!FechaVencimiento.Contains("/") || FechaVencimiento.Length != 5)
            {
                await Shell.Current.DisplayAlert("Error", "Formato de fecha inválido (MM/AA)", "OK");
                return;
            }

            if (Cvv.Length != 3 && Cvv.Length != 4)
            {
                await Shell.Current.DisplayAlert("Error", "El CVV debe tener 3 o 4 dígitos", "OK");
                return;
            }

            IsBusy = true;

            try
            {
                var pasajeroId = Preferences.Get("PasajeroId", 0);
                if (pasajeroId == 0)
                {
                    await Shell.Current.DisplayAlert("Error", "Sesión inválida", "OK");
                    return;
                }

                var tarjetaDto = new TarjetaDto
                {
                    NumeroTarjeta = NumeroTarjeta,
                    NombreTitular = NombreTitular,
                    FechaVencimiento = FechaVencimiento,
                    CVV = Cvv
                };

                var token = await _pagoService.GuardarTarjetaAsync(pasajeroId, tarjetaDto);

                if (!string.IsNullOrEmpty(token))
                {
                    await Shell.Current.DisplayAlert("Éxito", "Método de pago guardado correctamente", "OK");
                    Preferences.Set("MetodoPagoId", 2); // Tarjeta
                    await Shell.Current.GoToAsync("solicitudViaje");
                }
                else
                {
                    await Shell.Current.DisplayAlert("Error", "No se pudo guardar la tarjeta", "OK");
                }
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Error", $"Error al guardar tarjeta: {ex.Message}", "OK");
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}
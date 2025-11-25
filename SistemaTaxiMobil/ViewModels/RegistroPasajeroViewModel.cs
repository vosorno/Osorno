using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SistemaTaxiMobil.Core.DTOs;
using SistemaTaxiMobil.Core.Interfaces;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace SistemaTaxiMobil.ViewModels
{
    public partial class RegistroPasajeroViewModel : BaseViewModel
    {
        private readonly IAuthService _authService;

        [ObservableProperty]
        private string nombre = string.Empty;

        [ObservableProperty]
        private string apPaterno = string.Empty;

        [ObservableProperty]
        private string apMaterno = string.Empty;

        [ObservableProperty]
        private string telefono = string.Empty;

        [ObservableProperty]
        private string correo = string.Empty;

        [ObservableProperty]
        private string contrasena = string.Empty;

        public RegistroPasajeroViewModel(IAuthService authService)
        {
            _authService = authService;
            Title = "Registro de Pasajero";
        }

        [RelayCommand]
        async Task Aceptar()
        {
            if (IsBusy) return;

            // Validaciones
            if (string.IsNullOrWhiteSpace(Nombre) || string.IsNullOrWhiteSpace(ApPaterno) ||
                string.IsNullOrWhiteSpace(ApMaterno) || string.IsNullOrWhiteSpace(Correo) ||
                string.IsNullOrWhiteSpace(Contrasena))
            {
                await Shell.Current.DisplayAlert("Error", "Por favor complete todos los campos obligatorios", "OK");
                return;
            }

            if (!Correo.Contains("@") || !Correo.Contains("."))
            {
                await Shell.Current.DisplayAlert("Error", "Por favor ingrese un correo válido", "OK");
                return;
            }

            IsBusy = true;

            try
            {
                // Verificar si el correo ya existe
                if (await _authService.CorreoExisteAsync(Correo))
                {
                    await Shell.Current.DisplayAlert("Error", "El correo ya está registrado", "OK");
                    return;
                }

                var usuarioId = Preferences.Get("UsuarioId", 0);
                if (usuarioId == 0)
                {
                    await Shell.Current.DisplayAlert("Error", "Sesión inválida", "OK");
                    return;
                }

                var registroDto = new RegistroPasajeroDto
                {
                    UsuarioId = usuarioId,
                    Nombre = Nombre,
                    ApPaterno = ApPaterno,
                    ApMaterno = ApMaterno,
                    Telefono = Telefono,
                    Correo = Correo,
                    Contrasena = Contrasena
                };

                bool exito = await _authService.RegistrarPasajeroAsync(registroDto);

                if (exito)
                {
                    // Obtener el ID del pasajero
                    var usuarioAuth = await _authService.LoginAsync(new LoginDto
                    {
                        Usuario = Preferences.Get("UsuarioTemp", ""),
                        Password = Preferences.Get("PasswordTemp", ""),
                        RolUsuarioId = 1
                    });

                    if (usuarioAuth?.PasajeroId != null)
                    {
                        Preferences.Set("PasajeroId", usuarioAuth.PasajeroId.Value);
                    }

                    await Shell.Current.DisplayAlert("Éxito", "Registro completado", "OK");
                    await Shell.Current.GoToAsync("///tipoPago");
                }
                else
                {
                    await Shell.Current.DisplayAlert("Error", "No se pudo completar el registro", "OK");
                }
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Error", $"Error al registrar: {ex.Message}", "OK");
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}
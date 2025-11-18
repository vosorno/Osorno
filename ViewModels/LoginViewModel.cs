using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SistemaTaxiMobil.Core.DTOs;
using SistemaTaxiMobil.Core.Interfaces;

namespace SistemaTaxiMobil.ViewModels
{
    public partial class LoginViewModel : BaseViewModel
    {
        private readonly IAuthService _authService;

        [ObservableProperty]
        private string usuario = string.Empty;

        [ObservableProperty]
        private string password = string.Empty;

        [ObservableProperty]
        private int rolSeleccionado = 1; // 1=Pasajero, 2=Conductor

        [ObservableProperty]
        private bool esPasajero = true;

        public LoginViewModel(IAuthService authService)
        {
            _authService = authService;
            Title = "Iniciar Sesión";
        }

        [RelayCommand]
        void CambiarRol()
        {
            EsPasajero = !EsPasajero;
            RolSeleccionado = EsPasajero ? 1 : 2;
        }

        [RelayCommand]
        async Task IniciarSesion()
        {
            if (IsBusy) return;

            if (string.IsNullOrWhiteSpace(Usuario) || string.IsNullOrWhiteSpace(Password))
            {
                await Shell.Current.DisplayAlert("Error", "Por favor ingrese usuario y contraseña", "OK");
                return;
            }

            IsBusy = true;

            try
            {
                var loginDto = new LoginDto
                {
                    Usuario = Usuario,
                    Password = Password,
                    RolUsuarioId = RolSeleccionado
                };

                var usuarioAutenticado = await _authService.LoginAsync(loginDto);

                if (usuarioAutenticado == null)
                {
                    if (RolSeleccionado == 2) // Conductor
                    {
                        await Shell.Current.DisplayAlert("Error",
                            "No existe un conductor con la información registrada", "OK");
                    }
                    else // Pasajero
                    {
                        // Ofrecer registro
                        bool registrar = await Shell.Current.DisplayAlert("Usuario no encontrado",
                            "¿Desea registrarse?", "Sí", "No");

                        if (registrar)
                        {
                            await Shell.Current.GoToAsync("registroUsuario");
                        }
                    }
                    return;
                }

                // Guardar datos de sesión
                Preferences.Set("UsuarioId", usuarioAutenticado.UsuarioId);
                Preferences.Set("RolUsuarioId", usuarioAutenticado.RolUsuarioId);

                if (usuarioAutenticado.PasajeroId.HasValue)
                {
                    Preferences.Set("PasajeroId", usuarioAutenticado.PasajeroId.Value);
                    await Shell.Current.GoToAsync("///tipoPago");
                }
                else if (usuarioAutenticado.ConductorId.HasValue)
                {
                    Preferences.Set("ConductorId", usuarioAutenticado.ConductorId.Value);
                    // Navegar a vista de conductor (no implementada en este scope)
                    await Shell.Current.DisplayAlert("Éxito", "Bienvenido conductor", "OK");
                }
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Error", $"Error al iniciar sesión: {ex.Message}", "OK");
            }
            finally
            {
                IsBusy = false;
            }
        }

        [RelayCommand]
        async Task Registrarse()
        {
            await Shell.Current.GoToAsync("registroUsuario");
        }
    }
}
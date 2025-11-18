using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SistemaTaxiMobil.Core.DTOs;
using SistemaTaxiMobil.Core.Interfaces;

namespace SistemaTaxiMobil.ViewModels
{
    public partial class RegistroUsuarioViewModel : BaseViewModel
    {
        private readonly IAuthService _authService;

        [ObservableProperty]
        private string usuario = string.Empty;

        [ObservableProperty]
        private string password = string.Empty;

        [ObservableProperty]
        private string confirmarPassword = string.Empty;

        public RegistroUsuarioViewModel(IAuthService authService)
        {
            _authService = authService;
            Title = "Registro de Usuario";
        }

        [RelayCommand]
        async Task Registrar()
        {
            if (IsBusy) return;

            // Validaciones
            if (string.IsNullOrWhiteSpace(Usuario) || string.IsNullOrWhiteSpace(Password))
            {
                await Shell.Current.DisplayAlert("Error", "Por favor complete todos los campos", "OK");
                return;
            }

            if (Password != ConfirmarPassword)
            {
                await Shell.Current.DisplayAlert("Error", "Las contraseñas no coinciden", "OK");
                return;
            }

            if (Password.Length < 6)
            {
                await Shell.Current.DisplayAlert("Error", "La contraseña debe tener al menos 6 caracteres", "OK");
                return;
            }

            IsBusy = true;

            try
            {
                // Verificar si el usuario ya existe
                if (await _authService.UsuarioExisteAsync(Usuario))
                {
                    await Shell.Current.DisplayAlert("Error", "El usuario ya existe", "OK");
                    return;
                }

                var registroDto = new RegistroUsuarioDto
                {
                    Usuario = Usuario,
                    Password = Password,
                    RolUsuarioId = 1 // Pasajero
                };

                bool exito = await _authService.RegistrarUsuarioAsync(registroDto);

                if (exito)
                {
                    // Obtener el ID del usuario recién creado
                    var usuarioCreado = await _authService.LoginAsync(new LoginDto
                    {
                        Usuario = Usuario,
                        Password = Password,
                        RolUsuarioId = 1
                    });

                    if (usuarioCreado != null)
                    {
                        Preferences.Set("UsuarioId", usuarioCreado.UsuarioId);
                        await Shell.Current.GoToAsync("registroPasajero");
                    }
                }
                else
                {
                    await Shell.Current.DisplayAlert("Error", "No se pudo crear el usuario", "OK");
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
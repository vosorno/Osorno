using SistemaTaxiMobil.Core.DTOs;

namespace SistemaTaxiMobil.Core.Interfaces
{
    public interface IAuthService
    {
        Task<UsuarioAutenticadoDto?> LoginAsync(LoginDto loginDto);
        Task<bool> RegistrarUsuarioAsync(RegistroUsuarioDto registroDto);
        Task<bool> RegistrarPasajeroAsync(RegistroPasajeroDto pasajeroDto);
        Task<bool> UsuarioExisteAsync(string usuario);
        Task<bool> CorreoExisteAsync(string correo);
    }
}
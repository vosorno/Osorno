using System;
using SistemaTaxiMobil.Core.DTOs;
using SistemaTaxiMobil.Core.Interfaces;

namespace SistemaTaxiMobil.Services;

public class AuthService : IAuthService
{
    public Task<bool> CorreoExisteAsync(string correo)
    {
        throw new NotImplementedException();
    }

    public Task<UsuarioAutenticadoDto?> LoginAsync(LoginDto loginDto)
    {
        throw new NotImplementedException();
    }

    public Task<bool> RegistrarPasajeroAsync(RegistroPasajeroDto pasajeroDto)
    {
        throw new NotImplementedException();
    }

    public Task<bool> RegistrarUsuarioAsync(RegistroUsuarioDto registroDto)
    {
        throw new NotImplementedException();
    }

    public Task<bool> UsuarioExisteAsync(string usuario)
    {
        throw new NotImplementedException();
    }
}

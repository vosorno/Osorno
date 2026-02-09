using System;
using SistemaTaxiMobil.Core.DTOs;
using SistemaTaxiMobil.Core.Interfaces;

namespace SistemaTaxiMobil.Services;

public class AuthService(ITaxiApi restService) : IAuthService
{
    private readonly ITaxiApi _api = restService;
    public Task<bool> CorreoExisteAsync(string correo)
    {
        throw new NotImplementedException();
    }

    public async Task<UsuarioAutenticadoDto?> LoginAsync(LoginDto loginDto)
    {
        try
        {
            UsuarioAutenticadoDto dto;
            var request = await _api.LoginAsync(loginDto);
            if (request.IsSuccessStatusCode)
            {
                dto = request.Content!;
            }
            else
            {
                Console.WriteLine($"Login failed: {request.StatusCode}");
                return null;
            }
            return dto;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            throw;
        }
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

using Refit;
using SistemaTaxiMobil.Core.DTOs;

namespace SistemaTaxiMobil.Services;

public interface ITaxiApi
{
    [Post("/api/auth/login")]
    Task<ApiResponse<UsuarioAutenticadoDto>> LoginAsync([Body] LoginDto loginDto);
}

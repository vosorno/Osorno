using SistemaTaxiMobil.Core.DTOs;

namespace SistemaTaxiMobil.Core.Interfaces
{
    public interface IMapService
    {
        Task<RutaDto> ObtenerRutaAsync(double origenLat, double origenLng, double destinoLat, double destinoLng);
        Task<string> ObtenerDireccionAsync(double latitud, double longitud);
        Task<UbicacionDto> BuscarDireccionAsync(string direccion);
    }
}
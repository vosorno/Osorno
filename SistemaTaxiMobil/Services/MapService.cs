using System;
using SistemaTaxiMobil.Core.DTOs;
using SistemaTaxiMobil.Core.Interfaces;

namespace SistemaTaxiMobil.Services;

public class MapService : IMapService
{
    public Task<UbicacionDto> BuscarDireccionAsync(string direccion)
    {
        throw new NotImplementedException();
    }

    public Task<string> ObtenerDireccionAsync(double latitud, double longitud)
    {
        throw new NotImplementedException();
    }

    public Task<RutaDto> ObtenerRutaAsync(double origenLat, double origenLng, double destinoLat, double destinoLng)
    {
        throw new NotImplementedException();
    }
}

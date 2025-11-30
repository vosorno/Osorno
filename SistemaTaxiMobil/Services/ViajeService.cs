using System;
using SistemaTaxiMobil.Core.DTOs;
using SistemaTaxiMobil.Core.Interfaces;

namespace SistemaTaxiMobil.Services;

public class ViajeService : IViajeService
{
    public Task<decimal> CalcularCostoViajeAsync(decimal tiempoMinutos, decimal distanciaKm)
    {
        throw new NotImplementedException();
    }

    public Task<RutaDto> CalcularRutaAsync(UbicacionDto origen, UbicacionDto destino)
    {
        throw new NotImplementedException();
    }

    public Task<int> CrearViajeAsync(SolicitudViajeDto solicitud)
    {
        throw new NotImplementedException();
    }

    public Task<bool> FinalizarViajeAsync(FinalizarViajeDto finalizacion)
    {
        throw new NotImplementedException();
    }

    public Task<ParametrosTarifaDto> ObtenerParametrosTarifaAsync()
    {
        throw new NotImplementedException();
    }

    public Task<ViajeActivoDto?> ObtenerViajeActivoAsync(int pasajeroId)
    {
        throw new NotImplementedException();
    }
}

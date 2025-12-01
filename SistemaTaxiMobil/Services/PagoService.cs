using System;
using SistemaTaxiMobil.Core.DTOs;
using SistemaTaxiMobil.Core.Interfaces;

namespace SistemaTaxiMobil.Services;

public class PagoService : IPagoService
{
    public Task<string?> GenerarTokenMercadoPagoAsync(TarjetaDto tarjeta)
    {
        throw new NotImplementedException();
    }

    public Task<string?> GuardarTarjetaAsync(int pasajeroId, TarjetaDto tarjeta)
    {
        throw new NotImplementedException();
    }

    public Task<List<MetodoPagoDto>> ObtenerMetodosPagoAsync()
    {
        throw new NotImplementedException();
    }

    public Task<bool> TieneTarjetaGuardadaAsync(int pasajeroId)
    {
        throw new NotImplementedException();
    }
}

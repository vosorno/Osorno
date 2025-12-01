using SistemaTaxiMobil.Core.DTOs;

namespace SistemaTaxiMobil.Core.Interfaces
{
    public interface IViajeService
    {
        Task<RutaDto> CalcularRutaAsync(UbicacionDto origen, UbicacionDto destino);
        Task<int> CrearViajeAsync(SolicitudViajeDto solicitud);
        Task<ViajeActivoDto?> ObtenerViajeActivoAsync(int pasajeroId);
        Task<bool> FinalizarViajeAsync(FinalizarViajeDto finalizacion);
        Task<decimal> CalcularCostoViajeAsync(decimal tiempoMinutos, decimal distanciaKm);
        Task<ParametrosTarifaDto> ObtenerParametrosTarifaAsync();
    }
}
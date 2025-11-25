using SistemaTaxiMobil.Core.DTOs;

namespace SistemaTaxiMobil.Core.Interfaces
{
    public interface IPagoService
    {
        Task<bool> TieneTarjetaGuardadaAsync(int pasajeroId);
        Task<string?> GuardarTarjetaAsync(int pasajeroId, TarjetaDto tarjeta);
        Task<List<MetodoPagoDto>> ObtenerMetodosPagoAsync();
        Task<string?> GenerarTokenMercadoPagoAsync(TarjetaDto tarjeta);
    }

    public class MetodoPagoDto
    {
        public int MetodoPagoId { get; set; }
        public string Nombre { get; set; } = string.Empty;
    }
}
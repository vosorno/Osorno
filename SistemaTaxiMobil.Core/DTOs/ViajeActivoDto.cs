namespace SistemaTaxiMobil.Core.DTOs
{
    public class ViajeActivoDto
    {
        public int ViajeId { get; set; }
        public int EstadoViajeId { get; set; }
        public string NombreConductor { get; set; } = string.Empty;
        public string Placas { get; set; } = string.Empty;
        public string DescripcionVehiculo { get; set; } = string.Empty;
        public double ConductorLatitud { get; set; }
        public double ConductorLongitud { get; set; }
        public decimal? CostoTotal { get; set; }
        public DateTime? FechaInicio { get; set; }
    }
}
namespace SistemaTaxiMobil.Core.DTOs
{
    public class RutaDto
    {
        public List<UbicacionDto> Puntos { get; set; } = new();
        public decimal DistanciaKm { get; set; }
        public decimal DuracionMinutos { get; set; }
        public decimal CostoEstimado { get; set; }
    }
}
namespace SistemaTaxiMobil.Core.DTOs
{
    public class FinalizarViajeDto
    {
        public int ViajeId { get; set; }
        public decimal TiempoTranscurrido { get; set; }
        public decimal DistanciaRecorrida { get; set; }
        public decimal CostoTotal { get; set; }
    }
}
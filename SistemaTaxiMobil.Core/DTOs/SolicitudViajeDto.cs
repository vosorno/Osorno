namespace SistemaTaxiMobil.Core.DTOs
{
    public class SolicitudViajeDto
    {
        public int PasajeroId { get; set; }
        public int ConductorId { get; set; }
        public int MetodoPagoId { get; set; }
        public double OrigenLatitud { get; set; }
        public double OrigenLongitud { get; set; }
        public double DestinoLatitud { get; set; }
        public double DestinoLongitud { get; set; }
        public decimal DuracionEstimada { get; set; }
        public decimal DistanciaKm { get; set; }
    }
}
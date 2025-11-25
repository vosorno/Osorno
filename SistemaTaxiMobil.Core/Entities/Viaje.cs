namespace SistemaTaxiMobil.Core.Entities
{
    public class Viaje
    {
        public int ViajeId { get; set; }
        public int PasajeroId { get; set; }
        public int ConductorId { get; set; }
        public int? LocalidadOrigenId { get; set; }
        public int? LocalidadDestinoId { get; set; }
        public int MetodoPagoId { get; set; }
        public int? EstadoViajeId { get; set; }
        public string PuntoOrigen { get; set; } = string.Empty;
        public string PuntoDestino { get; set; } = string.Empty;
        public decimal? DuracionMinutosEstimado { get; set; }
        public decimal? CostoTotal { get; set; }
        public DateTime? FechaInicio { get; set; }
        public DateTime? FechaFin { get; set; }
        public string FormaPago { get; set; } = string.Empty;

        // Navigation properties
        public virtual Pasajero Pasajero { get; set; } = null!;
        public virtual Conductor Conductor { get; set; } = null!;
        public virtual Localidad? LocalidadOrigen { get; set; }
        public virtual Localidad? LocalidadDestino { get; set; }
        public virtual MetodoPago MetodoPago { get; set; } = null!;
        public virtual EstadoViaje? EstadoViaje { get; set; }
        public virtual ICollection<CancelaViaje> CancelaViajes { get; set; } = new List<CancelaViaje>();
    }
}
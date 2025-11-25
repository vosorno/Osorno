namespace SistemaTaxiMobil.Core.Entities
{
    public class CancelaViaje
    {
        public int CancelaViajeId { get; set; }
        public int ViajeId { get; set; }
        public string DescripcionCancelacion { get; set; } = string.Empty;

        // Navigation properties
        public virtual Viaje Viaje { get; set; } = null!;
    }
}
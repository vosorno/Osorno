namespace SistemaTaxiMobil.Core.Entities
{
    public class MetodoPago
    {
        public int MetodoPagoId { get; set; }
        public string Nombre { get; set; } = string.Empty;

        // Navigation properties
        public virtual ICollection<Viaje> Viajes { get; set; } = new List<Viaje>();
    }
}
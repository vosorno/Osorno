namespace SistemaTaxiMobil.Core.Entities
{
    public class RechazoViaje
    {
        public int RechazoViajeId { get; set; }
        public int ConductorId { get; set; }
        public DateTime? Fecha { get; set; }

        // Navigation properties
        public virtual Conductor Conductor { get; set; } = null!;
    }
}
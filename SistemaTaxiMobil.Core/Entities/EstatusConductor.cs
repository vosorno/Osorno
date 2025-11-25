namespace SistemaTaxiMobil.Core.Entities
{
    public class EstatusConductor
    {
        public int EstatusConductorId { get; set; }
        public string Nombre { get; set; } = string.Empty;

        // Navigation properties
        public virtual ICollection<Conductor> Conductores { get; set; } = new List<Conductor>();
    }
}
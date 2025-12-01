using NetTopologySuite.Geometries;

namespace SistemaTaxiMobil.Core.Entities
{
    public class BaseTaxi
    {
        public int BaseTaxiId { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public Geometry? Area { get; set; }

        // Navigation properties
        public virtual ICollection<Conductor> Conductores { get; set; } = new List<Conductor>();
    }
}
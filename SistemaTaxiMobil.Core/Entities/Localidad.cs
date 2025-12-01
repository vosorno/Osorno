using NetTopologySuite.Geometries;

namespace SistemaTaxiMobil.Core.Entities
{
    public class Localidad
    {
        public int LocalidadId { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public Geometry? Area { get; set; }

        // Navigation properties
        public virtual ICollection<Conductor> Conductores { get; set; } = new List<Conductor>();
        public virtual ICollection<Viaje> ViajesOrigen { get; set; } = new List<Viaje>();
        public virtual ICollection<Viaje> ViajesDestino { get; set; } = new List<Viaje>();
    }
}
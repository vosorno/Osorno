using NetTopologySuite.Geometries;

namespace SistemaTaxiMobil.Core.Entities
{
    public class Conductor
    {
        public int ConductorId { get; set; }
        public int UsuarioId { get; set; }
        public int BaseTaxiId { get; set; }
        public int LocalidadId { get; set; }
        public int EstatusConductorId { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string ApPaterno { get; set; } = string.Empty;
        public string ApMaterno { get; set; } = string.Empty;
        public string? Telefono { get; set; }
        public string DescripcionVehiculo { get; set; } = string.Empty;
        public string Placas { get; set; } = string.Empty;
        public bool? Disponible { get; set; }

        // Navigation properties
        public virtual Usuario Usuario { get; set; } = null!;
        public virtual BaseTaxi BaseTaxi { get; set; } = null!;
        public virtual Localidad Localidad { get; set; } = null!;
        public virtual EstatusConductor EstatusConductor { get; set; } = null!;
        public virtual ICollection<ConductorTurno> ConductorTurnos { get; set; } = new List<ConductorTurno>();
        public virtual ICollection<RechazoViaje> RechazoViajes { get; set; } = new List<RechazoViaje>();
        public virtual ICollection<Viaje> Viajes { get; set; } = new List<Viaje>();
    }
}
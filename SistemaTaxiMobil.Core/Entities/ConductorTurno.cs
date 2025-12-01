namespace SistemaTaxiMobil.Core.Entities
{
    public class ConductorTurno
    {
        public int ConductorTurnoId { get; set; }
        public int? ConductorId { get; set; }
        public int? TurnoId { get; set; }

        // Navigation properties
        public virtual Conductor? Conductor { get; set; }
        public virtual Turno? Turno { get; set; }
    }
}
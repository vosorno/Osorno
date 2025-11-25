namespace SistemaTaxiMobil.Core.Entities
{
    public class Turno
    {
        public int TurnoId { get; set; }
        public string Nombre { get; set; } = string.Empty;

        // Navigation properties
        public virtual ICollection<ConductorTurno> ConductorTurnos { get; set; } = new List<ConductorTurno>();
    }
}
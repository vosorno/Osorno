namespace SistemaTaxiMobil.Core.Entities
{
    public class RolUsuario
    {
        public int RolUsuarioId { get; set; }
        public string Nombre { get; set; } = string.Empty;

        // Navigation properties
        public virtual ICollection<Usuario> Usuarios { get; set; } = new List<Usuario>();
        public virtual ICollection<Pasajero> Pasajeros { get; set; } = new List<Pasajero>();
    }
}
namespace SistemaTaxiMobil.Core.Entities
{
    public class Usuario
    {
        public int UsuarioId { get; set; }
        public int? RolUsuarioId { get; set; }
        public string Usuario1 { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;

        // Navigation properties
        public virtual RolUsuario? RolUsuario { get; set; }
        public virtual ICollection<Conductor> Conductores { get; set; } = new List<Conductor>();
        public virtual ICollection<Pasajero> Pasajeros { get; set; } = new List<Pasajero>();
    }
}

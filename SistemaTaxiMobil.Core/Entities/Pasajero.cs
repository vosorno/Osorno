namespace SistemaTaxiMobil.Core.Entities
{
    public class Pasajero
    {
        public int PasajeroId { get; set; }
        public int UsuarioId { get; set; }
        public int RolUsuarioId { get; set; }
        public string? Telefono { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string ApPaterno { get; set; } = string.Empty;
        public string ApMaterno { get; set; } = string.Empty;
        public string Correo { get; set; } = string.Empty;
        public string Contrasena { get; set; } = string.Empty;
        public bool? Activo { get; set; }

        // Navigation properties
        public virtual Usuario Usuario { get; set; } = null!;
        public virtual RolUsuario RolUsuario { get; set; } = null!;
        public virtual ICollection<DatosTarjeta> DatosTarjetas { get; set; } = new List<DatosTarjeta>();
        public virtual ICollection<Viaje> Viajes { get; set; } = new List<Viaje>();
    }
}
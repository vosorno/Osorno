namespace SistemaTaxiMobil.Core.DTOs
{
    public class UsuarioAutenticadoDto
    {
        public int UsuarioId { get; set; }
        public int? PasajeroId { get; set; }
        public int? ConductorId { get; set; }
        public int RolUsuarioId { get; set; }
        public string NombreRol { get; set; } = string.Empty;
        public string NombreCompleto { get; set; } = string.Empty;
        public string Correo { get; set; } = string.Empty;
    }
}
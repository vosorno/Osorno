namespace SistemaTaxiMobil.Core.DTOs
{
    public class RegistroPasajeroDto
    {
        public int UsuarioId { get; set; }
        public string Telefono { get; set; } = string.Empty;
        public string Nombre { get; set; } = string.Empty;
        public string ApPaterno { get; set; } = string.Empty;
        public string ApMaterno { get; set; } = string.Empty;
        public string Correo { get; set; } = string.Empty;
        public string Contrasena { get; set; } = string.Empty;
    }
}
namespace SistemaTaxiMobil.Core.DTOs
{
    public class LoginDto
    {
        public string Usuario { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public int RolUsuarioId { get; set; }
    }
}
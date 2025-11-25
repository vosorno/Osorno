namespace SistemaTaxiMobil.Core.DTOs
{
    public class TarjetaDto
    {
        public string NumeroTarjeta { get; set; } = string.Empty;
        public string NombreTitular { get; set; } = string.Empty;
        public string FechaVencimiento { get; set; } = string.Empty;
        public string CVV { get; set; } = string.Empty;
    }
}
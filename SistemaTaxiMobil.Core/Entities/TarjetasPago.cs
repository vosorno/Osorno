namespace SistemaTaxiMobil.Core.Entities
{
    public class TarjetasPago
    {
        public int TarjetasPagoId { get; set; }
        public int DatosTarjetaId { get; set; }
        public string Token { get; set; } = string.Empty;

        // Navigation properties
        public virtual DatosTarjeta DatosTarjeta { get; set; } = null!;
    }
}
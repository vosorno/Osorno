namespace SistemaTaxiMobil.Core.Entities
{
    public class DatosTarjeta
    {
        public int DatosTarjetaId { get; set; }
        public int PasajeroId { get; set; }
        public string Token { get; set; } = string.Empty;

        // Navigation properties
        public virtual Pasajero Pasajero { get; set; } = null!;
        public virtual ICollection<TarjetasPago> TarjetasPagos { get; set; } = new List<TarjetasPago>();
    }
}
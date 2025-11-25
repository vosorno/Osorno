namespace SistemaTaxiMobil.Core.DTOs
{
    public class UbicacionDto
    {
        public double Latitud { get; set; }
        public double Longitud { get; set; }
        public string Direccion { get; set; } = string.Empty;
    }
}
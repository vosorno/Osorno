namespace SistemaTaxiMobil.Core.DTOs
{
    public class ParametrosTarifaDto
    {
        public decimal TarifaBase { get; set; }
        public decimal CostoMinuto { get; set; }
        public decimal CostoKilometro { get; set; }
    }
}
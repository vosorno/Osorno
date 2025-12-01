namespace SistemaTaxiMobil.Core.Entities
{
    public class Parametros
    {
        public byte ParametrosId { get; set; }
        public decimal? TarifaBase { get; set; }
        public decimal? CostoMinuto { get; set; }
        public decimal? CostoKilometro { get; set; }
    }
}
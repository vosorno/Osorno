using SistemaTaxiMobil.Core.DTOs;
using SistemaTaxiMobil.Core.Interfaces;

namespace SistemaTaxiMobil.Core.Services
{
    public class MapService : IMapService
    {
        private const string GOOGLE_MAPS_API_KEY = "YOUR-GOOGLE-MAPS-API-KEY";

        public async Task<RutaDto> ObtenerRutaAsync(double origenLat, double origenLng, double destinoLat, double destinoLng)
        {
            try
            {
                var distanciaKm = CalcularDistanciaHaversine(origenLat, origenLng, destinoLat, destinoLng);
                var duracionMinutos = (distanciaKm / 40.0m) * 60;

                var puntos = new List<UbicacionDto>
                {
                    new UbicacionDto { Latitud = origenLat, Longitud = origenLng },
                    new UbicacionDto { Latitud = destinoLat, Longitud = destinoLng }
                };

                await Task.Delay(300);

                return new RutaDto
                {
                    Puntos = puntos,
                    DistanciaKm = distanciaKm,
                    DuracionMinutos = duracionMinutos,
                    CostoEstimado = 50 + (2 * duracionMinutos) + (10 * distanciaKm)
                };
            }
            catch
            {
                return new RutaDto();
            }
        }

        public async Task<string> ObtenerDireccionAsync(double latitud, double longitud)
        {
            await Task.Delay(200);
            return $"Dirección aproximada: {latitud:F6}, {longitud:F6}";
        }

        public async Task<UbicacionDto> BuscarDireccionAsync(string direccion)
        {
            await Task.Delay(200);
            return new UbicacionDto
            {
                Latitud = 19.4326,
                Longitud = -99.1332,
                Direccion = direccion
            };
        }

        private decimal CalcularDistanciaHaversine(double lat1, double lon1, double lat2, double lon2)
        {
            const double R = 6371;

            var dLat = ToRadians(lat2 - lat1);
            var dLon = ToRadians(lon2 - lon1);

            var a = Math.Sin(dLat / 2) * Math.Sin(dLat / 2) +
                   Math.Cos(ToRadians(lat1)) * Math.Cos(ToRadians(lat2)) *
                   Math.Sin(dLon / 2) * Math.Sin(dLon / 2);

            var c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
            var distance = R * c;

            return (decimal)Math.Round(distance, 2);
        }

        private double ToRadians(double degrees)
        {
            return degrees * (Math.PI / 180);
        }
    }
}
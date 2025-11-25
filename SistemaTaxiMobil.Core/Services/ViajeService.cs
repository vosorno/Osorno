using SistemaTaxiMobil.Core.DTOs;
using SistemaTaxiMobil.Core.Entities;
using SistemaTaxiMobil.Core.Interfaces;
using System.Text.Json;

namespace SistemaTaxiMobil.Core.Services
{
    public class ViajeService : IViajeService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapService _mapService;

        public ViajeService(IUnitOfWork unitOfWork, IMapService mapService)
        {
            _unitOfWork = unitOfWork;
            _mapService = mapService;
        }

        public async Task<RutaDto> CalcularRutaAsync(UbicacionDto origen, UbicacionDto destino)
        {
            return await _mapService.ObtenerRutaAsync(
                origen.Latitud, origen.Longitud,
                destino.Latitud, destino.Longitud);
        }

        public async Task<int> CrearViajeAsync(SolicitudViajeDto solicitud)
        {
            try
            {
                var parametros = await ObtenerParametrosTarifaAsync();
                var costoEstimado = await CalcularCostoViajeAsync(
                    solicitud.DuracionEstimada,
                    solicitud.DistanciaKm);

                var puntoOrigen = JsonSerializer.Serialize(new
                {
                    latitud = solicitud.OrigenLatitud,
                    longitud = solicitud.OrigenLongitud
                });

                var puntoDestino = JsonSerializer.Serialize(new
                {
                    latitud = solicitud.DestinoLatitud,
                    longitud = solicitud.DestinoLongitud
                });

                var viaje = new Viaje
                {
                    PasajeroId = solicitud.PasajeroId,
                    ConductorId = solicitud.ConductorId,
                    MetodoPagoId = solicitud.MetodoPagoId,
                    EstadoViajeId = 2,
                    PuntoOrigen = puntoOrigen,
                    PuntoDestino = puntoDestino,
                    DuracionMinutosEstimado = solicitud.DuracionEstimada,
                    FechaInicio = DateTime.Now,
                    FormaPago = solicitud.MetodoPagoId == 1 ? "Efectivo" : "Tarjeta"
                };

                await _unitOfWork.Viajes.AddAsync(viaje);
                await _unitOfWork.SaveChangesAsync();

                return viaje.ViajeId;
            }
            catch
            {
                return 0;
            }
        }

        public async Task<ViajeActivoDto?> ObtenerViajeActivoAsync(int pasajeroId)
        {
            var viaje = await _unitOfWork.Viajes
                .FirstOrDefaultAsync(v => v.PasajeroId == pasajeroId &&
                                         (v.EstadoViajeId == 2));

            if (viaje == null)
                return null;

            var conductor = await _unitOfWork.Conductores
                .GetByIdAsync(viaje.ConductorId);

            if (conductor == null)
                return null;

            return new ViajeActivoDto
            {
                ViajeId = viaje.ViajeId,
                EstadoViajeId = viaje.EstadoViajeId ?? 0,
                NombreConductor = $"{conductor.Nombre} {conductor.ApPaterno}",
                Placas = conductor.Placas,
                DescripcionVehiculo = conductor.DescripcionVehiculo,
                CostoTotal = viaje.CostoTotal,
                FechaInicio = viaje.FechaInicio
            };
        }

        public async Task<bool> FinalizarViajeAsync(FinalizarViajeDto finalizacion)
        {
            try
            {
                var viaje = await _unitOfWork.Viajes.GetByIdAsync(finalizacion.ViajeId);
                if (viaje == null)
                    return false;

                viaje.CostoTotal = finalizacion.CostoTotal;
                viaje.EstadoViajeId = 3;
                viaje.FechaFin = DateTime.Now;

                _unitOfWork.Viajes.Update(viaje);
                await _unitOfWork.SaveChangesAsync();

                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<decimal> CalcularCostoViajeAsync(decimal tiempoMinutos, decimal distanciaKm)
        {
            var parametros = await ObtenerParametrosTarifaAsync();

            var costoTotal = parametros.TarifaBase +
                           (parametros.CostoMinuto * tiempoMinutos) +
                           (parametros.CostoKilometro * distanciaKm);

            return Math.Round(costoTotal, 2);
        }

        public async Task<ParametrosTarifaDto> ObtenerParametrosTarifaAsync()
        {
            var parametros = await _unitOfWork.Parametros.GetByIdAsync(1);

            if (parametros == null)
            {
                return new ParametrosTarifaDto
                {
                    TarifaBase = 50,
                    CostoMinuto = 2,
                    CostoKilometro = 10
                };
            }

            return new ParametrosTarifaDto
            {
                TarifaBase = parametros.TarifaBase ?? 50,
                CostoMinuto = parametros.CostoMinuto ?? 2,
                CostoKilometro = parametros.CostoKilometro ?? 10
            };
        }
    }
}
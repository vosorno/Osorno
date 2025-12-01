using Microsoft.AspNetCore.Mvc;
using SistemaTaxiMobil.Core.DTOs;
using SistemaTaxiMobil.Core.Interfaces;

namespace SistemaTaxiMobil.API.Controllers
{
    /// <summary>
    /// Controlador de gestión de viajes
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class ViajesController : ControllerBase
    {
        private readonly IViajeService _viajeService;
        private readonly ILogger<ViajesController> _logger;

        public ViajesController(IViajeService viajeService, ILogger<ViajesController> logger)
        {
            _viajeService = viajeService;
            _logger = logger;
        }

        /// <summary>
        /// Crear un nuevo viaje
        /// </summary>
        /// <param name="solicitud">Datos del viaje a crear</param>
        /// <returns>ID del viaje creado</returns>
        /// <response code="200">Viaje creado exitosamente</response>
        /// <response code="400">Error en los datos del viaje</response>
        [HttpPost]
        [ProducesResponseType(typeof(ApiResponse<int>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<int>), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ApiResponse<int>>> CrearViaje([FromBody] SolicitudViajeDto solicitud)
        {
            try
            {
                _logger.LogInformation($"Creando viaje - Pasajero: {solicitud.PasajeroId}, Conductor: {solicitud.ConductorId}");

                // Validaciones
                if (solicitud.PasajeroId <= 0 || solicitud.ConductorId <= 0)
                {
                    return BadRequest(new ApiResponse<int>
                    {
                        Success = false,
                        Message = "IDs de pasajero y conductor son requeridos"
                    });
                }

                if (solicitud.OrigenLatitud == 0 || solicitud.OrigenLongitud == 0 ||
                    solicitud.DestinoLatitud == 0 || solicitud.DestinoLongitud == 0)
                {
                    return BadRequest(new ApiResponse<int>
                    {
                        Success = false,
                        Message = "Coordenadas de origen y destino son requeridas"
                    });
                }

                var viajeId = await _viajeService.CrearViajeAsync(solicitud);

                if (viajeId > 0)
                {
                    _logger.LogInformation($"Viaje creado exitosamente - ViajeId: {viajeId}");

                    return Ok(new ApiResponse<int>
                    {
                        Success = true,
                        Message = "Viaje creado exitosamente",
                        Data = viajeId
                    });
                }

                return BadRequest(new ApiResponse<int>
                {
                    Success = false,
                    Message = "Error al crear el viaje"
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al crear viaje");
                return StatusCode(500, new ApiResponse<int>
                {
                    Success = false,
                    Message = "Error interno del servidor al crear viaje"
                });
            }
        }

        /// <summary>
        /// Obtener el viaje activo de un pasajero
        /// </summary>
        /// <param name="pasajeroId">ID del pasajero</param>
        /// <returns>Datos del viaje activo</returns>
        /// <response code="200">Viaje activo encontrado</response>
        /// <response code="404">No hay viaje activo</response>
        [HttpGet("activo/{pasajeroId}")]
        [ProducesResponseType(typeof(ApiResponse<ViajeActivoDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<ViajeActivoDto>), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ApiResponse<ViajeActivoDto>>> ObtenerViajeActivo(int pasajeroId)
        {
            try
            {
                _logger.LogInformation($"Obteniendo viaje activo para pasajero: {pasajeroId}");

                if (pasajeroId <= 0)
                {
                    return BadRequest(new ApiResponse<ViajeActivoDto>
                    {
                        Success = false,
                        Message = "ID de pasajero inválido"
                    });
                }

                var viaje = await _viajeService.ObtenerViajeActivoAsync(pasajeroId);

                if (viaje == null)
                {
                    _logger.LogInformation($"No hay viaje activo para pasajero: {pasajeroId}");

                    return NotFound(new ApiResponse<ViajeActivoDto>
                    {
                        Success = false,
                        Message = "No hay viaje activo para este pasajero"
                    });
                }

                return Ok(new ApiResponse<ViajeActivoDto>
                {
                    Success = true,
                    Message = "Viaje activo encontrado",
                    Data = viaje
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener viaje activo");
                return StatusCode(500, new ApiResponse<ViajeActivoDto>
                {
                    Success = false,
                    Message = "Error interno del servidor"
                });
            }
        }

        /// <summary>
        /// Finalizar un viaje en curso
        /// </summary>
        /// <param name="id">ID del viaje</param>
        /// <param name="finalizacion">Datos de finalización</param>
        /// <returns>Confirmación de finalización</returns>
        /// <response code="200">Viaje finalizado exitosamente</response>
        /// <response code="400">Error al finalizar viaje</response>
        [HttpPut("{id}/finalizar")]
        [ProducesResponseType(typeof(ApiResponse<bool>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<bool>), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ApiResponse<bool>>> FinalizarViaje(int id, [FromBody] FinalizarViajeDto finalizacion)
        {
            try
            {
                _logger.LogInformation($"Finalizando viaje: {id}");

                if (id <= 0)
                {
                    return BadRequest(new ApiResponse<bool>
                    {
                        Success = false,
                        Message = "ID de viaje inválido"
                    });
                }

                finalizacion.ViajeId = id;
                var resultado = await _viajeService.FinalizarViajeAsync(finalizacion);

                if (resultado)
                {
                    _logger.LogInformation($"Viaje finalizado exitosamente: {id}, Costo: ${finalizacion.CostoTotal}");

                    return Ok(new ApiResponse<bool>
                    {
                        Success = true,
                        Message = "Viaje finalizado exitosamente",
                        Data = true
                    });
                }

                return BadRequest(new ApiResponse<bool>
                {
                    Success = false,
                    Message = "Error al finalizar el viaje. El viaje no existe o ya está finalizado."
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error al finalizar viaje {id}");
                return StatusCode(500, new ApiResponse<bool>
                {
                    Success = false,
                    Message = "Error interno del servidor al finalizar viaje"
                });
            }
        }

        /// <summary>
        /// Calcular el costo de un viaje
        /// </summary>
        /// <param name="request">Tiempo y distancia del viaje</param>
        /// <returns>Costo calculado</returns>
        /// <response code="200">Costo calculado exitosamente</response>
        [HttpPost("calcular-costo")]
        [ProducesResponseType(typeof(ApiResponse<decimal>), StatusCodes.Status200OK)]
        public async Task<ActionResult<ApiResponse<decimal>>> CalcularCosto([FromBody] CalculoCostoRequest request)
        {
            try
            {
                _logger.LogInformation($"Calculando costo - Tiempo: {request.TiempoMinutos} min, Distancia: {request.DistanciaKm} km");

                if (request.TiempoMinutos < 0 || request.DistanciaKm < 0)
                {
                    return BadRequest(new ApiResponse<decimal>
                    {
                        Success = false,
                        Message = "Tiempo y distancia deben ser valores positivos"
                    });
                }

                var costo = await _viajeService.CalcularCostoViajeAsync(
                    request.TiempoMinutos,
                    request.DistanciaKm);

                _logger.LogInformation($"Costo calculado: ${costo}");

                return Ok(new ApiResponse<decimal>
                {
                    Success = true,
                    Message = "Costo calculado exitosamente",
                    Data = costo
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al calcular costo");
                return StatusCode(500, new ApiResponse<decimal>
                {
                    Success = false,
                    Message = "Error interno del servidor al calcular costo"
                });
            }
        }

        /// <summary>
        /// Obtener parámetros de tarifa del sistema
        /// </summary>
        /// <returns>Tarifas configuradas</returns>
        /// <response code="200">Parámetros obtenidos exitosamente</response>
        [HttpGet("parametros")]
        [ProducesResponseType(typeof(ApiResponse<ParametrosTarifaDto>), StatusCodes.Status200OK)]
        public async Task<ActionResult<ApiResponse<ParametrosTarifaDto>>> ObtenerParametros()
        {
            try
            {
                _logger.LogInformation("Obteniendo parámetros de tarifa");

                var parametros = await _viajeService.ObtenerParametrosTarifaAsync();

                return Ok(new ApiResponse<ParametrosTarifaDto>
                {
                    Success = true,
                    Message = "Parámetros obtenidos exitosamente",
                    Data = parametros
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener parámetros");
                return StatusCode(500, new ApiResponse<ParametrosTarifaDto>
                {
                    Success = false,
                    Message = "Error interno del servidor al obtener parámetros"
                });
            }
        }

        /// <summary>
        /// Calcular ruta entre origen y destino
        /// </summary>
        /// <param name="request">Ubicaciones de origen y destino</param>
        /// <returns>Ruta calculada con distancia y tiempo</returns>
        /// <response code="200">Ruta calculada exitosamente</response>
        [HttpPost("calcular-ruta")]
        [ProducesResponseType(typeof(ApiResponse<RutaDto>), StatusCodes.Status200OK)]
        public async Task<ActionResult<ApiResponse<RutaDto>>> CalcularRuta([FromBody] CalculoRutaRequest request)
        {
            try
            {
                _logger.LogInformation("Calculando ruta");

                if (request.Origen == null || request.Destino == null)
                {
                    return BadRequest(new ApiResponse<RutaDto>
                    {
                        Success = false,
                        Message = "Origen y destino son requeridos"
                    });
                }

                var ruta = await _viajeService.CalcularRutaAsync(
                    request.Origen,
                    request.Destino);

                return Ok(new ApiResponse<RutaDto>
                {
                    Success = true,
                    Message = "Ruta calculada exitosamente",
                    Data = ruta
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al calcular ruta");
                return StatusCode(500, new ApiResponse<RutaDto>
                {
                    Success = false,
                    Message = "Error interno del servidor al calcular ruta"
                });
            }
        }
    }

    /// <summary>
    /// Request para cálculo de costo
    /// </summary>
    public class CalculoCostoRequest
    {
        public decimal TiempoMinutos { get; set; }
        public decimal DistanciaKm { get; set; }
    }

    /// <summary>
    /// Request para cálculo de ruta
    /// </summary>
    public class CalculoRutaRequest
    {
        public UbicacionDto Origen { get; set; } = new();
        public UbicacionDto Destino { get; set; } = new();
    }
}
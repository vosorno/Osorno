using Microsoft.AspNetCore.Mvc;
using SistemaTaxiMobil.Core.DTOs;
using SistemaTaxiMobil.Core.Interfaces;

namespace SistemaTaxiMobil.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PagosController : ControllerBase
    {
        private readonly IPagoService _pagoService;
        private readonly ILogger<PagosController> _logger;

        public PagosController(IPagoService pagoService, ILogger<PagosController> logger)
        {
            _pagoService = pagoService;
            _logger = logger;
        }

        /// <summary>
        /// Verificar si el pasajero tiene tarjeta guardada
        /// </summary>
        [HttpGet("tiene-tarjeta/{pasajeroId}")]
        public async Task<ActionResult<ApiResponse<bool>>> TieneTarjetaGuardada(int pasajeroId)
        {
            try
            {
                var tieneTarjeta = await _pagoService.TieneTarjetaGuardadaAsync(pasajeroId);

                return Ok(new ApiResponse<bool>
                {
                    Success = true,
                    Data = tieneTarjeta
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al verificar tarjeta");
                return StatusCode(500, new ApiResponse<bool>
                {
                    Success = false,
                    Message = "Error interno del servidor"
                });
            }
        }

        /// <summary>
        /// Guardar tarjeta del pasajero
        /// </summary>
        [HttpPost("guardar-tarjeta")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ApiResponse<string>>> GuardarTarjeta([FromBody] GuardarTarjetaRequest request)
        {
            try
            {
                _logger.LogInformation($"Guardando tarjeta para pasajero {request.PasajeroId}");

                var token = await _pagoService.GuardarTarjetaAsync(request.PasajeroId, request.Tarjeta);

                if (!string.IsNullOrEmpty(token))
                {
                    return Ok(new ApiResponse<string>
                    {
                        Success = true,
                        Message = "Tarjeta guardada exitosamente",
                        Data = token
                    });
                }

                return BadRequest(new ApiResponse<string>
                {
                    Success = false,
                    Message = "Error al guardar la tarjeta"
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al guardar tarjeta");
                return StatusCode(500, new ApiResponse<string>
                {
                    Success = false,
                    Message = "Error interno del servidor"
                });
            }
        }

        /// <summary>
        /// Obtener métodos de pago disponibles
        /// </summary>
        [HttpGet("metodos")]
        public async Task<ActionResult<ApiResponse<List<MetodoPagoDto>>>> ObtenerMetodosPago()
        {
            try
            {
                var metodos = await _pagoService.ObtenerMetodosPagoAsync();

                return Ok(new ApiResponse<List<MetodoPagoDto>>
                {
                    Success = true,
                    Data = metodos
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener métodos de pago");
                return StatusCode(500, new ApiResponse<List<MetodoPagoDto>>
                {
                    Success = false,
                    Message = "Error interno del servidor"
                });
            }
        }

        /// <summary>
        /// Generar token de MercadoPago
        /// </summary>
        [HttpPost("generar-token")]
        public async Task<ActionResult<ApiResponse<string>>> GenerarTokenMercadoPago([FromBody] TarjetaDto tarjeta)
        {
            try
            {
                var token = await _pagoService.GenerarTokenMercadoPagoAsync(tarjeta);

                if (!string.IsNullOrEmpty(token))
                {
                    return Ok(new ApiResponse<string>
                    {
                        Success = true,
                        Data = token
                    });
                }

                return BadRequest(new ApiResponse<string>
                {
                    Success = false,
                    Message = "Error al generar token"
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al generar token MercadoPago");
                return StatusCode(500, new ApiResponse<string>
                {
                    Success = false,
                    Message = "Error interno del servidor"
                });
            }
        }
    }

    // Clase auxiliar
    public class GuardarTarjetaRequest
    {
        public int PasajeroId { get; set; }
        public TarjetaDto Tarjeta { get; set; } = new();
    }
}
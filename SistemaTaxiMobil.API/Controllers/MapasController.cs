using Microsoft.AspNetCore.Mvc;
using SistemaTaxiMobil.Core.DTOs;
using SistemaTaxiMobil.Core.Interfaces;

namespace SistemaTaxiMobil.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MapasController : ControllerBase
    {
        private readonly IMapService _mapService;
        private readonly ILogger<MapasController> _logger;

        public MapasController(IMapService mapService, ILogger<MapasController> logger)
        {
            _mapService = mapService;
            _logger = logger;
        }

        /// <summary>
        /// Obtener ruta entre dos puntos
        /// </summary>
        [HttpPost("obtener-ruta")]
        public async Task<ActionResult<ApiResponse<RutaDto>>> ObtenerRuta([FromBody] ObtenerRutaRequest request)
        {
            try
            {
                var ruta = await _mapService.ObtenerRutaAsync(
                    request.OrigenLat,
                    request.OrigenLng,
                    request.DestinoLat,
                    request.DestinoLng);

                return Ok(new ApiResponse<RutaDto>
                {
                    Success = true,
                    Data = ruta
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener ruta");
                return StatusCode(500, new ApiResponse<RutaDto>
                {
                    Success = false,
                    Message = "Error interno del servidor"
                });
            }
        }

        /// <summary>
        /// Obtener dirección desde coordenadas (Geocodificación inversa)
        /// </summary>
        [HttpGet("obtener-direccion")]
        public async Task<ActionResult<ApiResponse<string>>> ObtenerDireccion(
            [FromQuery] double latitud,
            [FromQuery] double longitud)
        {
            try
            {
                var direccion = await _mapService.ObtenerDireccionAsync(latitud, longitud);

                return Ok(new ApiResponse<string>
                {
                    Success = true,
                    Data = direccion
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener dirección");
                return StatusCode(500, new ApiResponse<string>
                {
                    Success = false,
                    Message = "Error interno del servidor"
                });
            }
        }

        /// <summary>
        /// Buscar coordenadas desde dirección (Geocodificación)
        /// </summary>
        [HttpGet("buscar-direccion")]
        public async Task<ActionResult<ApiResponse<UbicacionDto>>> BuscarDireccion([FromQuery] string direccion)
        {
            try
            {
                var ubicacion = await _mapService.BuscarDireccionAsync(direccion);

                return Ok(new ApiResponse<UbicacionDto>
                {
                    Success = true,
                    Data = ubicacion
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al buscar dirección");
                return StatusCode(500, new ApiResponse<UbicacionDto>
                {
                    Success = false,
                    Message = "Error interno del servidor"
                });
            }
        }
    }

    // Clase auxiliar
    public class ObtenerRutaRequest
    {
        public double OrigenLat { get; set; }
        public double OrigenLng { get; set; }
        public double DestinoLat { get; set; }
        public double DestinoLng { get; set; }
    }
}
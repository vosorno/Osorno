using Microsoft.AspNetCore.Mvc;

namespace SistemaTaxiMobil.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HealthController : ControllerBase
    {
        /// <summary>
        /// Verificar estado de la API
        /// </summary>
        [HttpGet]
        public ActionResult<ApiResponse<HealthStatus>> Get()
        {
            return Ok(new ApiResponse<HealthStatus>
            {
                Success = true,
                Message = "API funcionando correctamente",
                Data = new HealthStatus
                {
                    Status = "Healthy",
                    Version = "1.0.0",
                    ServerTime = DateTime.Now
                }
            });
        }

        /// <summary>
        /// Ping simple
        /// </summary>
        [HttpGet("ping")]
        public ActionResult<string> Ping()
        {
            return Ok("pong");
        }
    }

    public class HealthStatus
    {
        public string Status { get; set; } = string.Empty;
        public string Version { get; set; } = string.Empty;
        public DateTime ServerTime { get; set; }
    }
}
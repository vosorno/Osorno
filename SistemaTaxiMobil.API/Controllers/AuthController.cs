using Microsoft.AspNetCore.Mvc;
using SistemaTaxiMobil.Core.DTOs;
using SistemaTaxiMobil.Core.Interfaces;

namespace SistemaTaxiMobil.API.Controllers
{
    /// <summary>
    /// Controlador de autenticación y registro de usuarios
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly ILogger<AuthController> _logger;

        public AuthController(IAuthService authService, ILogger<AuthController> logger)
        {
            _authService = authService;
            _logger = logger;
        }

        /// <summary>
        /// Iniciar sesión en el sistema
        /// </summary>
        /// <param name="loginDto">Credenciales de usuario</param>
        /// <returns>Datos del usuario autenticado</returns>
        /// <response code="200">Login exitoso</response>
        /// <response code="401">Credenciales inválidas</response>
        [HttpPost("login")]
        [ProducesResponseType(typeof(ApiResponse<UsuarioAutenticadoDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<UsuarioAutenticadoDto>), StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<ApiResponse<UsuarioAutenticadoDto>>> Login([FromBody] LoginDto loginDto)
        {
            try
            {
                _logger.LogInformation($"Intento de login: Usuario={loginDto.Usuario}, Rol={loginDto.RolUsuarioId}");

                // Validar datos de entrada
                if (string.IsNullOrWhiteSpace(loginDto.Usuario) || string.IsNullOrWhiteSpace(loginDto.Password))
                {
                    return BadRequest(new ApiResponse<UsuarioAutenticadoDto>
                    {
                        Success = false,
                        Message = "Usuario y contraseña son requeridos"
                    });
                }

                var usuario = await _authService.LoginAsync(loginDto);

                if (usuario == null)
                {
                    _logger.LogWarning($"Login fallido para usuario: {loginDto.Usuario}");

                    return Unauthorized(new ApiResponse<UsuarioAutenticadoDto>
                    {
                        Success = false,
                        Message = loginDto.RolUsuarioId == 2
                            ? "No existe un conductor con la información registrada"
                            : "Usuario o contraseña incorrectos"
                    });
                }

                _logger.LogInformation($"Login exitoso: UsuarioId={usuario.UsuarioId}, Rol={usuario.NombreRol}");

                return Ok(new ApiResponse<UsuarioAutenticadoDto>
                {
                    Success = true,
                    Message = "Login exitoso",
                    Data = usuario
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error en el proceso de login");
                return StatusCode(500, new ApiResponse<UsuarioAutenticadoDto>
                {
                    Success = false,
                    Message = "Error interno del servidor al procesar login"
                });
            }
        }

        /// <summary>
        /// Registrar nuevo usuario en el sistema
        /// </summary>
        /// <param name="registroDto">Datos del nuevo usuario</param>
        /// <returns>Confirmación de registro</returns>
        /// <response code="200">Usuario registrado exitosamente</response>
        /// <response code="400">Error en validación o usuario ya existe</response>
        [HttpPost("register")]
        [ProducesResponseType(typeof(ApiResponse<bool>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<bool>), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ApiResponse<bool>>> Register([FromBody] RegistroUsuarioDto registroDto)
        {
            try
            {
                _logger.LogInformation($"Intento de registro: Usuario={registroDto.Usuario}");

                // Validaciones
                if (string.IsNullOrWhiteSpace(registroDto.Usuario) || string.IsNullOrWhiteSpace(registroDto.Password))
                {
                    return BadRequest(new ApiResponse<bool>
                    {
                        Success = false,
                        Message = "Usuario y contraseña son requeridos"
                    });
                }

                if (registroDto.Password.Length < 6)
                {
                    return BadRequest(new ApiResponse<bool>
                    {
                        Success = false,
                        Message = "La contraseña debe tener al menos 6 caracteres"
                    });
                }

                // Verificar si el usuario ya existe
                if (await _authService.UsuarioExisteAsync(registroDto.Usuario))
                {
                    _logger.LogWarning($"Intento de registro con usuario existente: {registroDto.Usuario}");

                    return BadRequest(new ApiResponse<bool>
                    {
                        Success = false,
                        Message = "El usuario ya existe"
                    });
                }

                var resultado = await _authService.RegistrarUsuarioAsync(registroDto);

                if (resultado)
                {
                    _logger.LogInformation($"Usuario registrado exitosamente: {registroDto.Usuario}");

                    return Ok(new ApiResponse<bool>
                    {
                        Success = true,
                        Message = "Usuario registrado exitosamente",
                        Data = true
                    });
                }

                return BadRequest(new ApiResponse<bool>
                {
                    Success = false,
                    Message = "Error al registrar usuario"
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error en el proceso de registro");
                return StatusCode(500, new ApiResponse<bool>
                {
                    Success = false,
                    Message = "Error interno del servidor al procesar registro"
                });
            }
        }

        /// <summary>
        /// Registrar información de pasajero
        /// </summary>
        /// <param name="pasajeroDto">Datos del pasajero</param>
        /// <returns>Confirmación de registro</returns>
        /// <response code="200">Pasajero registrado exitosamente</response>
        /// <response code="400">Error en validación o correo ya existe</response>
        [HttpPost("register-pasajero")]
        [ProducesResponseType(typeof(ApiResponse<bool>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<bool>), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ApiResponse<bool>>> RegisterPasajero([FromBody] RegistroPasajeroDto pasajeroDto)
        {
            try
            {
                _logger.LogInformation($"Registro de pasajero: Correo={pasajeroDto.Correo}");

                // Validaciones
                if (string.IsNullOrWhiteSpace(pasajeroDto.Nombre) ||
                    string.IsNullOrWhiteSpace(pasajeroDto.ApPaterno) ||
                    string.IsNullOrWhiteSpace(pasajeroDto.ApMaterno) ||
                    string.IsNullOrWhiteSpace(pasajeroDto.Correo))
                {
                    return BadRequest(new ApiResponse<bool>
                    {
                        Success = false,
                        Message = "Todos los campos son requeridos"
                    });
                }

                // Validar formato de correo básico
                if (!pasajeroDto.Correo.Contains("@") || !pasajeroDto.Correo.Contains("."))
                {
                    return BadRequest(new ApiResponse<bool>
                    {
                        Success = false,
                        Message = "Formato de correo inválido"
                    });
                }

                // Verificar si el correo ya existe
                if (await _authService.CorreoExisteAsync(pasajeroDto.Correo))
                {
                    _logger.LogWarning($"Intento de registro con correo existente: {pasajeroDto.Correo}");

                    return BadRequest(new ApiResponse<bool>
                    {
                        Success = false,
                        Message = "El correo ya está registrado"
                    });
                }

                var resultado = await _authService.RegistrarPasajeroAsync(pasajeroDto);

                if (resultado)
                {
                    _logger.LogInformation($"Pasajero registrado exitosamente: {pasajeroDto.Correo}");

                    return Ok(new ApiResponse<bool>
                    {
                        Success = true,
                        Message = "Pasajero registrado exitosamente",
                        Data = true
                    });
                }

                return BadRequest(new ApiResponse<bool>
                {
                    Success = false,
                    Message = "Error al registrar pasajero"
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error en el registro de pasajero");
                return StatusCode(500, new ApiResponse<bool>
                {
                    Success = false,
                    Message = "Error interno del servidor al procesar registro de pasajero"
                });
            }
        }

        /// <summary>
        /// Verificar si un usuario existe en el sistema
        /// </summary>
        /// <param name="usuario">Nombre de usuario a verificar</param>
        /// <returns>True si existe, False si no</returns>
        [HttpGet("usuario-existe/{usuario}")]
        [ProducesResponseType(typeof(ApiResponse<bool>), StatusCodes.Status200OK)]
        public async Task<ActionResult<ApiResponse<bool>>> UsuarioExiste(string usuario)
        {
            try
            {
                var existe = await _authService.UsuarioExisteAsync(usuario);

                return Ok(new ApiResponse<bool>
                {
                    Success = true,
                    Data = existe,
                    Message = existe ? "El usuario existe" : "El usuario no existe"
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al verificar existencia de usuario");
                return StatusCode(500, new ApiResponse<bool>
                {
                    Success = false,
                    Message = "Error interno del servidor"
                });
            }
        }

        /// <summary>
        /// Verificar si un correo existe en el sistema
        /// </summary>
        /// <param name="correo">Correo electrónico a verificar</param>
        /// <returns>True si existe, False si no</returns>
        [HttpGet("correo-existe/{correo}")]
        [ProducesResponseType(typeof(ApiResponse<bool>), StatusCodes.Status200OK)]
        public async Task<ActionResult<ApiResponse<bool>>> CorreoExiste(string correo)
        {
            try
            {
                var existe = await _authService.CorreoExisteAsync(correo);

                return Ok(new ApiResponse<bool>
                {
                    Success = true,
                    Data = existe,
                    Message = existe ? "El correo existe" : "El correo no existe"
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al verificar existencia de correo");
                return StatusCode(500, new ApiResponse<bool>
                {
                    Success = false,
                    Message = "Error interno del servidor"
                });
            }
        }
    }

    /// <summary>
    /// Modelo de respuesta estándar de la API
    /// </summary>
    public class ApiResponse<T>
    {
        public bool Success { get; set; }
        public string Message { get; set; } = string.Empty;
        public T? Data { get; set; }
        public DateTime Timestamp { get; set; } = DateTime.Now;
    }
}
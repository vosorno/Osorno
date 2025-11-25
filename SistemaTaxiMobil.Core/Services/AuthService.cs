using Microsoft.EntityFrameworkCore;
using SistemaTaxiMobil.Core.DTOs;
using SistemaTaxiMobil.Core.Entities;
using SistemaTaxiMobil.Core.Interfaces;

namespace SistemaTaxiMobil.Core.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUnitOfWork _unitOfWork;

        public AuthService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<UsuarioAutenticadoDto?> LoginAsync(LoginDto loginDto)
        {
            var usuario = await _unitOfWork.Usuarios
                .FirstOrDefaultAsync(u => u.Usuario1 == loginDto.Usuario &&
                                         u.Password == loginDto.Password &&
                                         u.RolUsuarioId == loginDto.RolUsuarioId);

            if (usuario == null)
                return null;

            var usuarioAutenticado = new UsuarioAutenticadoDto
            {
                UsuarioId = usuario.UsuarioId,
                RolUsuarioId = usuario.RolUsuarioId ?? 0
            };

            if (loginDto.RolUsuarioId == 1)
            {
                var pasajero = await _unitOfWork.Pasajeros
                    .FirstOrDefaultAsync(p => p.UsuarioId == usuario.UsuarioId);

                if (pasajero != null)
                {
                    usuarioAutenticado.PasajeroId = pasajero.PasajeroId;
                    usuarioAutenticado.NombreCompleto = $"{pasajero.Nombre} {pasajero.ApPaterno} {pasajero.ApMaterno}";
                    usuarioAutenticado.Correo = pasajero.Correo;
                    usuarioAutenticado.NombreRol = "Pasajero";
                }
            }
            else if (loginDto.RolUsuarioId == 2)
            {
                var conductor = await _unitOfWork.Conductores
                    .FirstOrDefaultAsync(c => c.UsuarioId == usuario.UsuarioId);

                if (conductor != null)
                {
                    usuarioAutenticado.ConductorId = conductor.ConductorId;
                    usuarioAutenticado.NombreCompleto = $"{conductor.Nombre} {conductor.ApPaterno} {conductor.ApMaterno}";
                    usuarioAutenticado.NombreRol = "Conductor";
                }
            }

            return usuarioAutenticado;
        }

        public async Task<bool> RegistrarUsuarioAsync(RegistroUsuarioDto registroDto)
        {
            try
            {
                if (await UsuarioExisteAsync(registroDto.Usuario))
                    return false;

                var nuevoUsuario = new Usuario
                {
                    Usuario1 = registroDto.Usuario,
                    Password = registroDto.Password,
                    RolUsuarioId = registroDto.RolUsuarioId
                };

                await _unitOfWork.Usuarios.AddAsync(nuevoUsuario);
                await _unitOfWork.SaveChangesAsync();

                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> RegistrarPasajeroAsync(RegistroPasajeroDto pasajeroDto)
        {
            try
            {
                if (await CorreoExisteAsync(pasajeroDto.Correo))
                    return false;

                var nuevoPasajero = new Pasajero
                {
                    UsuarioId = pasajeroDto.UsuarioId,
                    RolUsuarioId = 1,
                    Telefono = pasajeroDto.Telefono,
                    Nombre = pasajeroDto.Nombre,
                    ApPaterno = pasajeroDto.ApPaterno,
                    ApMaterno = pasajeroDto.ApMaterno,
                    Correo = pasajeroDto.Correo,
                    Contrasena = pasajeroDto.Contrasena,
                    Activo = true
                };

                await _unitOfWork.Pasajeros.AddAsync(nuevoPasajero);
                await _unitOfWork.SaveChangesAsync();

                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> UsuarioExisteAsync(string usuario)
        {
            return await _unitOfWork.Usuarios
                .AnyAsync(u => u.Usuario1 == usuario);
        }

        public async Task<bool> CorreoExisteAsync(string correo)
        {
            return await _unitOfWork.Pasajeros
                .AnyAsync(p => p.Correo == correo);
        }
    }
}
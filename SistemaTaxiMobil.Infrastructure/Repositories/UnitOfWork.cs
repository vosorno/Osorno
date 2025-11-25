using SistemaTaxiMobil.Core.Entities;
using SistemaTaxiMobil.Core.Interfaces;
using SistemaTaxiMobil.Infrastructure.Data;

namespace SistemaTaxiMobil.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        private IRepository<Usuario>? _usuarios;
        private IRepository<RolUsuario>? _rolUsuarios;
        private IRepository<Pasajero>? _pasajeros;
        private IRepository<Conductor>? _conductores;
        private IRepository<Viaje>? _viajes;
        private IRepository<MetodoPago>? _metodoPagos;
        private IRepository<EstadoViaje>? _estadoViajes;
        private IRepository<DatosTarjeta>? _datosTarjetas;
        private IRepository<Parametros>? _parametros;

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
        }

        public IRepository<Usuario> Usuarios =>
            _usuarios ??= new Repository<Usuario>(_context);

        public IRepository<RolUsuario> RolUsuarios =>
            _rolUsuarios ??= new Repository<RolUsuario>(_context);

        public IRepository<Pasajero> Pasajeros =>
            _pasajeros ??= new Repository<Pasajero>(_context);

        public IRepository<Conductor> Conductores =>
            _conductores ??= new Repository<Conductor>(_context);

        public IRepository<Viaje> Viajes =>
            _viajes ??= new Repository<Viaje>(_context);

        public IRepository<MetodoPago> MetodoPagos =>
            _metodoPagos ??= new Repository<MetodoPago>(_context);

        public IRepository<EstadoViaje> EstadoViajes =>
            _estadoViajes ??= new Repository<EstadoViaje>(_context);

        public IRepository<DatosTarjeta> DatosTarjetas =>
            _datosTarjetas ??= new Repository<DatosTarjeta>(_context);

        public IRepository<Parametros> Parametros =>
            _parametros ??= new Repository<Parametros>(_context);

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
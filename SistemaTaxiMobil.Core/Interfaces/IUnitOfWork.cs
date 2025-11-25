using SistemaTaxiMobil.Core.Entities;

namespace SistemaTaxiMobil.Core.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<Usuario> Usuarios { get; }
        IRepository<RolUsuario> RolUsuarios { get; }
        IRepository<Pasajero> Pasajeros { get; }
        IRepository<Conductor> Conductores { get; }
        IRepository<Viaje> Viajes { get; }
        IRepository<MetodoPago> MetodoPagos { get; }
        IRepository<EstadoViaje> EstadoViajes { get; }
        IRepository<DatosTarjeta> DatosTarjetas { get; }
        IRepository<Parametros> Parametros { get; }

        Task<int> SaveChangesAsync();
    }
}
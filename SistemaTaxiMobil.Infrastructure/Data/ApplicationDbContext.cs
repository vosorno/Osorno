using Microsoft.EntityFrameworkCore;
using SistemaTaxiMobil.Core.Entities;

namespace SistemaTaxiMobil.Infrastructure.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // DbSets - Tablas de la base de datos
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<RolUsuario> RolUsuarios { get; set; }
        public DbSet<Pasajero> Pasajeros { get; set; }
        public DbSet<Conductor> Conductores { get; set; }
        public DbSet<BaseTaxi> BaseTaxis { get; set; }
        public DbSet<Localidad> Localidades { get; set; }
        public DbSet<EstatusConductor> EstatusConductores { get; set; }
        public DbSet<Turno> Turnos { get; set; }
        public DbSet<ConductorTurno> ConductorTurnos { get; set; }
        public DbSet<Viaje> Viajes { get; set; }
        public DbSet<EstadoViaje> EstadoViajes { get; set; }
        public DbSet<CancelaViaje> CancelaViajes { get; set; }
        public DbSet<RechazoViaje> RechazoViajes { get; set; }
        public DbSet<MetodoPago> MetodoPagos { get; set; }
        public DbSet<DatosTarjeta> DatosTarjetas { get; set; }
        public DbSet<TarjetasPago> TarjetasPagos { get; set; }
        public DbSet<Parametros> Parametros { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // ========== Configuración de Usuario ==========
            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.ToTable("Usuario");
                entity.HasKey(e => e.UsuarioId);
                entity.Property(e => e.Usuario1).HasColumnName("Usuario").HasMaxLength(20).IsRequired();
                entity.Property(e => e.Password).HasMaxLength(20).IsRequired();

                entity.HasOne(d => d.RolUsuario)
                    .WithMany(p => p.Usuarios)
                    .HasForeignKey(d => d.RolUsuarioId)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            // ========== Configuración de RolUsuario ==========
            modelBuilder.Entity<RolUsuario>(entity =>
            {
                entity.ToTable("RolUsuario");
                entity.HasKey(e => e.RolUsuarioId);
                entity.Property(e => e.Nombre).HasMaxLength(50).IsRequired();
            });

            // ========== Configuración de Pasajero ==========
            modelBuilder.Entity<Pasajero>(entity =>
            {
                entity.ToTable("Pasajero");
                entity.HasKey(e => e.PasajeroId);
                entity.Property(e => e.Telefono).HasMaxLength(20);
                entity.Property(e => e.Nombre).HasMaxLength(100).IsRequired();
                entity.Property(e => e.ApPaterno).HasMaxLength(100).IsRequired();
                entity.Property(e => e.ApMaterno).HasMaxLength(100).IsRequired();
                entity.Property(e => e.Correo).HasMaxLength(100).IsRequired();
                entity.Property(e => e.Contrasena).HasMaxLength(255).IsRequired();
                entity.Property(e => e.Activo).HasDefaultValue(true);

                entity.HasIndex(e => e.Correo).IsUnique();

                entity.HasOne(d => d.Usuario)
                    .WithMany(p => p.Pasajeros)
                    .HasForeignKey(d => d.UsuarioId)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.RolUsuario)
                    .WithMany(p => p.Pasajeros)
                    .HasForeignKey(d => d.RolUsuarioId)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            // ========== Configuración de Conductor ==========
            modelBuilder.Entity<Conductor>(entity =>
            {
                entity.ToTable("Conductor");
                entity.HasKey(e => e.ConductorId);
                entity.Property(e => e.Nombre).HasMaxLength(100).IsRequired();
                entity.Property(e => e.ApPaterno).HasMaxLength(100).IsRequired();
                entity.Property(e => e.ApMaterno).HasMaxLength(100).IsRequired();
                entity.Property(e => e.Telefono).HasMaxLength(20);
                entity.Property(e => e.DescripcionVehiculo).HasMaxLength(100).IsRequired();
                entity.Property(e => e.Placas).HasMaxLength(5).IsRequired();
                entity.Property(e => e.Disponible).HasDefaultValue(false);

                entity.HasOne(d => d.Usuario)
                    .WithMany(p => p.Conductores)
                    .HasForeignKey(d => d.UsuarioId)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.BaseTaxi)
                    .WithMany(p => p.Conductores)
                    .HasForeignKey(d => d.BaseTaxiId)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.Localidad)
                    .WithMany(p => p.Conductores)
                    .HasForeignKey(d => d.LocalidadId)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.EstatusConductor)
                    .WithMany(p => p.Conductores)
                    .HasForeignKey(d => d.EstatusConductorId)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            // ========== Configuración de BaseTaxi ==========
            modelBuilder.Entity<BaseTaxi>(entity =>
            {
                entity.ToTable("BaseTaxi");
                entity.HasKey(e => e.BaseTaxiId);
                entity.Property(e => e.Nombre).HasMaxLength(50).IsRequired();
            });

            // ========== Configuración de Localidad ==========
            modelBuilder.Entity<Localidad>(entity =>
            {
                entity.ToTable("Localidad");
                entity.HasKey(e => e.LocalidadId);
                entity.Property(e => e.Nombre).HasMaxLength(50).IsRequired();
            });

            // ========== Configuración de EstatusConductor ==========
            modelBuilder.Entity<EstatusConductor>(entity =>
            {
                entity.ToTable("EstatusConductor");
                entity.HasKey(e => e.EstatusConductorId);
                entity.Property(e => e.Nombre).HasMaxLength(50).IsRequired();
            });

            // ========== Configuración de Turno ==========
            modelBuilder.Entity<Turno>(entity =>
            {
                entity.ToTable("Turno");
                entity.HasKey(e => e.TurnoId);
                entity.Property(e => e.Nombre).HasMaxLength(50).IsRequired();
            });

            // ========== Configuración de ConductorTurno ==========
            modelBuilder.Entity<ConductorTurno>(entity =>
            {
                entity.ToTable("ConductorTurno");
                entity.HasKey(e => e.ConductorTurnoId);

                entity.HasOne(d => d.Conductor)
                    .WithMany(p => p.ConductorTurnos)
                    .HasForeignKey(d => d.ConductorId)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.Turno)
                    .WithMany(p => p.ConductorTurnos)
                    .HasForeignKey(d => d.TurnoId)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            // ========== Configuración de Viaje ==========
            modelBuilder.Entity<Viaje>(entity =>
            {
                entity.ToTable("Viaje");
                entity.HasKey(e => e.ViajeId);
                entity.Property(e => e.PuntoOrigen).IsRequired();
                entity.Property(e => e.PuntoDestino).IsRequired();
                entity.Property(e => e.DuracionMinutosEstimado).HasColumnType("decimal(10, 2)");
                entity.Property(e => e.CostoTotal).HasColumnType("decimal(10, 2)");
                entity.Property(e => e.FechaInicio).HasDefaultValueSql("getdate()");
                entity.Property(e => e.FormaPago).HasMaxLength(50).IsRequired();

                entity.HasOne(d => d.Pasajero)
                    .WithMany(p => p.Viajes)
                    .HasForeignKey(d => d.PasajeroId)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.Conductor)
                    .WithMany(p => p.Viajes)
                    .HasForeignKey(d => d.ConductorId)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.LocalidadOrigen)
                    .WithMany(p => p.ViajesOrigen)
                    .HasForeignKey(d => d.LocalidadOrigenId)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.LocalidadDestino)
                    .WithMany(p => p.ViajesDestino)
                    .HasForeignKey(d => d.LocalidadDestinoId)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.MetodoPago)
                    .WithMany(p => p.Viajes)
                    .HasForeignKey(d => d.MetodoPagoId)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.EstadoViaje)
                    .WithMany(p => p.Viajes)
                    .HasForeignKey(d => d.EstadoViajeId)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            // ========== Configuración de EstadoViaje ==========
            modelBuilder.Entity<EstadoViaje>(entity =>
            {
                entity.ToTable("EstadoViaje");
                entity.HasKey(e => e.EstadoViajeId);
                entity.Property(e => e.Nombre).HasMaxLength(50).IsRequired();
            });

            // ========== Configuración de CancelaViaje ==========
            modelBuilder.Entity<CancelaViaje>(entity =>
            {
                entity.ToTable("CancelaViaje");
                entity.HasKey(e => e.CancelaViajeId);
                entity.Property(e => e.DescripcionCancelacion).HasMaxLength(255).IsRequired();

                entity.HasOne(d => d.Viaje)
                    .WithMany(p => p.CancelaViajes)
                    .HasForeignKey(d => d.ViajeId)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            // ========== Configuración de RechazoViaje ==========
            modelBuilder.Entity<RechazoViaje>(entity =>
            {
                entity.ToTable("RechazoViaje");
                entity.HasKey(e => e.RechazoViajeId);

                entity.HasOne(d => d.Conductor)
                    .WithMany(p => p.RechazoViajes)
                    .HasForeignKey(d => d.ConductorId)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            // ========== Configuración de MetodoPago ==========
            modelBuilder.Entity<MetodoPago>(entity =>
            {
                entity.ToTable("MetodoPago");
                entity.HasKey(e => e.MetodoPagoId);
                entity.Property(e => e.Nombre).HasMaxLength(50).IsRequired();
            });

            // ========== Configuración de DatosTarjeta ==========
            modelBuilder.Entity<DatosTarjeta>(entity =>
            {
                entity.ToTable("DatosTarjeta");
                entity.HasKey(e => e.DatosTarjetaId);
                entity.Property(e => e.Token).HasMaxLength(256).IsRequired();

                entity.HasOne(d => d.Pasajero)
                    .WithMany(p => p.DatosTarjetas)
                    .HasForeignKey(d => d.PasajeroId)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            // ========== Configuración de TarjetasPago ==========
            modelBuilder.Entity<TarjetasPago>(entity =>
            {
                entity.ToTable("TarjetasPago");
                entity.HasKey(e => e.TarjetasPagoId);
                entity.Property(e => e.Token).HasMaxLength(256).IsRequired();

                entity.HasOne(d => d.DatosTarjeta)
                    .WithMany(p => p.TarjetasPagos)
                    .HasForeignKey(d => d.DatosTarjetaId)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            // ========== Configuración de Parametros ==========
            modelBuilder.Entity<Parametros>(entity =>
            {
                entity.ToTable("Parametros");
                entity.HasKey(e => e.ParametrosId);
                entity.Property(e => e.TarifaBase).HasColumnType("decimal(18, 0)");
                entity.Property(e => e.CostoMinuto).HasColumnType("decimal(18, 0)");
                entity.Property(e => e.CostoKilometro).HasColumnType("decimal(18, 0)");
            });

            // ========== Datos Semilla (Seed Data) ==========
            SeedData(modelBuilder);
        }

        private void SeedData(ModelBuilder modelBuilder)
        {
            // Roles de Usuario
            modelBuilder.Entity<RolUsuario>().HasData(
                new RolUsuario { RolUsuarioId = 1, Nombre = "Pasajero" },
                new RolUsuario { RolUsuarioId = 2, Nombre = "Conductor" }
            );

            // Estados de Viaje
            modelBuilder.Entity<EstadoViaje>().HasData(
                new EstadoViaje { EstadoViajeId = 1, Nombre = "Inactivo" },
                new EstadoViaje { EstadoViajeId = 2, Nombre = "Iniciado" },
                new EstadoViaje { EstadoViajeId = 3, Nombre = "Terminado" }
            );

            // Métodos de Pago
            modelBuilder.Entity<MetodoPago>().HasData(
                new MetodoPago { MetodoPagoId = 1, Nombre = "Efectivo" },
                new MetodoPago { MetodoPagoId = 2, Nombre = "Tarjeta" }
            );

            // Parámetros de Tarifas
            modelBuilder.Entity<Parametros>().HasData(
                new Parametros
                {
                    ParametrosId = 1,
                    TarifaBase = 50,
                    CostoMinuto = 2,
                    CostoKilometro = 10
                }
            );

            // Estatus Conductor
            modelBuilder.Entity<EstatusConductor>().HasData(
                new EstatusConductor { EstatusConductorId = 1, Nombre = "Activo" },
                new EstatusConductor { EstatusConductorId = 2, Nombre = "Inactivo" }
            );
        }
    }
}
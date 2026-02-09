using Microsoft.Extensions.Logging;
//using Microsoft.EntityFrameworkCore;
//using SistemaTaxiMobil.Infrastructure.Repositories;
using SistemaTaxiMobil.Views;
using SistemaTaxiMobil.ViewModels;
using SistemaTaxiMobil.Services;
using SistemaTaxiMobil.Core.Interfaces;
using Refit;

namespace SistemaTaxiMobil
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                //.UseMauiCommunityToolkit()
                .UseMauiMaps()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

            // Configurar DbContext
            /*
            var connectionString = "Server=DESKTOP-G8ER1PA\\BASESQLVOG;Database=SistemaTaxiMobil;Trusted_Connection=True;TrustServerCertificate=True;MultipleActiveResultSets=true";
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(connectionString,
                    sqlOptions => sqlOptions.UseNetTopologySuite()));
            */

            // Registrar UnitOfWork y Repositorios
            //builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

            // Registrar Servicios
            builder.Services.AddScoped<IAuthService, AuthService>();
            builder.Services.AddScoped<IPagoService, PagoService>();
            builder.Services.AddScoped<IViajeService, ViajeService>();
            builder.Services.AddScoped<IMapService, MapService>();

            // Registrar ViewModels
            builder.Services.AddTransient<SplashViewModel>();
            builder.Services.AddTransient<LoginViewModel>();
            builder.Services.AddTransient<RegistroUsuarioViewModel>();
            builder.Services.AddTransient<RegistroPasajeroViewModel>();
            builder.Services.AddTransient<TipoPagoViewModel>();
            builder.Services.AddTransient<RegistroTarjetaViewModel>();
            builder.Services.AddTransient<SolicitudViajeViewModel>();
            builder.Services.AddTransient<ViajeEnCursoViewModel>();
            builder.Services.AddTransient<ViajeCerradoViewModel>();

            // Registrar Views
            builder.Services.AddTransient<SplashPage>();
            builder.Services.AddTransient<LoginPage>();
            builder.Services.AddTransient<RegistroUsuarioPage>();
            builder.Services.AddTransient<RegistroPasajeroPage>();
            builder.Services.AddTransient<TipoPagoPage>();
            builder.Services.AddTransient<RegistroTarjetaPage>();
            builder.Services.AddTransient<SolicitudViajePage>();
            builder.Services.AddTransient<ViajeEnCursoPage>();
            builder.Services.AddTransient<ViajeCerradoPage>();
            builder.Services.AddRefitClient<ITaxiApi>()
                .ConfigureHttpClient(c =>
                {
                    c.BaseAddress = new Uri("https://localhost:5001");
                });

#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
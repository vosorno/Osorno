
using Microsoft.EntityFrameworkCore;
using SistemaTaxiMobil.Core.Interfaces;
using SistemaTaxiMobil.Core.Services;
using SistemaTaxiMobil.Infrastructure.Data;
using SistemaTaxiMobil.Infrastructure.Repositories;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

//// Configurar DbContext con SQL Server
//var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
//builder.Services.AddDbContext<ApplicationDbContext>(options =>
//    options.UseSqlServer(connectionString,
//        sqlOptions => sqlOptions.UseNetTopologySuite()));

// ========== CONFIGURAR DbContext ==========
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString,
    sqlOptions => sqlOptions.UseNetTopologySuite()));

// ========== REGISTRAR SERVICIOS ==========
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IPagoService, PagoService>();
builder.Services.AddScoped<IViajeService, ViajeService>();
builder.Services.AddScoped<IMapService, MapService>();

// ========== CONFIGURAR CORS ==========
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

// ========== AGREGAR CONTROLLERS ==========
builder.Services.AddControllers();

// ========== CONFIGURAR SWAGGER ==========
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Sistema Taxi Mobil API",
        Version = "v1",
        Description = "API REST para gestión de sistema de taxis",
        Contact = new OpenApiContact
        {
            Name = "Unión de Taxis",
            Email = "contacto@uniondetaxis.com"
        }
    });
});

var app = builder.Build();

// ========== CONFIGURAR PIPELINE HTTP ==========

// IMPORTANTE: Swagger debe estar ANTES de cualquier otra configuración
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Sistema Taxi Mobil API v1");
    c.RoutePrefix = string.Empty; // Swagger en la raíz (http://localhost:5000)
});

app.UseCors("AllowAll");

app.UseAuthorization();

app.MapControllers();

// ========== MENSAJE DE INICIO ==========
Console.WriteLine("╔════════════════════════════════════════════╗");
Console.WriteLine("║   🚕 Sistema Taxi Mobil API - Iniciada   ║");
Console.WriteLine("╚════════════════════════════════════════════╝");
Console.WriteLine($"📍 Base de datos: {connectionString?.Split(';')[0]}");
Console.WriteLine($"🌐 Swagger UI: http://localhost:5000");
Console.WriteLine($"📡 Endpoints: http://localhost:5000/api/");
Console.WriteLine("════════════════════════════════════════════");

app.Run();

//using Microsoft.EntityFrameworkCore;
//using SistemaTaxiMobil.Core.Interfaces;
//using SistemaTaxiMobil.Core.Services;
//using SistemaTaxiMobil.Infrastructure.Data;
//using SistemaTaxiMobil.Infrastructure.Repositories;

//var builder = WebApplication.CreateBuilder(args);

//// Configurar DbContext con SQL Server
//var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
//builder.Services.AddDbContext<ApplicationDbContext>(options =>
//    options.UseSqlServer(connectionString,
//        sqlOptions => sqlOptions.UseNetTopologySuite()));

//// Registrar UnitOfWork y Repositorios
//builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

//// Registrar Servicios de Negocio
//builder.Services.AddScoped<IAuthService, AuthService>();
//builder.Services.AddScoped<IPagoService, PagoService>();
//builder.Services.AddScoped<IViajeService, ViajeService>();
//builder.Services.AddScoped<IMapService, MapService>();

//// Configurar CORS (permitir acceso desde la app MAUI)
//builder.Services.AddCors(options =>
//{
//    options.AddPolicy("AllowAll", builder =>
//    {
//        builder.AllowAnyOrigin()
//               .AllowAnyMethod()
//               .AllowAnyHeader();
//    });
//});

//// Agregar Controllers
//builder.Services.AddControllers();

//// Configurar Swagger para documentación de API
//builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen(c =>
//{
//    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
//    {
//        Title = "Sistema Taxi Mobil API",
//        Version = "v1",
//        Description = "API REST para gestión de sistema de taxis",
//        Contact = new Microsoft.OpenApi.Models.OpenApiContact
//        {
//            Name = "Unión de Taxis",
//            Email = "contacto@uniondetaxis.com"
//        }
//    });
//});

//var app = builder.Build();

//// Configurar pipeline de HTTP request
//if (app.Environment.IsDevelopment())
//{
//    app.UseSwagger();
//    app.UseSwaggerUI(c =>
//    {
//        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Sistema Taxi Mobil API v1");
//        c.RoutePrefix = string.Empty; // Swagger en la raíz
//    });
//}

//// Usar CORS
//app.UseCors("AllowAll");

//// Usar HTTPS (descomenta en producción)
//// app.UseHttpsRedirection();

//app.UseAuthorization();

//app.MapControllers();

//// Mensaje de inicio
//Console.WriteLine("===========================================");
//Console.WriteLine("🚕 Sistema Taxi Mobil API");
//Console.WriteLine("===========================================");
//Console.WriteLine($"📍 Servidor: {builder.Configuration["ConnectionStrings:DefaultConnection"]?.Split(';')[0]}");
//Console.WriteLine($"🌐 Swagger UI: http://localhost:5000");
//Console.WriteLine("===========================================");

//app.Run();

//var builder = WebApplication.CreateBuilder(args);

//// Add services to the container.

//builder.Services.AddControllers();
//// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
//builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();

//var app = builder.Build();

//// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
//    app.UseSwagger();
//    app.UseSwaggerUI();
//}

//app.UseAuthorization();

//app.MapControllers();

//app.Run();

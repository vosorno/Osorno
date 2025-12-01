using SistemaTaxiMobil.Core.DTOs;
using SistemaTaxiMobil.Core.Entities;
using SistemaTaxiMobil.Core.Interfaces;

namespace SistemaTaxiMobil.Core.Services
{
    public class PagoService : IPagoService
    {
        private readonly IUnitOfWork _unitOfWork;
        private const string MERCADOPAGO_PUBLIC_KEY = "TEST-YOUR-PUBLIC-KEY";

        public PagoService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> TieneTarjetaGuardadaAsync(int pasajeroId)
        {
            return await _unitOfWork.DatosTarjetas
                .AnyAsync(dt => dt.PasajeroId == pasajeroId);
        }

        public async Task<string?> GuardarTarjetaAsync(int pasajeroId, TarjetaDto tarjeta)
        {
            try
            {
                var token = await GenerarTokenMercadoPagoAsync(tarjeta);

                if (string.IsNullOrEmpty(token))
                    return null;

                var datosTarjeta = new DatosTarjeta
                {
                    PasajeroId = pasajeroId,
                    Token = token
                };

                await _unitOfWork.DatosTarjetas.AddAsync(datosTarjeta);
                await _unitOfWork.SaveChangesAsync();

                return token;
            }
            catch
            {
                return null;
            }
        }

        public async Task<List<MetodoPagoDto>> ObtenerMetodosPagoAsync()
        {
            var metodos = await _unitOfWork.MetodoPagos.GetAllAsync();
            return metodos.Select(m => new MetodoPagoDto
            {
                MetodoPagoId = m.MetodoPagoId,
                Nombre = m.Nombre
            }).ToList();
        }

        public async Task<string?> GenerarTokenMercadoPagoAsync(TarjetaDto tarjeta)
        {
            try
            {
                var token = $"MP-TOKEN-{Guid.NewGuid():N}";
                await Task.Delay(500);
                return token;
            }
            catch
            {
                return null;
            }
        }
    }
}
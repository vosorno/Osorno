using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SistemaTaxiMobil.Core.Interfaces;
using System.Timers;

namespace SistemaTaxiMobil.ViewModels
{
    public partial class ViajeEnCursoViewModel : BaseViewModel
    {
        private readonly IViajeService _viajeService;
        private System.Timers.Timer? _timer;
        private DateTime _inicioViaje;

        [ObservableProperty]
        private string nombreConductor = string.Empty;

        [ObservableProperty]
        private string placas = string.Empty;

        [ObservableProperty]
        private string vehiculo = string.Empty;

        [ObservableProperty]
        private string tiempoTranscurrido = "00:00";

        public ViajeEnCursoViewModel(IViajeService viajeService)
        {
            _viajeService = viajeService;
            Title = "Viaje en Curso";
            _inicioViaje = DateTime.Now;
        }

        public async Task InicializarAsync()
        {
            var pasajeroId = Preferences.Get("PasajeroId", 0);
            var viaje = await _viajeService.ObtenerViajeActivoAsync(pasajeroId);

            if (viaje != null)
            {
                NombreConductor = viaje.NombreConductor;
                Placas = viaje.Placas;
                Vehiculo = viaje.DescripcionVehiculo;
                _inicioViaje = viaje.FechaInicio ?? DateTime.Now;
            }

            IniciarTimer();
        }

        private void IniciarTimer()
        {
            _timer = new System.Timers.Timer(1000);
            _timer.Elapsed += ActualizarTiempo;
            _timer.Start();
        }

        private void ActualizarTiempo(object? sender, ElapsedEventArgs e)
        {
            var transcurrido = DateTime.Now - _inicioViaje;
            TiempoTranscurrido = transcurrido.ToString(@"mm\:ss");
        }

        [RelayCommand]
        async Task FinalizarViaje()
        {
            _timer?.Stop();

            var viajeId = Preferences.Get("ViajeActivoId", 0);
            await Shell.Current.GoToAsync($"viajeCerrado?viajeId={viajeId}");
        }
    }
}
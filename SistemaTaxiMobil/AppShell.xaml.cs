using SistemaTaxiMobil.Views;

namespace SistemaTaxiMobil
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            // Registrar rutas
            Routing.RegisterRoute("login", typeof(LoginPage));
            Routing.RegisterRoute("registroUsuario", typeof(RegistroUsuarioPage));
            Routing.RegisterRoute("registroPasajero", typeof(RegistroPasajeroPage));
            Routing.RegisterRoute("tipoPago", typeof(TipoPagoPage));
            Routing.RegisterRoute("registroTarjeta", typeof(RegistroTarjetaPage));
            Routing.RegisterRoute("solicitudViaje", typeof(SolicitudViajePage));
            Routing.RegisterRoute("viajeEnCurso", typeof(ViajeEnCursoPage));
            Routing.RegisterRoute("viajeCerrado", typeof(ViajeCerradoPage));
        }
    }
}
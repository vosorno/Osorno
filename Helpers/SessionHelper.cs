namespace SistemaTaxiMobil.Helpers
{
    public static class SessionHelper
    {
        public static int GetUsuarioId()
        {
            return Preferences.Get("UsuarioId", 0);
        }

        public static int GetPasajeroId()
        {
            return Preferences.Get("PasajeroId", 0);
        }

        public static int GetRolUsuarioId()
        {
            return Preferences.Get("RolUsuarioId", 0);
        }

        public static int GetMetodoPagoId()
        {
            return Preferences.Get("MetodoPagoId", 1);
        }

        public static void ClearSession()
        {
            Preferences.Clear();
        }

        public static bool IsLoggedIn()
        {
            return GetUsuarioId() > 0;
        }
    }
}
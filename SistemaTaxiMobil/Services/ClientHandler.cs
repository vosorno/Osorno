namespace SistemaTaxiMobil.Services;

public static class ClientHandler
{
    public static HttpClientHandler CreateHandler()
    {
        HttpClientHandler handler = new ();
        handler.ServerCertificateCustomValidationCallback += (sender, cert, chain, sslPolicyErrors) => true;
        return handler;
    }
}

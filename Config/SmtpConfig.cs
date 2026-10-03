/*
    * Nombre: SmtpConfig.cs
    * Descripción: clases utilizadas para la estructura de configuración de un servidor SMTP y de un cliente SMTP. 
*/

namespace PokemonServer.Config;

public class SmtpServerConfig
{
    public string Host { get; set; } = string.Empty;
    public int Port { get; set; }
}

public class SmtpClientConfig
{
    public string Username { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public bool EnableSsl { get; set; }
}
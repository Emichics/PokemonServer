/*
    * Nombre: EmailService.cs
    * Descripción: manejador de envíos de correo electrónico.
    * Historial de cambios: 
        03/10/2026
        - Se agrega el método SendEmailAsync() para envío de archivo por correo electrónico . 
*/

using System.Net;
using System.Net.Mail;
using PokemonServer.Config;
using PokemonServer.Exceptions;
using PokemonServer.Constants;

public class EmailService
{
    private readonly SmtpServerConfig _server;
    private readonly SmtpClientConfig _client;

    public EmailService(IConfiguration configuration)
    {
        _server = configuration
            .GetSection("Smtp:Server")
            .Get<SmtpServerConfig>()!;

        _client = configuration
            .GetSection("Smtp:Client")
            .Get<SmtpClientConfig>()!;
    }

    public async Task SendEmailAsync(
        string recipient,
        string subject,
        string message,
        byte[] attachment,
        string attachmentName,
        string attachmentContentType)
    {
        try
        {
            using var mail = new MailMessage();

            mail.From = new MailAddress(_client.Username);
            mail.To.Add(recipient);
            mail.Subject = subject;
            mail.Body = message;
            mail.IsBodyHtml = false;

            using var attachmentStream = new MemoryStream(attachment);

            mail.Attachments.Add(
                new Attachment(
                    attachmentStream,
                    attachmentName,
                    attachmentContentType
                )
            );

            using var smtp = new SmtpClient(
                _server.Host,
                _server.Port
            );

            smtp.Credentials = new NetworkCredential(
                _client.Username,
                _client.Password
            );

            smtp.EnableSsl = _client.EnableSsl;

            await smtp.SendMailAsync(mail);
        }
        catch
        {
            throw new SendEmailException();
        }
    }
}

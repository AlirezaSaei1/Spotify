using System.Net;
using System.Net.Mail;

namespace Spotify.Services;

public class EmailService : IEmailService
{
    private readonly string _smtpHost;
    private readonly int _smtpPort;
    private readonly string _smtpUsername;
    private readonly string _smtpPassword;

    public EmailService(string host, int port, string username, string password)
    {
        _smtpHost = host;
        _smtpPort = port;
        _smtpUsername = username;
        _smtpPassword = password;
    }
    
    public Task SendEmailAsync(string email, string subject, string message)
    {
        var client = new SmtpClient
        {
            Port = _smtpPort,
            Host = _smtpHost,
            EnableSsl = true,
            DeliveryMethod = SmtpDeliveryMethod.Network,
            UseDefaultCredentials = false,
            Credentials = new NetworkCredential(_smtpUsername, _smtpPassword)
        };
        return client.SendMailAsync(_smtpUsername, email, subject, message);
    }
}
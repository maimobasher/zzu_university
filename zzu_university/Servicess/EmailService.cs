using System.Net.Mail;
using System.Net;
using zzu_university.Servicess;

public class EmailService : IEmailService
{
    private readonly string _smtpServer;
    private readonly int _smtpPort;
    private readonly string _smtpUsername;
    private readonly string _smtpPassword;

    public EmailService(IConfiguration configuration)
    {
        _smtpServer = configuration["Email:Smtp:Host"];
        _smtpPort = int.Parse(configuration["Email:Smtp:Port"]);
        _smtpUsername = configuration["Email:Smtp:Username"];
        _smtpPassword = configuration["Email:Smtp:Password"];
    }

    public async Task SendEmailAsync(string to, string subject, string body)
    {
        var client = new SmtpClient(_smtpServer)
        {
            Port = _smtpPort,
            Credentials = new NetworkCredential(_smtpUsername, _smtpPassword),
            EnableSsl = true
        };

        var mailMessage = new MailMessage
        {
            From = new MailAddress(_smtpUsername),
            Subject = subject,
            Body = body,
            IsBodyHtml = false,
        };

        mailMessage.To.Add(to);

        await client.SendMailAsync(mailMessage);
    }
}

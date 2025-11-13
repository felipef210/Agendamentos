
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using MimeKit.Text;

namespace AgendamentosApi.Services.Email;

public class EmailSender : IEmailSender
{
    private readonly IConfiguration _configuration;

    public EmailSender(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public async Task SendEmailAsync(string to, string subject, string htmlMessage)
    {
        var email = new MimeMessage();
        email.From.Add(MailboxAddress.Parse(_configuration["Mail:User"]));
        email.To.Add(MailboxAddress.Parse(to));
        email.Subject = subject;
        email.Body = new TextPart(TextFormat.Html) { Text = htmlMessage };

        using var smtp = new SmtpClient();
        await smtp.ConnectAsync(_configuration["Mail:Host"], int.Parse(_configuration["Mail:Port"]!), SecureSocketOptions.StartTls);
        await smtp.AuthenticateAsync(_configuration["Mail:User"], _configuration["Mail:Pass"]);
        await smtp.SendAsync(email);
        await smtp.DisconnectAsync(true);
    }
}

using System.Net.Mail;
using System.Net;
using API_Web.Contracts;

namespace API_Web.Utility;

public class EmailService : IEmailService
{
    private readonly string smtpServer;
    private readonly int smtpPort;
    private readonly string fromEmailAddress;

    private readonly FluentEmail fluentEmail = new();


    public EmailService(string smtpServer, int smtpPort, string fromEmailAddress)
    {
        this.smtpServer = smtpServer;
        this.smtpPort = smtpPort;
        this.fromEmailAddress = fromEmailAddress;
    }

    public void SentEmailAsynch()
    {
        var message = new MailMessage
        {
            From = new MailAddress(fromEmailAddress),
            Subject = fluentEmail.Subject,
            Body = fluentEmail.HtmlMessage,
            To = { fluentEmail.Email },
            IsBodyHtml = true
        };

        using var client = new SmtpClient(smtpServer, smtpPort);
        client.Send(message);

        message.Dispose();
        client.Dispose();
    }

    public EmailService SetEmail(string email)
    {
        fluentEmail.Email = email;
        return this;
    }

    public EmailService SetHtmlMessage(string htmlMessage)
    {
        fluentEmail.HtmlMessage = htmlMessage;
        return this;

    }

    public EmailService SetSubject(string subject)
    {
        fluentEmail.Subject = subject;
        return this;

    }

}
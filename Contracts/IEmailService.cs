using API_Web.Utility;
using static API_Web.Utility.EmailService;

namespace API_Web.Contracts;

public interface IEmailService
{
    void SentEmailAsynch();
    EmailService SetEmail(string email);
    EmailService SetSubject(string subject);
    EmailService SetHtmlMessage(string htmlMessage);
}

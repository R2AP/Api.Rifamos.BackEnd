using System.Net.Mail;
using Api.Rifamos.BackEnd.Domain.Interfaces.Services;
using Api.Rifamos.BackEnd.Adapter;
using System.Runtime.InteropServices.Marshalling;

namespace Api.Rifamos.BackEnd.Domain.Services{
    public class EmailService : IEmailService
    {
        public EmailService(IConfiguration configuration){}
        public bool SendEmailGmail(EmailDTO oEmail)
        { 

            string sServeSmptp = "smtp.gmail.com";

            string sEmailFrom = oEmail.EmailFrom;
            string sEmailTo = oEmail.EmailTo;
            string sEmailPassword = oEmail.EmailPassword;
            string sEmailSubject= oEmail.EmailSubject;
            string sEmailBody = oEmail.EmailBody;

            MailMessage oMailMessage = new(sEmailFrom, sEmailTo, sEmailSubject, sEmailBody)
            {
                IsBodyHtml = true
            };

            SmtpClient oSmtpClient = new(sServeSmptp)
            {
                EnableSsl = true,
                UseDefaultCredentials = false,
                Port = 587,
                Credentials = new System.Net.NetworkCredential(sEmailFrom, sEmailPassword)
            };

            oSmtpClient.Send(oMailMessage);

            oSmtpClient.Dispose();

            return true;
        }

    }
}

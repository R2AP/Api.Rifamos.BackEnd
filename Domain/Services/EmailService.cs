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

            string sEmailFrom = oEmail.EmailFrom; // "RifamosTodo.online@gmail.com";
            string sEmailTo = oEmail.EmailTo; // "RifamosTodo.online@gmail.com";
            string sEmailPassword = oEmail.EmailPassword; // "jgkipuqyxsuxmzyn";
            string sEmailSubject= oEmail.EmailSubject; // "RifamosTodo.online: te enviamos tu opción de compra!";
            string sEmailBody = oEmail.EmailBody; // "<!DOCTYPE html><html><head></head><body><div style=\"width:100%;\"><h1>SALUDOS Terrícolas</h1></div></body></html>";

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

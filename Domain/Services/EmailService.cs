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

            MailMessage oMailMessage = new(oEmail.EmailFrom, oEmail.EmailTo)
            {
                Subject = oEmail.EmailSubject,
                IsBodyHtml = true
            };

            if (oEmail.EmailContentId!=string.Empty){
                AlternateView oAlternateView = AlternateView.CreateAlternateViewFromString(oEmail.EmailBody,null,"text/html");
                LinkedResource oLinkedResource = new(oEmail.EmailAttachmentContent)
                {
                    ContentId = oEmail.EmailContentId
                };
                oAlternateView.LinkedResources.Add(oLinkedResource);
                oMailMessage.AlternateViews.Add(oAlternateView); 
                oMailMessage.Body = oLinkedResource.ContentId;
            }
            else{
                oMailMessage.Body = oEmail.EmailBody;
            }

            oMailMessage.IsBodyHtml = true;

            if (oEmail.EmailAttachment!=string.Empty)
            {
                oMailMessage.Attachments.Add(new Attachment(oEmail.EmailAttachment));
            }

            SmtpClient oSmtpClient = new(sServeSmptp)
            {
                EnableSsl = true,
                UseDefaultCredentials = false,
                Port = 587,
                Credentials = new System.Net.NetworkCredential(oEmail.EmailFrom, oEmail.EmailPassword)
            };

            oSmtpClient.Send(oMailMessage);

            oSmtpClient.Dispose();

            return true;
        }

    }
}

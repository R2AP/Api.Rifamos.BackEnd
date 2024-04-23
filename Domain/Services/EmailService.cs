using System.Net.Mail;
using System.Text;
using System.Net.Mime;
using Api.Rifamos.BackEnd.Domain.Interfaces.Services;
using System.Net;

namespace Api.Rifamos.BackEnd.Domain.Services{
    public class EmailService : IEmailService
    {

        public EmailService(IConfiguration configuration){}

        public bool SendEmailGmail()
        {

            MailMessage oMailMessage = new();
            SmtpClient oSmtpClient = new();

            oMailMessage.From = new MailAddress("romulo.alegre@gmail.com");
            oMailMessage.To.Add("romulo.alegre@gmail.com");
            oMailMessage.To.Add("epam73@gmail.com");
            oMailMessage.Subject = "RifamosTodo.online: te enviamos tu opción de compra!";

            string oHtml = "<!DOCTYPE html><html><head></head><body><div style=\"width:100%;\"><h1>SALUDOS Terrícolas</h1></div></body></html>";

            AlternateView oAlternateView = AlternateView.CreateAlternateViewFromString(oHtml, Encoding.UTF8, MediaTypeNames.Text.Html);

            oMailMessage.AlternateViews.Add(oAlternateView);

            oSmtpClient.Host = "smtp.gmail.com";
            oSmtpClient.Port = 587;
            oSmtpClient.Credentials = new NetworkCredential("romulo.alegre@gmail.com", "G132639%Rrap.2024");
            oSmtpClient.EnableSsl = true;

            try
            {
                oSmtpClient.Send(oMailMessage);
                Console.WriteLine("Correo enviado");

            }catch(Exception oException)
            {
                Console.WriteLine("Error" + oException.Message);
                return false;
            }

            return true;
        }

    }
}

using Api.Rifamos.BackEnd.Adapter;

namespace Api.Rifamos.BackEnd.Domain.Interfaces.Services
{
    public interface IEmailService : IServiceBase
    {
        public bool SendEmailGmail(EmailDTO oEmail);

    }
}
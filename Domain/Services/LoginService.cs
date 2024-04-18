using Api.Rifamos.BackEnd.Adapter;
using Api.Rifamos.BackEnd.Domain.Interfaces.Repositories;
using Api.Rifamos.BackEnd.Domain.Interfaces.Services;
using Api.Rifamos.BackEnd.Domain.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using log4net;

namespace Api.Rifamos.BackEnd.Domain.Services{
    public class LoginService : ILoginService
    {
        private readonly ILoginRepository _loginRepository;
        private readonly ICryptoService _cryptoService;
        private static readonly ILog log = LogManager.GetLogger(typeof(UsuarioService));

        public LoginService(ILoginRepository loginRepository,
                            ICryptoService cryptoService,
                            IConfiguration configuration/*,
                            IHostingEnvironment environment*/
                            )
        {
            _loginRepository = loginRepository;
            _cryptoService = cryptoService;
            // _configuration = configuration;
            // _environment = environment;
        }

       public async Task<Usuario?> LoginUsuario(LoginDTO LoginDTO)
        {

            string sError = "";

            Usuario oUsuario = await _loginRepository.GetUsuarioEmail(LoginDTO);

            if (oUsuario == null)
            {
                sError = "No se encontró la cuenta indicada: " + LoginDTO.Email;
                log.Error(sError);                
                return null;
            }

            byte[] oKey = new byte[16];
            byte[] oIV  = new byte[16];
            byte[] oEncryptedPassword = oUsuario.Password;
            
            oKey = oUsuario.Key1;
            oIV = oUsuario.Key2;

            //Decrypt the password
            string sDecryptedPassword = _cryptoService.Decrypt(oEncryptedPassword, oKey, oIV);
            
            if (LoginDTO.Password != sDecryptedPassword)
            {
                sError = "Password incorrecto: " + LoginDTO.Email;
                log.Error(sError);
                return null;
            }

            return oUsuario;

        }
    }
}
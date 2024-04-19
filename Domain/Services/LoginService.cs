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

        private readonly IUsuarioRepository _usuarioRepository; 
        private readonly ICryptoService _cryptoService;
        private static readonly ILog log = LogManager.GetLogger(typeof(UsuarioService));

        public LoginService(ILoginRepository loginRepository,
                            IUsuarioRepository usuarioRepository, 
                            ICryptoService cryptoService,
                            IConfiguration configuration/*,
                            IHostingEnvironment environment*/
                            )
        {
            _loginRepository = loginRepository;
            _usuarioRepository = usuarioRepository;
            _cryptoService = cryptoService;
            // _configuration = configuration;
            // _environment = environment;
        }

       public async Task<UsuarioDTO> LoginUsuario(LoginDTO LoginDTO)
        {

            string sError = "";

            // Buscamos la cuenta de correo
            UsuarioDTO oUsuarioDTO = await _loginRepository.GetUsuarioEmail(LoginDTO);

            if (oUsuarioDTO == null)
            {
                sError = "No se encontró la cuenta indicada: " + LoginDTO.Email;
                log.Error(sError);                
                return null;
            }

            Usuario oUsuario = await _usuarioRepository.Get(oUsuarioDTO.UsuarioId);

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

            return oUsuarioDTO;

        }
    }
}
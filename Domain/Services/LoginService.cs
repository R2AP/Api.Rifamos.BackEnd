using System.Text;
using Api.Rifamos.BackEnd.Adapter;
using Api.Rifamos.BackEnd.Domain.Interfaces.Repositories;
using Api.Rifamos.BackEnd.Domain.Interfaces.Services;
using Api.Rifamos.BackEnd.Domain.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using log4net;

namespace Api.Rifamos.BackEnd.Domain.Services{
    public class LoginService : ILoginService
    {
        private readonly ILoginRepository _loginRepository;
        private readonly IUsuarioRepository _usuarioRepository; 
        private readonly ICryptoService _cryptoService;
        private readonly IConfiguration _configuration;
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
            _configuration = configuration;
            // _environment = environment;
        }

       public async Task<UsuarioFrontDTO> LoginUsuario(LoginDTO LoginDTO)
        {

            string sError = "";

            //Inicializamos clases y objetos
            Usuario oUsuario = new();
            UsuarioDTO oUsuarioDTO = new();
            UsuarioFrontDTO oUsuarioFrontDTO = new();

            // Buscamos la cuenta de correo
            oUsuario = await _usuarioRepository.GetUsuarioPorEmail(LoginDTO.Email);

            if (oUsuario == null)
            {
                oUsuarioFrontDTO.Error = true;
                oUsuarioFrontDTO.Mensaje = "Cuenta de correo o usuario no existe [" + LoginDTO.Email + "]";
                sError = "LoginService.LoginUsuario: Cuenta de correo o usuario no existe [" + LoginDTO.Email + "]";
                log.Error(sError);
                return oUsuarioFrontDTO;
            }
            
            List<string> oListToken = [];
       
            oListToken.Add(oUsuario.Password);
            oListToken.Add(oUsuario.Key1);
            oListToken.Add(oUsuario.Key2);
            
            //Decrypt the password
            string sDecryptedPassword = _cryptoService.IDecrypt(oListToken);
    
            if (LoginDTO.Password != sDecryptedPassword)
            {
                oUsuarioFrontDTO.Error = true;
                oUsuarioFrontDTO.Mensaje = "Cuenta de correo o usuario y/o password incorrecto [" + LoginDTO.Email + "]";
                sError = "LoginService.LoginUsuario: Cuenta de correo o usuario y/o password incorrecto [" + LoginDTO.Email + "]";
                log.Error(sError);
                return oUsuarioFrontDTO;
            }

            //Valores para generar el token de sesión
            oUsuarioDTO.UsuarioId = oUsuario.UsuarioId;
            oUsuarioDTO.Nombres = oUsuario.Nombres;
            oUsuarioDTO.ApellidoPaterno = oUsuario.ApellidoPaterno; 
            oUsuarioDTO.ApellidoMaterno = oUsuario.ApellidoMaterno;
            oUsuarioDTO.Email = oUsuario.Email;
            oUsuarioDTO.TipoDocumento = oUsuario.TipoDocumento;
            oUsuarioDTO.NumeroDocumento = oUsuario.NumeroDocumento;
            oUsuarioDTO.Telefono = oUsuario.Telefono;
            oUsuarioDTO.Token = GenerarToken(oUsuarioDTO);

            //Valores para devolver al controlador
            oUsuarioFrontDTO.UsuarioId = oUsuario.UsuarioId;
            oUsuarioFrontDTO.Nombres = oUsuario.Nombres;
            oUsuarioFrontDTO.ApellidoPaterno = oUsuario.ApellidoPaterno; 
            oUsuarioFrontDTO.ApellidoMaterno = oUsuario.ApellidoMaterno;
            oUsuarioFrontDTO.Email = oUsuario.Email;
            oUsuarioFrontDTO.TipoDocumento = oUsuario.TipoDocumento;
            oUsuarioFrontDTO.NumeroDocumento = oUsuario.NumeroDocumento;
            oUsuarioFrontDTO.Telefono = oUsuario.Telefono;
            oUsuarioFrontDTO.Token = oUsuarioDTO.Token;

            return oUsuarioFrontDTO;

        }

        private string GenerarToken(UsuarioDTO UsuarioDTO)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"])); 
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var userClaims = new[]
            {
                // new Claim(ClaimTypes.NameIdentifier, UsuarioDTO.UsuarioId),
                new Claim(ClaimTypes.Name, UsuarioDTO.Nombres),
                new Claim(ClaimTypes.Email, UsuarioDTO.Email)
            };
            var token = new JwtSecurityToken(
                issuer: "Rifamos.com",
                audience: "Rifamos.com",
                claims: userClaims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: credentials
                );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
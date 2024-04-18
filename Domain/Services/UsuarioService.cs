using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using Api.Rifamos.BackEnd.Adapter;
using Api.Rifamos.BackEnd.Domain.Interfaces.Repositories;
using Api.Rifamos.BackEnd.Domain.Interfaces.Services;
using Api.Rifamos.BackEnd.Domain.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
//using Newtonsoft.Json;
using System.Security.Cryptography;
using log4net;

namespace Api.Rifamos.BackEnd.Domain.Services{
    public class UsuarioService : IUsuarioService
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly ICryptoService _cryptoService;
        private static readonly ILog log = LogManager.GetLogger(typeof(UsuarioService));

        public UsuarioService(
                            IUsuarioRepository usuarioRepository,
                            ICryptoService cryptoService,
                            IConfiguration configuration/*,
                            IHostingEnvironment environment*/
                            )
        {
            _usuarioRepository = usuarioRepository;
            _cryptoService = cryptoService;
            // _configuration = configuration;
            // _environment = environment;
        }

        public async Task<Usuario> GetUsuario(Int32 UsuarioId)
        {
            return await _usuarioRepository.Get(UsuarioId);
        }

        public async Task<Usuario> InsertUsuario(Usuario Usuario, string Password)
        {

            byte[] oKey = new byte[16];
            byte[] oIV = new byte[16];

            using(RandomNumberGenerator rng = RandomNumberGenerator.Create()) {
            rng.GetBytes(oKey);
            rng.GetBytes(oIV);
            }

            //Encrypt the password
            byte[] oEncryptedPassword = _cryptoService.Encrypt(Password, oKey, oIV);
            
            Usuario.Password = oEncryptedPassword;

            // Usuario oUsuario = new(){

            //     UsuarioId = UsuarioDTO.UsuarioId,
            //     Nombres = UsuarioDTO.Nombres, 
            //     ApellidoPaterno = UsuarioDTO.ApellidoPaterno, 
            //     ApellidoMaterno = UsuarioDTO.ApellidoMaterno,
            //     Email = UsuarioDTO.Email,
            //     Password = oEncryptedPassword,
            //     Key1 = oKey,
            //     Key2 = oIV,
            //     TipoDocumento = UsuarioDTO.TipoDocumento,
            //     NumeroDocumento = UsuarioDTO.NumeroDocumento,
            //     Telefono = UsuarioDTO.Telefono,
            //     AuditoriaUsuarioIngreso = UsuarioDTO.AuditoriaUsuarioIngreso, 
            //     AuditoriaFechaIngreso = DateTime.Now 
                
            // };

            await _usuarioRepository.Post(Usuario);

            Usuario = await GetUsuario(Usuario.UsuarioId);

            return Usuario;

        }

        public async Task<Usuario> UpdateUsuario(Usuario Usuario)
        {

            Usuario oUsuario = await _usuarioRepository.Get(Usuario.UsuarioId);

            // oUsuario.UsuarioId = UsuarioDTO.UsuarioId;
            // oUsuario.Nombres = UsuarioDTO.Nombres;
            // oUsuario.ApellidoPaterno = UsuarioDTO.ApellidoPaterno;
            // oUsuario.ApellidoMaterno = UsuarioDTO.ApellidoMaterno;
            // oUsuario.Email = UsuarioDTO.Email;
            // oUsuario.TipoDocumento = UsuarioDTO.TipoDocumento;
            // oUsuario.NumeroDocumento = UsuarioDTO.NumeroDocumento;
            // oUsuario.Telefono = UsuarioDTO.Telefono;
            // oUsuario.AuditoriaUsuarioIngreso = UsuarioDTO.AuditoriaUsuarioIngreso;
            // oUsuario.AuditoriaUsuarioModificacion = UsuarioDTO.AuditoriaUsuarioModificacion; 
            // oUsuario.AuditoriaFechaModificacion = DateTime.Now;

            await _usuarioRepository.Put(oUsuario);

            return oUsuario;

        }

        public async Task<Usuario> DeleteUsuario(Int32 UsuarioId)
        {

            Usuario oUsuario = await _usuarioRepository.Get(UsuarioId);

            await _usuarioRepository.Delete(oUsuario);

            return oUsuario;

<<<<<<< HEAD
        }   
=======
        }

        public string GenerarToken(UsuarioDTO UsuarioDTO)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("Nim&jV@hy43UEbsMN>k}=84hF>h80qy_`96bD"));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var userClaims = new[]
            {
                // new Claim(ClaimTypes.NameIdentifier, UsuarioDTO.UsuarioId),
                new Claim(ClaimTypes.Name, UsuarioDTO.Nombres),
                new Claim(ClaimTypes.Email, UsuarioDTO.Email),
                new Claim(ClaimTypes.Role, UsuarioDTO.Email)
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
    
        public async Task<Usuario?> LoginUsuario(string Email, string Password)
        {

            string sError = "";

            Usuario oUsuario = await _usuarioRepository.GetUsuarioEmail(Email);

            if (oUsuario == null)
            {
                sError = "No se encontró la cuenta indicada: " + Email;
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
            
            if (Password != sDecryptedPassword)
            {
                sError = "Password incorrecto: " + Email;
                log.Error(sError);                
                return null;
            }

            return oUsuario;

        }     
>>>>>>> 64cf452dd8cefa82e5db80807b6bc38b019b165d
    }
}

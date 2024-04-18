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

namespace Api.Rifamos.BackEnd.Domain.Services{
    public class UsuarioService : IUsuarioService
    {
        private readonly IUsuarioRepository _usuarioRepository;

        // public IConfiguration _configuration { get; }
        // private IHostingEnvironment _environment;
        //private static readonly ILog log = LogManager.GetLogger(typeof(UltimusService));

        public UsuarioService(IUsuarioRepository usuarioRepository,
                            IConfiguration configuration/*,
                            IHostingEnvironment environment*/
                            )
        {
            _usuarioRepository = usuarioRepository;
            // _configuration = configuration;
            // _environment = environment;
        }

        public async Task<Usuario> GetUsuario(Int32 UsuarioId)
        {
            return await _usuarioRepository.Get(UsuarioId);
        }

        // public async Task<List<Rifa>> GetListRifaEstado(Int32 EstadoId)
        // {
        //     // var ejemplo = _configuration["prueba"];
        //     return await _rifaRepository.GetListRifaEstado(EstadoId);
        // }

        public async Task<Usuario> InsertUsuario(UsuarioDTO UsuarioDTO)
        {
            // var ejemplo = _configuration["prueba"];

            Usuario oUsuario = new Usuario(){

                UsuarioId = UsuarioDTO.UsuarioId,
                Nombres = UsuarioDTO.Nombres, 
                ApellidoPaterno = UsuarioDTO.ApellidoPaterno, 
                ApellidoMaterno = UsuarioDTO.ApellidoMaterno,
                Email = UsuarioDTO.Email,
                TipoDocumento = UsuarioDTO.TipoDocumento,
                NumeroDocumento = UsuarioDTO.NumeroDocumento,
                Telefono = UsuarioDTO.Telefono,
                AuditoriaUsuarioIngreso = UsuarioDTO.AuditoriaUsuarioIngreso, 
                AuditoriaFechaIngreso = DateTime.Now 
                
            };

            await _usuarioRepository.Post(oUsuario);

            return oUsuario;

        }

        public async Task<Usuario> UpdateUsuario(UsuarioDTO UsuarioDTO)
        {

            Usuario oUsuario = await _usuarioRepository.Get(UsuarioDTO.UsuarioId);

            oUsuario.UsuarioId = UsuarioDTO.UsuarioId;
            oUsuario.Nombres = UsuarioDTO.Nombres;
            oUsuario.ApellidoPaterno = UsuarioDTO.ApellidoPaterno;
            oUsuario.ApellidoMaterno = UsuarioDTO.ApellidoMaterno;
            oUsuario.Email = UsuarioDTO.Email;
            oUsuario.TipoDocumento = UsuarioDTO.TipoDocumento;
            oUsuario.NumeroDocumento = UsuarioDTO.NumeroDocumento;
            oUsuario.Telefono = UsuarioDTO.Telefono;
            oUsuario.AuditoriaUsuarioIngreso = UsuarioDTO.AuditoriaUsuarioIngreso;
            oUsuario.AuditoriaUsuarioModificacion = UsuarioDTO.AuditoriaUsuarioModificacion; 
            oUsuario.AuditoriaFechaModificacion = DateTime.Now;

            await _usuarioRepository.Put(oUsuario);

            return oUsuario;

        }

        public async Task<Usuario> DeleteUsuario(Int32 UsuarioId)
        {

            Usuario oUsuario = await _usuarioRepository.Get(UsuarioId);

            await _usuarioRepository.Delete(oUsuario);

            return oUsuario;

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
    }

}

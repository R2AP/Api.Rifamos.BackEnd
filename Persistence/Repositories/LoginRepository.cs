using Api.Rifamos.BackEnd.Domain.Interfaces.Repositories;
using Api.Rifamos.BackEnd.Domain.Models;
using Api.Rifamos.BackEnd.Adapter;
using Api.Rifamos.BackEnd.Persistence.Contexts;
using System.Configuration;
using Microsoft.EntityFrameworkCore;

namespace Api.Rifamos.BackEnd.Domain.Persistence.Repositories
{

    public class LoginRepository : RepositoryBase<LoginDTO>, ILoginRepository
    {
        public LoginRepository(RifamosContext context) : base(context) { }

        public async Task<UsuarioDTO> GetUsuarioEmail(LoginDTO LoginDTO)
        {

            Usuario oUsuario = new();
            UsuarioDTO oUsuarioDTO = new();

            oUsuario = await _context.Usuarios.Where(x => x.Email == LoginDTO.Email).FirstOrDefaultAsync();

            oUsuarioDTO.UsuarioId = oUsuario.UsuarioId;
            oUsuarioDTO.Nombres = oUsuario.Nombres;
            oUsuarioDTO.ApellidoPaterno = oUsuario.ApellidoPaterno; 
            oUsuarioDTO.ApellidoMaterno = oUsuario.ApellidoMaterno;
            oUsuarioDTO.Email = oUsuario.Email;
            oUsuarioDTO.Password = "****************";
            oUsuarioDTO.TipoDocumento = oUsuario.TipoDocumento;
            oUsuarioDTO.NumeroDocumento = oUsuario.NumeroDocumento;
            oUsuarioDTO.Telefono = oUsuario.Telefono;

            return oUsuarioDTO;

        }
    }
}

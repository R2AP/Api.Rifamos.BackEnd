using Api.Rifamos.BackEnd.Domain.Interfaces.Repositories;
using Api.Rifamos.BackEnd.Domain.Models;
using Api.Rifamos.BackEnd.Adapter;
using Api.Rifamos.BackEnd.Persistence.Contexts;

namespace Api.Rifamos.BackEnd.Domain.Persistence.Repositories
{

    public class UsuarioRepository : RepositoryBase<Usuario>, IUsuarioRepository
    {
        public UsuarioRepository(RifamosContext context) : base(context) { }

        public async Task<Usuario> GetUsuarioEmail(string Email)
        {

            Usuario oUsuario = new();

            oUsuario = _context.Usuarios.Where(usr => usr.Email == Email).FirstOrDefault();

/*             oUsuarioDTO.UsuarioId = oUsuario.UsuarioId;
            oUsuarioDTO.Nombres = oUsuario.Nombres;
            oUsuarioDTO.ApellidoPaterno = oUsuario.ApellidoPaterno;
            oUsuarioDTO.ApellidoMaterno = oUsuario.ApellidoMaterno;
            oUsuarioDTO.Email = oUsuario.Email;
            oUsuarioDTO.Password = oUsuario.Password.ToString();
            oUsuarioDTO.TipoDocumento = oUsuario.TipoDocumento;
            oUsuarioDTO.NumeroDocumento = oUsuario.NumeroDocumento;
            oUsuarioDTO.Telefono = oUsuario.Telefono;
            oUsuarioDTO.AuditoriaUsuarioIngreso = oUsuario.AuditoriaUsuarioIngreso;
            oUsuarioDTO.AuditoriaFechaIngreso = oUsuario.AuditoriaFechaIngreso;
            oUsuarioDTO.AuditoriaUsuarioModificacion = oUsuario.AuditoriaUsuarioModificacion;
            oUsuarioDTO.AuditoriaFechaModificacion = oUsuario.AuditoriaFechaModificacion; */

            return oUsuario;

        }
    }
}

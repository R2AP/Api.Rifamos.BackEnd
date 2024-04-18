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

        public async Task<Usuario> GetUsuarioEmail(LoginDTO LoginDTO)
        {

            return await _context.Usuarios.Where(x => x.Email == LoginDTO.Email).FirstOrDefaultAsync();

            // oUsuario =  (
            //                 from usr in _context.Usuarios 
            //                 where usr.Email == LoginDTO.Email
            //                 select new Usuario
            //                 {
            //                     UsuarioId = usr.UsuarioId,
            //                     Nombres = usr.Nombres,
            //                     ApellidoPaterno = usr.ApellidoPaterno,
            //                     ApellidoMaterno = usr.ApellidoMaterno,
            //                     Email = usr.Email,
            //                     Password = usr.Password,
            //                     Key1 = usr.Key1,
            //                     Key2 = usr.Key2,
            //                     TipoDocumento = usr.TipoDocumento,
            //                     NumeroDocumento = usr.NumeroDocumento,
            //                     Telefono = usr.Telefono,
            //                     AuditoriaUsuarioIngreso = usr.AuditoriaUsuarioIngreso,
            //                     AuditoriaFechaIngreso = usr.AuditoriaFechaIngreso,
            //                     AuditoriaUsuarioModificacion = usr.AuditoriaUsuarioModificacion,
            //                     AuditoriaFechaModificacion = usr.AuditoriaFechaModificacion
            //                 }).FirstOrDefaultAsync();

            //return await usuario;

        }
    }
}

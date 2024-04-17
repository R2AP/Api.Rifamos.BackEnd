using System;
using System.Configuration;
using System.Transactions;
using Api.Rifamos.BackEnd.Domain.Interfaces.Repositories;
using Api.Rifamos.BackEnd.Domain.Models;
using Api.Rifamos.BackEnd.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using Microsoft.VisualBasic;

namespace Api.Rifamos.BackEnd.Domain.Persistence.Repositories
{

    public class UsuarioRepository : RepositoryBase<Usuario>, IUsuarioRepository
    {
        public UsuarioRepository(RifamosContext context) : base(context) { }

        public async Task<Usuario>LoginUsuario(string Usuario, string Password)
        {

            var usuario = _context.Usuarios.Where(usr => usr.Email == Usuario).FirstOrDefault();
            if (usuario == null)
            {
                usuario.UsuarioId = 0;
            }            

            return usuario;

            // return await (from usr in _context.Usuarios
            //                 where usr.Email == Usuario
            //                 select new Usuario
            //                 {
            //                     UsuarioId = usr.UsuarioId, 
            //                     Nombres = usr.Nombres,
            //                     ApellidoPaterno = usr.ApellidoPaterno,
            //                     ApellidoMaterno = usr.ApellidoMaterno,
            //                     Email = usr.Email,
            //                     TipoDocumento = usr.TipoDocumento,
            //                     NumeroDocumento = usr.NumeroDocumento,
            //                     Telefono = usr.Telefono,
            //                     AuditoriaUsuarioIngreso = usr.AuditoriaUsuarioIngreso,
            //                     AuditoriaFechaIngreso = usr.AuditoriaFechaIngreso,
            //                     AuditoriaUsuarioModificacion = usr.AuditoriaUsuarioModificacion,
            //                     AuditoriaFechaModificacion = usr.AuditoriaFechaModificacion
            //                 }).FirstOrDefaultAsync();

        }

    }
}
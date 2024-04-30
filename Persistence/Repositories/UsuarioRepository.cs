using Api.Rifamos.BackEnd.Domain.Interfaces.Repositories;
using Api.Rifamos.BackEnd.Domain.Models;
using Api.Rifamos.BackEnd.Adapter;
using Api.Rifamos.BackEnd.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Api.Rifamos.BackEnd.Domain.Persistence.Repositories
{

    public class UsuarioRepository(RifamosContext context) : RepositoryBase<Usuario>(context), IUsuarioRepository
    {
        public async Task<Usuario> GetUsuarioPorEmail(string oEmail)
        {
            return await _context.Usuarios.Where(x => x.Email == oEmail).FirstOrDefaultAsync();
        }
    }
}

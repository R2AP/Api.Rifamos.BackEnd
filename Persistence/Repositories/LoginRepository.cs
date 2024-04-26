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

        public async Task<Usuario> GetUsuarioEmail(LoginDTO LoginDTO) => await _context.Usuarios.Where(x => x.Email == LoginDTO.Email).FirstOrDefaultAsync();
    }
}

using Api.Rifamos.BackEnd.Domain.Interfaces.Repositories;
using Api.Rifamos.BackEnd.Domain.Models;
using Api.Rifamos.BackEnd.Persistence.Contexts;

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

        }

    }
}
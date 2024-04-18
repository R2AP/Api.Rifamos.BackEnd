using Api.Rifamos.BackEnd.Domain.Interfaces.Repositories;
using Api.Rifamos.BackEnd.Domain.Models;
using Api.Rifamos.BackEnd.Adapter;
using Api.Rifamos.BackEnd.Persistence.Contexts;

namespace Api.Rifamos.BackEnd.Domain.Persistence.Repositories
{

    public class UsuarioRepository : RepositoryBase<Usuario>, IUsuarioRepository
    {
        public UsuarioRepository(RifamosContext context) : base(context) { }
    }
}
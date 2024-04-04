using Api.Rifamos.BackEnd.Domain.Interfaces.Repositories;
using Api.Rifamos.BackEnd.Domain.Models;
using Api.Rifamos.BackEnd.Persistence.Contexts;

namespace Api.Rifamos.BackEnd.Domain.Persistence.Repositories
{

    public class RifaRepository : RepositoryBase<Rifa>, IRifaRepository
    {
        public RifaRepository(RifamosContext context) : base(context) { }
    }
}
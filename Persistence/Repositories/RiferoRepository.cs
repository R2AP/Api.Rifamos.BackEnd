using Api.Rifamos.BackEnd.Domain.Interfaces.Repositories;
using Api.Rifamos.BackEnd.Domain.Models;
using Api.Rifamos.BackEnd.Adapter;
using Api.Rifamos.BackEnd.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Api.Rifamos.BackEnd.Domain.Persistence.Repositories
{

    public class RiferoRepository(RifamosContext context) : RepositoryBase<Rifero>(context), IRiferoRepository
    {
        public async Task<Rifero> GetRiferoPorEmail(string oEmail)
        {
            return await _context.Riferos.Where(x => x.Email == oEmail).FirstOrDefaultAsync();
        }
    }
}

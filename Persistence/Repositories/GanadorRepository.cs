using System;
using System.Transactions;
using Api.Rifamos.BackEnd.Domain.Interfaces.Repositories;
using Api.Rifamos.BackEnd.Domain.Models;
using Api.Rifamos.BackEnd.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;

namespace Api.Rifamos.BackEnd.Domain.Persistence.Repositories
{

    public class GanadorRepository : RepositoryBase<Ganador>, IGanadorRepository
    {
        public GanadorRepository(RifamosContext context) : base(context) { }

        public async Task<List<Ganador>> GetGanadorPorPremio(Int32 oPremioId)
        {
            var ganador = (from gan in _context.Ganadors
                            where gan.PremioId == oPremioId // Alta
                            select new Ganador
                            {
                                GanadorId = gan.GanadorId,
                                PremioId = gan.PremioId,
                                RiferoId = gan.RiferoId
                            })
                            .ToListAsync();

            return await ganador;            
        }

    }
}
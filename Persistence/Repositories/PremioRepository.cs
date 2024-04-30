using Api.Rifamos.BackEnd.Domain.Interfaces.Repositories;
using Api.Rifamos.BackEnd.Domain.Models;
using Api.Rifamos.BackEnd.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;

namespace Api.Rifamos.BackEnd.Domain.Persistence.Repositories
{

    public class PremioRepository : RepositoryBase<Premio>, IPremioRepository
    {
        public PremioRepository(RifamosContext context) : base(context) { }

        public async Task<List<Premio>> GetListPremio(Int32 RifaId)
        {

            return await (from prm in _context.Premios 
                            where prm.RifaId == RifaId 
                            select new Premio
                            {
                                PremioId = prm.PremioId,
                                RifaId = prm.RifaId,
                                PremioDescripcion = prm.PremioDescripcion,
                                PremioDetalle = prm.PremioDetalle,
                                Url = prm.Url,
                                Imagen = prm.Imagen,
                                AuditoriaUsuarioIngreso = prm.AuditoriaUsuarioIngreso,
                                AuditoriaFechaIngreso = prm.AuditoriaFechaIngreso,
                                AuditoriaUsuarioModificacion = prm.AuditoriaUsuarioModificacion,
                                AuditoriaFechaModificacion = prm.AuditoriaFechaModificacion, 

                            }).ToListAsync();

        }
    }
}
using Api.Rifamos.BackEnd.Domain.Interfaces.Repositories;
using Api.Rifamos.BackEnd.Domain.Models;
using Api.Rifamos.BackEnd.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Api.Rifamos.BackEnd.Domain.Persistence.Repositories
{

    public class PrecioRepository : RepositoryBase<Precio>, IPrecioRepository
    {
        public PrecioRepository(RifamosContext context) : base(context) { }

        public async Task<Precio> GetPrecioUnitario(Int32 oRifaId)
        {
            return await (from prc in _context.Precios
                          join rif in _context.Rifas on new { RifaId = prc.RifaId } equals new { RifaId = rif.RifaId }
                          where prc.RifaId == oRifaId
                          select new Precio
                          {
                              PrecioId = prc.PrecioId,
                              RifaId = prc.RifaId,
                              PrecioUnitario = prc.PrecioUnitario,
                              AuditoriaUsuarioIngreso = prc.AuditoriaUsuarioIngreso,
                              AuditoriaFechaIngreso = prc.AuditoriaFechaIngreso,
                              AuditoriaUsuarioModificacion = prc.AuditoriaUsuarioModificacion,
                              AuditoriaFechaModificacion = prc.AuditoriaFechaModificacion,

                          }).FirstOrDefaultAsync();

        }

    }
}
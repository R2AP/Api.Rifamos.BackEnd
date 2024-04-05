using Api.Rifamos.BackEnd.Domain.Interfaces.Repositories;
using Api.Rifamos.BackEnd.Domain.Models;
using Api.Rifamos.BackEnd.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;

namespace Api.Rifamos.BackEnd.Domain.Persistence.Repositories
{

    public class RifaRepository : RepositoryBase<Rifa>, IRifaRepository
    {
        public RifaRepository(RifamosContext context) : base(context) { }

        public async Task<List<Rifa>> GetListRifa(Int32 UsuarioId)
        {

            var rifa = (from opc in _context.Opcions
                            join rif in _context.Rifas on new {RifaId = opc.RifaId} equals new {RifaId = rif.RifaId}
                            where opc.UsuarioId == UsuarioId
                            select new Rifa
                            {
                                RifaId = rif.RifaId,
                                RifaDescripcion = rif.RifaDescripcion,
                                FechaSorteo = rif.FechaSorteo,
                                HoraSorteo = rif.HoraSorteo,
                                Sponsor = rif.Sponsor,
                                AuditoriaUsuarioIngreso = rif.AuditoriaUsuarioIngreso,
                                AuditoriaFechaIngreso = rif.AuditoriaFechaIngreso,
                                AuditoriaUsuarioModificacion = rif.AuditoriaUsuarioModificacion,
                                AuditoriaFechaModificacion = rif.AuditoriaFechaModificacion, 

                            }).ToListAsync();

            return await rifa;
        }        
    }
}
using Api.Rifamos.BackEnd.Domain.Interfaces.Repositories;
using Api.Rifamos.BackEnd.Domain.Models;
using Api.Rifamos.BackEnd.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;

namespace Api.Rifamos.BackEnd.Domain.Persistence.Repositories
{

    public class OpcionRepository : RepositoryBase<Opcion>, IOpcionRepository
    {
        public OpcionRepository(RifamosContext context) : base(context) { }

        public async Task<List<Opcion>> GetListOpcion(Int32 RifaId, Int32 UsuarioId )
        {

            var opcion = (from opc in _context.Opcions 
                            where opc.RifaId == RifaId && opc.UsuarioId == UsuarioId
                            select new Opcion
                            {
                                OpcionId = opc.OpcionId,
                                RifaId = opc.RifaId,
                                UsuarioId = opc.UsuarioId,
                                CantidadOpciones = opc.CantidadOpciones,
                                EstadoOpcion = opc.EstadoOpcion,
                                AuditoriaUsuarioIngreso = opc.AuditoriaUsuarioIngreso,
                                AuditoriaFechaIngreso = opc.AuditoriaFechaIngreso,
                                AuditoriaUsuarioModificacion = opc.AuditoriaUsuarioModificacion,
                                AuditoriaFechaModificacion = opc.AuditoriaFechaModificacion, 

                            }).ToListAsync();

            return await opcion;
        }
    }
}
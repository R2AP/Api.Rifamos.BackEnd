using Api.Rifamos.BackEnd.Adapter;
using Api.Rifamos.BackEnd.Domain.Interfaces.Repositories;
using Api.Rifamos.BackEnd.Domain.Models;
using Api.Rifamos.BackEnd.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;

namespace Api.Rifamos.BackEnd.Domain.Persistence.Repositories
{
    public class OpcionRepository(RifamosContext context) : RepositoryBase<Opcion>(context), IOpcionRepository
    {
        public async Task<List<Opcion>> GetListOpcion(Int32 RifaId, Int32 UsuarioId )
        {

            return await (from opc in _context.Opcions 
                            where opc.RifaId == RifaId && opc.UsuarioId == UsuarioId
                            select new Opcion
                            {
                                OpcionId = opc.OpcionId,
                                RifaId = opc.RifaId,
                                UsuarioId = opc.UsuarioId,
                                TokenOpcion = opc.TokenOpcion,
                                TokenKey1 = opc.TokenKey1,
                                TokenKey2 = opc.TokenKey2,
                                CantidadOpciones = opc.CantidadOpciones,
                                EstadoOpcion = opc.EstadoOpcion,
                                AuditoriaUsuarioIngreso = opc.AuditoriaUsuarioIngreso,
                                AuditoriaFechaIngreso = opc.AuditoriaFechaIngreso,
                                AuditoriaUsuarioModificacion = opc.AuditoriaUsuarioModificacion,
                                AuditoriaFechaModificacion = opc.AuditoriaFechaModificacion

                            }).ToListAsync();
        }

        public async Task<Opcion> GetOpcionToken(string TokenOpcion) => await _context.Opcions.Where(x => x.TokenOpcion == TokenOpcion).FirstOrDefaultAsync();
    }
}
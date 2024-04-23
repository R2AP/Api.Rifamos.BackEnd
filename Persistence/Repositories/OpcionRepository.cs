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

        public async Task<OpcionDTO> GetOpcionToken(string TokenOpcion)
        {

            OpcionDTO oOpcionDTO = new();
            Opcion oOpcion = new();

            oOpcion = await _context.Opcions.Where(x => x.TokenOpcion == TokenOpcion).FirstOrDefaultAsync();

            oOpcionDTO.OpcionId = oOpcion.OpcionId;
            oOpcionDTO.RifaId = oOpcion.RifaId;
            oOpcionDTO.UsuarioId = oOpcion.UsuarioId;
            oOpcionDTO.TokenOpcion = oOpcion.TokenOpcion;
            oOpcionDTO.TokenKey1 = oOpcion.TokenKey1;
            oOpcionDTO.TokenKey2 = oOpcion.TokenKey2;
            oOpcionDTO.CantidadOpciones = oOpcion.CantidadOpciones;
            oOpcionDTO.EstadoOpcion = oOpcion.EstadoOpcion;
            oOpcionDTO.AuditoriaUsuarioIngreso = oOpcion.AuditoriaUsuarioIngreso;
            oOpcionDTO.AuditoriaFechaIngreso = oOpcion.AuditoriaFechaIngreso;
            oOpcionDTO.AuditoriaUsuarioModificacion = oOpcion.AuditoriaUsuarioModificacion;
            oOpcionDTO.AuditoriaFechaModificacion = oOpcion.AuditoriaFechaModificacion;

            return oOpcionDTO;
        }        
    }
}
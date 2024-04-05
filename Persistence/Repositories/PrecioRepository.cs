using Api.Rifamos.BackEnd.Domain.Interfaces.Repositories;
using Api.Rifamos.BackEnd.Domain.Models;
using Api.Rifamos.BackEnd.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;


//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;

namespace Api.Rifamos.BackEnd.Domain.Persistence.Repositories
{

    public class PrecioRepository : RepositoryBase<Precio>, IPrecioRepository
    {
        public PrecioRepository(RifamosContext context) : base(context) { }

    public async Task<List<Precio>> GetListPrecio(Int32 RifaId)
    {
        var precio = (from prc in _context.Precios 
                        join rif in _context.Rifas on new {RifaId = prc.RifaId} equals new {RifaId = rif.RifaId}
                        where prc.RifaId == RifaId
                        select new Precio
                        {
                            //PrecioId = Convert.ToInt64(prc.PrecioId),
                            PrecioId = prc.PrecioId,
                            RifaId =prc.RifaId,
                            PrecioUnitario = prc.PrecioUnitario,
                            AuditoriaUsuarioIngreso = prc.AuditoriaUsuarioIngreso,
                            AuditoriaFechaIngreso = prc.AuditoriaFechaIngreso,
                            AuditoriaUsuarioModificacion = prc.AuditoriaUsuarioModificacion,
                            AuditoriaFechaModificacion = prc.AuditoriaFechaModificacion, 

                        }).ToListAsync();

        return await precio;
    }

    }
}
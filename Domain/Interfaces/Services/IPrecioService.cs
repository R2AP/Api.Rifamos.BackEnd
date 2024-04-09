using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Api.Rifamos.BackEnd.Domain.Models;
using Api.Rifamos.BackEnd.Adapter;

namespace Api.Rifamos.BackEnd.Domain.Interfaces.Services
{
    public interface IPrecioService : IServiceBase
    {
        Task<Precio> GetPrecio(Int32 PrecioId);
        Task<List<Precio>> GetListPrecio(Int32 RifaId);
        Task<Precio> InsertPrecio(PrecioDTO PrecioDTO);
        Task<Precio> UpdatePrecio(PrecioDTO PrecioDTO);        
    }
}
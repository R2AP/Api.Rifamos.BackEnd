using System;
using System.Collections.Generic;
using System.Threading.Tasks;
//using Interseguro.ADMWR.Backend.Adapter;
using Api.Rifamos.BackEnd.Domain.Models;

namespace Api.Rifamos.BackEnd.Domain.Interfaces.Services
{
    public interface IPrecioService : IServiceBase
    {

        Task<Precio> GetPrecio(Int32 PrecioId);
        Task<List<Precio>> GetListPrecio(Int32 RifaId);
    }
}
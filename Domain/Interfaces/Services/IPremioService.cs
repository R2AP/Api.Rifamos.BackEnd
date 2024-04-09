using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Api.Rifamos.BackEnd.Adapter;
using Api.Rifamos.BackEnd.Domain.Models;

namespace Api.Rifamos.BackEnd.Domain.Interfaces.Services
{
    public interface IPremioService : IServiceBase
    {
        Task<List<Premio>> GetListPremio(Int32 RifaId);
        Task<Premio> InsertPremio(PremioDTO PremioDTO);
        Task<Premio> UpdatePremio(PremioDTO PremioDTO);        
    }
}

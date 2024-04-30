using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Api.Rifamos.BackEnd.Adapter;
using Api.Rifamos.BackEnd.Domain.Models;

namespace Api.Rifamos.BackEnd.Domain.Interfaces.Services
{
    public interface IPremioService : IServiceBase
    {

        //Métodos Básicos
        Task<Premio> Get(Int32 oRifaId);
        Task<Premio> Insert(Premio oPremio);
        Task<Premio> Update(Premio oPremio);
        Task<Premio> Delete(Int32 oPremioId);

		//Métodos Complementarios
        Task<List<Premio>> GetListPremio(Int32 oRifaId);
        Task<PremioFrontDTO> InsertPremio(PremioDTO oPremioDTO);
        Task<PremioFrontDTO> UpdatePremio(PremioDTO oPremioDTO);
        Task<PremioFrontDTO> DeletePremio(Int32 oPremioId);  
    }
}

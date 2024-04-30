using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Api.Rifamos.BackEnd.Domain.Models;
using Api.Rifamos.BackEnd.Adapter;

namespace Api.Rifamos.BackEnd.Domain.Interfaces.Services
{
    public interface IPrecioService : IServiceBase
    {
        //Métodos Básicos
        Task<Precio> Get(Int32 oPrecioId);
        Task<Precio> Insert(Precio oPrecio);
        Task<Precio> Update(Precio oPrecio);
        Task<Precio> Delete(Int32 oPrecioId);

		//Métodos Complementarios
        Task<Precio> GetPrecio(Int32 oPrecioId);
        Task<Precio> GetPrecioUnitario(Int32 oRifaId);
        Task<PrecioFrontDTO> InsertPrecio(PrecioDTO oPrecioDTO);
        Task<Precio> UpdatePrecio(PrecioDTO oPrecioDTO);
        Task<Precio> DeletePrecio(Int32 oPrecioId);                
    }
}
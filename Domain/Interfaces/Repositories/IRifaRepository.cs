using System;
using System.Collections.Generic;
using System.Threading.Tasks;
//using Interseguro.ADMWR.Backend.Adapter;
using Api.Rifamos.Backend.Domain;
using Api.Rifamos.BackEnd.Domain.Interfaces.Repositories;

//using Api.Rifamos.BackEnd.Domain.Interfaces.Repositories;
using Api.Rifamos.BackEnd.Domain.Models;

namespace Api.Rifamos.Backend.Domain.Interfaces.Repositories
{
    public interface IRifaRepository : IRepositoryBase<Rifa>
    {
        // Task<List<EndosoDTO>> GetAllEndososFlujo(Int64 codPoliza);
        // Task<bool> ValidarEndosoPendientes(string glsPoliza);
        // Task<List<DetalleEndosoDTO>> ListarDetalleBaseEndoso(Int64 codPoliza);
        // Task<List<DetalleEndosoDTO>> ListarDetalleEndoso(Int64 codEndoso);
        // Task<List<DetalleEndosoPensionDTO>> ListarDetalleEndosoPension(Int64 codEndoso);
        // Task<EndosoApoderadoDTO> ObtenerDetalleEndosoApoderado(Int64 codEndoso);
        // Task<int> GetNumEndosoPoliza(decimal codPoliza);
        Task<Rifa> GetRifa(Int64 IDRifa);
        // Task<EndosoDTO> ObtenerEndosoCabecera(Int64 codPoliza, Int64 codEndoso);
        // Task<Int64> GetNumEndosoConfirmadoAnteriorPoliza(Int64 codPoliza, Int64 codEndoso);
    }

}
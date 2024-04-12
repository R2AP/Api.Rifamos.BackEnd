using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Api.Rifamos.BackEnd.Adapter;
using Api.Rifamos.BackEnd.Domain.Models;

namespace Api.Rifamos.BackEnd.Domain.Interfaces.Services
{
    public interface ISesionService : IServiceBase
    {

        Task<Sesion> GetSesion(Int32 SesionId);
        Task<Sesion> InsertSesion(SesionDTO SesionDTO);
        Task<Sesion> UpdateSesion(SesionDTO SesionDTO);
        Task<Sesion> DeleteSesion(Int32 SesionId);
    }
}
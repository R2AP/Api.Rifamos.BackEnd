using System;
using System.Collections.Generic;
using System.Threading.Tasks;
//using Interseguro.ADMWR.Backend.Adapter;
using Api.Rifamos.BackEnd.Domain.Models;

namespace Api.Rifamos.BackEnd.Domain.Interfaces.Services
{
    public interface IRifaService : IServiceBase
    {

        Task<Rifa> GetRifa(Int64 RifaId);

    }
}
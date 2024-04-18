using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Api.Rifamos.BackEnd.Domain.Models;
using Api.Rifamos.BackEnd.Adapter;

namespace Api.Rifamos.BackEnd.Domain.Interfaces.Services
{
    public interface ILoginService : IServiceBase
    {
        Task<Usuario>LoginUsuario(LoginDTO LoginDTO);
    }
}
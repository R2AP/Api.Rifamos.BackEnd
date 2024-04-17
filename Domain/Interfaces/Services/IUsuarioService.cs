using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Api.Rifamos.BackEnd.Adapter;
using Api.Rifamos.BackEnd.Domain.Models;

namespace Api.Rifamos.BackEnd.Domain.Interfaces.Services
{
    public interface IUsuarioService : IServiceBase
    {

        Task<Usuario>GetUsuario(Int32 UsuarioId);
        Task<Usuario>InsertUsuario(UsuarioDTO UsuarioDTO, string Password);
        Task<Usuario>UpdateUsuario(UsuarioDTO UsuarioDTO);
        Task<Usuario>DeleteUsuario(Int32 UsuarioId);
        Task<Usuario>LoginUsuario(String Usuario, string Password);
    }
}

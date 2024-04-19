using Api.Rifamos.BackEnd.Domain.Models;
using Api.Rifamos.BackEnd.Adapter;

namespace Api.Rifamos.BackEnd.Domain.Interfaces.Services
{
    public interface IUsuarioService : IServiceBase
    {

        Task<UsuarioDTO>GetUsuario(UsuarioDTO UsuarioDTO);
        Task<UsuarioDTO>InsertUsuario(UsuarioDTO UsuarioDTO);
        Task<UsuarioDTO>UpdateUsuario(UsuarioDTO UsuarioDTO);
        Task<UsuarioDTO>DeleteUsuario(UsuarioDTO UsuarioDTO);
    }

}

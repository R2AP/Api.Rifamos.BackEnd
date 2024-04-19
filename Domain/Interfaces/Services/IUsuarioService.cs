using Api.Rifamos.BackEnd.Domain.Models;
using Api.Rifamos.BackEnd.Adapter;

namespace Api.Rifamos.BackEnd.Domain.Interfaces.Services
{
    public interface IUsuarioService : IServiceBase
    {

        Task<UsuarioDTO>GetUsuario(Int32 UsuarioId);
        //Task<Usuario>InsertUsuario(Usuario Usuario, string Password);
        Task<UsuarioDTO>InsertUsuario(UsuarioDTO UsuarioDTO);
        Task<Usuario>UpdateUsuario(Usuario Usuario);
        Task<Usuario>DeleteUsuario(Int32 UsuarioId);
    }

}

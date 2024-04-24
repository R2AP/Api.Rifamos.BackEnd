using Api.Rifamos.BackEnd.Domain.Models;
using Api.Rifamos.BackEnd.Adapter;

namespace Api.Rifamos.BackEnd.Domain.Interfaces.Services
{
    public interface IUsuarioService : IServiceBase
    {

        Task<Usuario> GetUsuario(Int32 UsuarioId );
        Task<Usuario> GetUsuarioPorEmail(string Email);    
        Task<UsuarioFrontDTO> InsertUsuario(UsuarioDTO UsuarioDTO);
        Task<UsuarioFrontDTO> UpdateUsuario(UsuarioDTO UsuarioDTO);
        Task<UsuarioFrontDTO> DeleteUsuario(UsuarioDTO UsuarioDTO);
        Task<UsuarioFrontDTO> UpdatePasswordUsuario(UsuarioPasswordDTO UsuarioPasswordDTO);
    }

}

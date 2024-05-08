using Api.Rifamos.BackEnd.Domain.Models;
using Api.Rifamos.BackEnd.Adapter;

namespace Api.Rifamos.BackEnd.Domain.Interfaces.Services
{
    public interface IUsuarioService : IServiceBase
    {
        //Métodos Básicos
        Task<Usuario> Get(Int32 oUsuarioId);
        Task<Usuario> Insert(Usuario oUsuario);
        Task<Usuario> Update(Usuario oUsuario);
        Task<Usuario> Delete(Int32 oUsuarioId);

        //Métodos Complementarios
        Task<Usuario> GetUsuario(Int32 oUsuarioId);
        Task<Usuario> GetUsuarioPorEmail(string oEmail);    
        Task<UsuarioFrontDTO> InsertUsuario(UsuarioDTO oUsuarioDTO);
        Task<UsuarioFrontDTO> UpdateUsuario(UsuarioDTO oUsuarioDTO);
        Task<UsuarioFrontDTO> DeleteUsuario(Int32 oUsuarioId);
        Task<UsuarioFrontDTO> UpdatePasswordUsuario(UsuarioPasswordDTO oUsuarioPasswordDTO);
        Task<UsuarioFrontDTO> RecuperarPassword(string oEmail);
    }

}

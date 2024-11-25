using Api.Rifamos.BackEnd.Domain.Models;
using Api.Rifamos.BackEnd.Adapter;

namespace Api.Rifamos.BackEnd.Domain.Interfaces.Services
{
    public interface IUsuarioService : IServiceBase
    {
        //Métodos Básicos
        Task<Usuario> Get(string oEmail);
        Task<Usuario> Insert(Usuario oUsuario);
        Task<Usuario> Update(Usuario oUsuario);
        Task<Usuario> Delete(string oEmail);

        //Métodos Complementarios
        Task<Usuario> GetUsuario(string oEmail);
        Task<Usuario> GetUsuarioPorEmail(string oEmail);    
        Task<UsuarioFrontDTO> InsertUsuario(UsuarioDTO oUsuarioDTO);
        Task<UsuarioFrontDTO> UpdateUsuario(UsuarioDTO oUsuarioDTO);
        Task<UsuarioFrontDTO> DeleteUsuario(string oEmail);
        Task<UsuarioFrontDTO> UpdatePasswordUsuario(UsuarioPasswordDTO oUsuarioPasswordDTO);
        Task<UsuarioFrontDTO> RecuperarPassword(string oEmail);
    }

}

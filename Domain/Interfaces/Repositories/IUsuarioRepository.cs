using Api.Rifamos.BackEnd.Domain.Models;

namespace Api.Rifamos.BackEnd.Domain.Interfaces.Repositories

{
    public interface IUsuarioRepository : IRepositoryBase<Usuario>
    {
        Task<Usuario> GetUsuarioPorEmail(string oEmail);   
    }

}
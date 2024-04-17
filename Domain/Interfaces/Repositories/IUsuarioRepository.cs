using Api.Rifamos.BackEnd.Domain.Models;

namespace Api.Rifamos.BackEnd.Domain.Interfaces.Repositories

{
    public interface IUsuarioRepository : IRepositoryBase<Usuario>
    {
        Task<Usuario>LoginUsuario(string UsuarioId, string Password);
    }

}
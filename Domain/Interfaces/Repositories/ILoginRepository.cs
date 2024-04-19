using Api.Rifamos.BackEnd.Adapter;
using Api.Rifamos.BackEnd.Domain.Models;

namespace Api.Rifamos.BackEnd.Domain.Interfaces.Repositories

{
    public interface ILoginRepository : IRepositoryBase<LoginDTO>
    {
        Task<UsuarioDTO> GetUsuarioEmail(LoginDTO LoginDTO);
    }

}

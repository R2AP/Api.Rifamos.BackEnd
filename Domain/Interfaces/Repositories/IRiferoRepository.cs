using Api.Rifamos.BackEnd.Domain.Models;

namespace Api.Rifamos.BackEnd.Domain.Interfaces.Repositories

{
    public interface IRiferoRepository : IRepositoryBase<Rifero>
    {
        Task<Rifero> GetRiferoPorEmail(string oEmail);   
    }

}
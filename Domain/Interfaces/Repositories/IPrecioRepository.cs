using Api.Rifamos.BackEnd.Domain.Models;

namespace Api.Rifamos.BackEnd.Domain.Interfaces.Repositories

{
    public interface IPrecioRepository : IRepositoryBase<Precio>
    {
        Task<Precio> GetPrecioUnitario(Int32 RifaId);
        // Task<bool> ValidarEndosoPendientes(string glsPoliza);
        // Task<List<DetalleEndosoDTO>> ListarDetalleBaseEndoso(Int64 codPoliza);
        // Task<List<DetalleEndosoDTO>> ListarDetalleEndoso(Int64 codEndoso);
        // Task<List<DetalleEndosoPensionDTO>> ListarDetalleEndosoPension(Int64 codEndoso);
        // Task<EndosoApoderadoDTO> ObtenerDetalleEndosoApoderado(Int64 codEndoso);
        // Task<int> GetNumEndosoPoliza(decimal codPoliza);
        // Task<Rifa> GetRifa(Int64 IDRifa);
        // Task<EndosoDTO> ObtenerEndosoCabecera(Int64 codPoliza, Int64 codEndoso);
        // Task<Int64> GetNumEndosoConfirmadoAnteriorPoliza(Int64 codPoliza, Int64 codEndoso);
    }

}
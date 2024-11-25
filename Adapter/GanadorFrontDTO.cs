namespace Api.Rifamos.BackEnd.Adapter{
    
public partial class GanadorFrontDTO : ErrorDTO{
    public int PremioId { get; set; }

    public int RiferoId { get; set; }

    public string AuditoriaUsuarioIngreso { get; set; } = null!;

    public DateTime AuditoriaFechaIngreso { get; set; }

    public string? AuditoriaUsuarioModificacion { get; set; }

    public DateTime? AuditoriaFechaModificacion { get; set; }

    }
}
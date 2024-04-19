namespace Api.Rifamos.BackEnd.Adapter{

public class UsuarioDTO{
    public int UsuarioId { get; set; }

    public string Nombres { get; set; } = null!;

    public string ApellidoPaterno { get; set; } = null!;

    public string? ApellidoMaterno { get; set; }

    public string Email { get; set; } = null!;

    public string? Password { get; set; }

    public int TipoDocumento { get; set; }

    public string NumeroDocumento { get; set; } = null!;

    public string Telefono { get; set; } = null!;

    public string AuditoriaUsuarioIngreso { get; set; } = null!;

    public DateTime AuditoriaFechaIngreso { get; set; }

    public string? AuditoriaUsuarioModificacion { get; set; }

    public DateTime? AuditoriaFechaModificacion { get; set; }

    }

}

namespace Api.Rifamos.BackEnd.Adapter{

public class RiferoFrontDTO : ErrorDTO {
    public int RiferoId { get; set; }

    public string Email { get; set; } = null!;

    public string Nombres { get; set; } = null!;

    public string ApellidoPaterno { get; set; } = null!;

    public string? ApellidoMaterno { get; set; }

    public int? TipoDocumento { get; set; }

    public string NumeroDocumento { get; set; } = null!;

    public string Telefono { get; set; } = null!;
    
    public string? Token { get; set; }
    }

}
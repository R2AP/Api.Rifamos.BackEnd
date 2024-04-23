using System;
using System.Collections.Generic;

namespace Api.Rifamos.BackEnd.Adapter{

public class OpcionFrontDTO{
    public int OpcionId { get; set; }

    public int RifaId { get; set; }

    public int UsuarioId { get; set; }

    public int CantidadOpciones { get; set; }

    public string? TokenOpcion { get; set; }

    public int EstadoOpcion { get; set; }

    public string AuditoriaUsuarioIngreso { get; set; } = null!;

    public DateTime AuditoriaFechaIngreso { get; set; }

    public string? AuditoriaUsuarioModificacion { get; set; }

    public DateTime? AuditoriaFechaModificacion { get; set; }

    }
}

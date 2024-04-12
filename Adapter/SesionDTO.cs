using System;
using System.Collections.Generic;

namespace Api.Rifamos.BackEnd.Adapter{

public class SesionDTO{
    public int SesionId { get; set; }

    public int UsuarioId { get; set; }

    public int TipoEvento { get; set; }

    public string Ip { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string AuditoriaUsuarioIngreso { get; set; } = null!;

    public DateTime AuditoriaFechaIngreso { get; set; }

    public string? AuditoriaUsuarioModificacion { get; set; }

    public DateTime? AuditoriaFechaModificacion { get; set; }

    }

}

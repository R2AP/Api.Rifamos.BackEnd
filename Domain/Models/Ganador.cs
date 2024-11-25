using System;
using System.Collections.Generic;

namespace Api.Rifamos.BackEnd.Domain.Models;

public partial class Ganador
{
    public int GanadorId { get; set; }

    public int PremioId { get; set; }

    public int RiferoId { get; set; }

    public string AuditoriaUsuarioIngreso { get; set; } = null!;

    public DateTime AuditoriaFechaIngreso { get; set; }

    public string? AuditoriaUsuarioModificacion { get; set; }

    public DateTime? AuditoriaFechaModificacion { get; set; }

    public virtual Premio Premio { get; set; } = null!;

    public virtual Rifero Rifero { get; set; } = null!;
}

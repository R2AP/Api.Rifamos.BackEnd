using System;
using System.Collections.Generic;

namespace Api.Rifamos.BackEnd.Domain.Models;

public partial class Precio
{
    public int PrecioId { get; set; }

    public int RifaId { get; set; }

    public decimal PrecioUnitario { get; set; }

    public string AuditoriaUsuarioIngreso { get; set; } = null!;

    public DateTime AuditoriaFechaIngreso { get; set; }

    public string? AuditoriaUsuarioModificacion { get; set; }

    public DateTime? AuditoriaFechaModificacion { get; set; }

    public virtual Rifa Rifa { get; set; } = null!;
}

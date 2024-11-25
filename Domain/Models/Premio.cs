using System;
using System.Collections.Generic;

namespace Api.Rifamos.BackEnd.Domain.Models;

public partial class Premio
{
    public int PremioId { get; set; }

    public int RifaId { get; set; }

    public string PremioDescripcion { get; set; } = null!;

    public string PremioDetalle { get; set; } = null!;

    public string Url { get; set; } = null!;

    public byte[] Imagen { get; set; } = null!;

    public byte[] ImagenCorta { get; set; } = null!;

    public string AuditoriaUsuarioIngreso { get; set; } = null!;

    public DateTime AuditoriaFechaIngreso { get; set; }

    public string? AuditoriaUsuarioModificacion { get; set; }

    public DateTime? AuditoriaFechaModificacion { get; set; }

    public virtual ICollection<Ganador> Ganadors { get; set; } = new List<Ganador>();

    public virtual Rifa Rifa { get; set; } = null!;
}

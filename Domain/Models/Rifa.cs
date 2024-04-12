using System;
using System.Collections.Generic;

namespace Api.Rifamos.BackEnd.Domain.Models;

public partial class Rifa
{
    public int RifaId { get; set; }

    public string RifaDescripcion { get; set; } = null!;

    public DateOnly FechaSorteo { get; set; }

    public TimeOnly HoraSorteo { get; set; }

    public byte[]? Imagen { get; set; }

    public string Sponsor { get; set; } = null!;

    public int EstadoRifa { get; set; }

    public string AuditoriaUsuarioIngreso { get; set; } = null!;

    public DateTime AuditoriaFechaIngreso { get; set; }

    public string? AuditoriaUsuarioModificacion { get; set; }

    public DateTime? AuditoriaFechaModificacion { get; set; }

    public virtual EstadoRifa EstadoRifaNavigation { get; set; } = null!;

    public virtual ICollection<Opcion> Opcions { get; set; } = new List<Opcion>();

    public virtual ICollection<Precio> Precios { get; set; } = new List<Precio>();

    public virtual ICollection<Premio> Premios { get; set; } = new List<Premio>();
}

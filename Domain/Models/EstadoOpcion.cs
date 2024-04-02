using System;
using System.Collections.Generic;

namespace Api.Rifamos.BackEnd.Domain.Models;

public partial class EstadoOpcion
{
    public string CodigoEstadoOpcion { get; set; } = null!;

    public string DescripcionEstadoOpcion { get; set; } = null!;

    public string AuditoriaUsuarioIngreso { get; set; } = null!;

    public DateTime AuditoriaFechaIngreso { get; set; }

    public string? AuditoriaUsuarioModificacion { get; set; }

    public DateTime? AuditoriaFechaModificacion { get; set; }

    public virtual ICollection<Opcion> Opcions { get; set; } = new List<Opcion>();
}

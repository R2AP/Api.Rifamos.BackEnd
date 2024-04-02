using System;
using System.Collections.Generic;

namespace Api.Rifamos.BackEnd.Domain.Models;

public partial class Opcion
{
    public int OpcionId { get; set; }

    public int RifaId { get; set; }

    public int UsuarioId { get; set; }

    public int CantidadOpciones { get; set; }

    public string TokenOpcion { get; set; } = null!;

    public string CodigoEstadoOpcion { get; set; } = null!;

    public string AuditoriaUsuarioIngreso { get; set; } = null!;

    public DateTime AuditoriaFechaIngreso { get; set; }

    public string? AuditoriaUsuarioModificacion { get; set; }

    public DateTime? AuditoriaFechaModificacion { get; set; }

    public virtual EstadoOpcion CodigoEstadoOpcionNavigation { get; set; } = null!;

    public virtual Rifa Rifa { get; set; } = null!;

    public virtual Usuario Usuario { get; set; } = null!;

    public virtual Ventum? Ventum { get; set; }
}

using System;
using System.Collections.Generic;

namespace Api.Rifamos.BackEnd.Domain.Models;

/// <summary>
/// Tipo de Documento de Identidad
/// </summary>
public partial class TipoDocumento
{
    public int TipoDocumentoId { get; set; }

    public string DescripcionTipoDocumento { get; set; } = null!;

    public string AuditoriaUsuarioIngreso { get; set; } = null!;

    public DateTime AuditoriaFechaIngreso { get; set; }

    public string? AuditoriaUsuarioModificacion { get; set; }

    public DateTime? AuditoriaFechaModificacion { get; set; }

    public virtual ICollection<Rifero> Riferos { get; set; } = new List<Rifero>();

    public virtual ICollection<Usuario> Usuarios { get; set; } = new List<Usuario>();
}

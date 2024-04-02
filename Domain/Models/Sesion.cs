using System;
using System.Collections.Generic;

namespace Api.Rifamos.BackEnd.Domain.Models;

public partial class Sesion
{
    public int SesionId { get; set; }

    public int UsuarioId { get; set; }

    public string CodigoTipoEvento { get; set; } = null!;

    public string Ip { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string AuditoriaUsuarioIngreso { get; set; } = null!;

    public DateTime AuditoriaFechaIngreso { get; set; }

    public string? AuditoriaUsuarioModificacion { get; set; }

    public DateTime? AuditoriaFechaModificacion { get; set; }

    public virtual TipoEvento CodigoTipoEventoNavigation { get; set; } = null!;

    public virtual Usuario Usuario { get; set; } = null!;
}

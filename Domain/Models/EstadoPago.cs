using System;
using System.Collections.Generic;

namespace Api.Rifamos.BackEnd.Domain.Models;

public partial class EstadoPago
{
    public string CodigoEstadoPago { get; set; } = null!;

    public string DescripcionEstadoPago { get; set; } = null!;

    public string AuditoriaUsuarioIngreso { get; set; } = null!;

    public DateTime AuditoriaFechaIngreso { get; set; }

    public string? AuditoriaUsuarioModificacion { get; set; }

    public DateTime? AuditoriaFechaModificacion { get; set; }

    public virtual Pago CodigoEstadoPagoNavigation { get; set; } = null!;
}

using System;
using System.Collections.Generic;

namespace Api.Rifamos.BackEnd.Domain.Models;

/// <summary>
/// 1. Tarjetas de crédito o débito.
/// 2. Pagos en efectivo.
/// 3. Transferencias bancarias
/// </summary>
public partial class TipoPago
{
    public int TipoPagoId { get; set; }

    public string DescripcionTipoPago { get; set; } = null!;

    public string AuditoriaUsuarioIngreso { get; set; } = null!;

    public DateTime AuditoriaFechaIngreso { get; set; }

    public string? AuditoriaUsuarioModificacion { get; set; }

    public DateTime? AuditoriaFechaModificacion { get; set; }

    public virtual ICollection<Pago> Pagos { get; set; } = new List<Pago>();
}

using System;
using System.Collections.Generic;

namespace Api.Rifamos.BackEnd.Domain.Models;

public partial class Pago
{
    public int PagoId { get; set; }

    public int VentaId { get; set; }

    public string CodigoTipoPago { get; set; } = null!;

    /// <summary>
    /// Código ofrecido por la pasarela
    /// </summary>
    public string CodigoTransaccion { get; set; } = null!;

    public DateOnly FechaPago { get; set; }

    public TimeOnly HoraPago { get; set; }

    public string Moneda { get; set; } = null!;

    public decimal Monto { get; set; }

    public string CodigoEstadoPago { get; set; } = null!;

    public string AuditoriaUsuarioIngreso { get; set; } = null!;

    public DateTime AuditoriaFechaIngreso { get; set; }

    public string? AuditoriaUsuarioModificacion { get; set; }

    public DateTime? AuditoriaFechaModificacion { get; set; }

    public virtual TipoPago CodigoTipoPagoNavigation { get; set; } = null!;

    public virtual EstadoPago? EstadoPago { get; set; }

    public virtual Ventum Venta { get; set; } = null!;
}

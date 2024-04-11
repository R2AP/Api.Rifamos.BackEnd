using System;
using System.Collections.Generic;

namespace Api.Rifamos.BackEnd.Domain.Models;

public partial class Pago
{
    public int PagoId { get; set; }

    public int VentaId { get; set; }

    public int TipoPago { get; set; }

    /// <summary>
    /// Código ofrecido por la pasarela
    /// </summary>
    public string CodigoTransaccion { get; set; } = null!;

    public DateOnly FechaPago { get; set; }

    public TimeOnly HoraPago { get; set; }

    public string Moneda { get; set; } = null!;

    public decimal Monto { get; set; }

    public int EstadoPago { get; set; }

    public string AuditoriaUsuarioIngreso { get; set; } = null!;

    public DateTime AuditoriaFechaIngreso { get; set; }

    public string? AuditoriaUsuarioModificacion { get; set; }

    public DateTime? AuditoriaFechaModificacion { get; set; }

    public virtual EstadoPago EstadoPagoNavigation { get; set; } = null!;

    public virtual TipoPago TipoPagoNavigation { get; set; } = null!;

    public virtual Ventum Venta { get; set; } = null!;
}

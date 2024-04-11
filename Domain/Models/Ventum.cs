using System;
using System.Collections.Generic;

namespace Api.Rifamos.BackEnd.Domain.Models;

public partial class Ventum
{
    public int VentaId { get; set; }

    public int OpcionId { get; set; }

    public int TipoComprobante { get; set; }

    public string SerieComprobante { get; set; } = null!;

    public string NumeroComprobante { get; set; } = null!;

    public int Moneda { get; set; }

    public decimal Monto { get; set; }

    public int EstadoVenta { get; set; }

    public string AuditoriaUsuarioIngreso { get; set; } = null!;

    public DateTime AuditoriaFechaIngreso { get; set; }

    public string? AuditoriaUsuarioModificacion { get; set; }

    public DateTime? AuditoriaFechaModificacion { get; set; }

    public virtual EstadoVentum EstadoVentaNavigation { get; set; } = null!;

    public virtual Monedum MonedaNavigation { get; set; } = null!;

    public virtual ICollection<Pago> Pagos { get; set; } = new List<Pago>();

    public virtual TipoComprobante TipoComprobanteNavigation { get; set; } = null!;

    public virtual Opcion Venta { get; set; } = null!;
}

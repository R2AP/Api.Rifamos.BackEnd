﻿using System;
using System.Collections.Generic;

namespace Api.Rifamos.BackEnd.Domain.Models;

public partial class Ventum
{
    public int VentaId { get; set; }

    public int OpcionId { get; set; }

    public string TipoComprobante { get; set; } = null!;

    public string SerieComprobante { get; set; } = null!;

    public string NumeroComprobante { get; set; } = null!;

    public string Moneda { get; set; } = null!;

    public decimal Monto { get; set; }

    public string CodigoEstadoVenta { get; set; } = null!;

    public string AuditoriaUsuarioIngreso { get; set; } = null!;

    public DateTime AuditoriaFechaIngreso { get; set; }

    public string? AuditoriaUsuarioModificacion { get; set; }

    public DateTime? AuditoriaFechaModificacion { get; set; }

    public virtual EstadoVentum CodigoEstadoVentaNavigation { get; set; } = null!;

    public virtual ICollection<Pago> Pagos { get; set; } = new List<Pago>();

    public virtual Opcion Venta { get; set; } = null!;
}

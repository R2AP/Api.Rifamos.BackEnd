using System;
using System.Collections.Generic;

namespace Api.Rifamos.BackEnd.Adapter{

public class VentaDTO{
    public int VentaId { get; set; }

    public int OpcionId { get; set; }

    public string TipoComprobante { get; set; } = null!;

    public string SerieComprobante { get; set; } = null!;

    public string NumeroComprobante { get; set; } = null!;

    public string Moneda { get; set; } = null!;

    public decimal Monto { get; set; }

    public int EstadoVenta { get; set; }

    public string AuditoriaUsuarioIngreso { get; set; } = null!;

    public DateTime AuditoriaFechaIngreso { get; set; }

    public string? AuditoriaUsuarioModificacion { get; set; }

    public DateTime? AuditoriaFechaModificacion { get; set; }

    }

}

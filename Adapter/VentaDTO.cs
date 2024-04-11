using System;
using System.Collections.Generic;

namespace Api.Rifamos.BackEnd.Adapter{

public class VentaDTO{
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

    }

}

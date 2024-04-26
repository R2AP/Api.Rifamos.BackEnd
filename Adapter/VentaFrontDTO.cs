using System;
using System.Collections.Generic;

namespace Api.Rifamos.BackEnd.Adapter{

public class VentaFrontDTO{

    public int OpcionId { get; set; }

    public int RifaId { get; set; }

    public int UsuarioId { get; set; }

    public int CantidadOpciones { get; set; }

    public string? TokenOpcion { get; set; }

    public int EstadoOpcion { get; set; }

    public int VentaId { get; set; }

    public int TipoComprobante { get; set; }

    public string SerieComprobante { get; set; } = null!;

    public string NumeroComprobante { get; set; } = null!;

    public int Moneda { get; set; }

    public decimal Monto { get; set; }

    public int EstadoVenta { get; set; }

    }

}

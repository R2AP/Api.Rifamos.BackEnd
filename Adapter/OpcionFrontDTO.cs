using System;
using System.Collections.Generic;

namespace Api.Rifamos.BackEnd.Adapter{

public class OpcionFrontDTO : ErrorDTO{
    public int OpcionId { get; set; }

    public int RifaId { get; set; }

    public int UsuarioId { get; set; }

    public int CantidadOpciones { get; set; }

    public string? TokenOpcion { get; set; }

    public int EstadoOpcion { get; set; }

    }
}

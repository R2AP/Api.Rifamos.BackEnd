using System;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Api.Rifamos.BackEnd.Adapter{

public class RifaFrontDTO : ErrorDTO{
    public int RifaId { get; set; }

    public string RifaDescripcion { get; set; } = null!;

    public DateOnly FechaSorteo { get; set; }

    public TimeOnly HoraSorteo { get; set; }

    public byte[]? Imagen { get; set; }
    
    public string Sponsor { get; set; } = null!;

    public int EstadoRifa { get; set; }

    }
}


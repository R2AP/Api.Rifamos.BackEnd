using System;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Api.Rifamos.BackEnd.Adapter{

public class RifaDTO{
    public int RifaId { get; set; }

    public string RifaDescripcion { get; set; } = null!;
    
    public string IndicadorPremium { get; set; } = null!;
    
    public string RifaDetalle { get; set; } = null!;

    public DateOnly FechaSorteo { get; set; }

    public TimeOnly HoraSorteo { get; set; }

    public string Sponsor { get; set; } = null!;

    public int EstadoRifa { get; set; }

    public string AuditoriaUsuario { get; set; } = null!;

    }
}


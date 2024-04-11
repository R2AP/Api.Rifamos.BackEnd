using System;
using System.Collections;
using System.Collections.Generic;

namespace Api.Rifamos.BackEnd.Adapter{

public class RifaDTO{
    public int RifaId { get; set; }

    public string RifaDescripcion { get; set; } = null!;

    public DateOnly FechaSorteo { get; set; }

    public TimeOnly HoraSorteo { get; set; }

    public BitArray? Imagen { get; set; }

    public string Sponsor { get; set; } = null!;

    public int EstadoRifa { get; set; }

    public string AuditoriaUsuarioIngreso { get; set; } = null!;

    public DateTime AuditoriaFechaIngreso { get; set; }

    public string? AuditoriaUsuarioModificacion { get; set; }

    public DateTime? AuditoriaFechaModificacion { get; set; }

    }
}


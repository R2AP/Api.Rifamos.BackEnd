using System;
using System.Collections.Generic;

namespace Api.Rifamos.BackEnd.Adapter{

public class PrecioDTO{
    public int PrecioId { get; set; }

    public int RifaId { get; set; }

    public decimal PrecioUnitario { get; set; }

    public string AuditoriaUsuario { get; set; } = null!;

    }

}


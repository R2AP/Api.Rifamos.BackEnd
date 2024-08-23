using System;
using System.Collections;
using System.Collections.Generic;

namespace Api.Rifamos.BackEnd.Adapter{

public class PremioDTO{
    public int PremioId { get; set; }

    public int RifaId { get; set; }

    public string PremioDescripcion { get; set; } = null!;

    public string PremioDetalle { get; set; } = null!;

    public string Url { get; set; } = null!;

    public byte[]? Imagen { get; set; }

    public string AuditoriaUsuario { get; set; } = null!;

    }
}


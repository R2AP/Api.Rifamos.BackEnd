using System;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Api.Rifamos.BackEnd.Adapter{

public class RifaFrontFechaDTO {
    public int Anho { get; set; }

    public int  Mes { get; set; }

    public int Dia { get; set; }

    public List<RifaFrontDTO> ListRifa { get; set; } = null!;

    }
}


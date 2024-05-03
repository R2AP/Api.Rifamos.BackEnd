using System;
using System.Collections.Generic;

namespace Api.Rifamos.BackEnd.Adapter{

public class SesionDTO{


    public int SesionId { get; set; }

    public string Email { get; set; } = null!;

    public string Ip { get; set; } = null!;

    public int TipoEvento { get; set; }

    public DateTime FechaEvento { get; set; }
    
    }

}

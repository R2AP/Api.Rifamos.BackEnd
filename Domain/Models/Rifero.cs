using System;
using System.Collections.Generic;

namespace Api.Rifamos.BackEnd.Domain.Models;

/// <summary>
/// Cliente de RifamosTodo.Online
/// </summary>
public partial class Rifero
{
    public int RiferoId { get; set; }

    public string Email { get; set; } = null!;

    public string? Password { get; set; }

    public string? Key1 { get; set; }

    public string? Key2 { get; set; }

    public string? Nombre { get; set; }

    public string? ApellidoPaterno { get; set; }

    public string? ApellidoMaterno { get; set; }

    public int? TipoDocumentoId { get; set; }

    public string? NumeroDocumentoIdentidad { get; set; }

    public string? Telefono { get; set; }

    public DateTime? AuditoriaFechaIngreso { get; set; }

    public string? AuditoriaUsuarioModificacion { get; set; }

    public DateTime? AuditoriaFechaModificacion { get; set; }

    public virtual ICollection<Ganador> Ganadors { get; set; } = new List<Ganador>();

    public virtual ICollection<Opcion> Opcions { get; set; } = new List<Opcion>();

    public virtual TipoDocumento? TipoDocumento { get; set; }
}

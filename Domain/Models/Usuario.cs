﻿using System;
using System.Collections.Generic;

namespace Api.Rifamos.BackEnd.Domain.Models;

public partial class Usuario
{
    public int UsuarioId { get; set; }

    public string Nombres { get; set; } = null!;

    public string ApellidoPaterno { get; set; } = null!;

    public string? ApellidoMaterno { get; set; }

    public string Email { get; set; } = null!;

    public string CodigoTipoDocumento { get; set; } = null!;

    public string NumeroDocumento { get; set; } = null!;

    public string Telefono { get; set; } = null!;

    public string AuditoriaUsuarioIngreso { get; set; } = null!;

    public DateTime AuditoriaFechaIngreso { get; set; }

    public string? AuditoriaUsuarioModificacion { get; set; }

    public DateTime? AuditoriaFechaModificacion { get; set; }

    public virtual TipoDocumento CodigoTipoDocumentoNavigation { get; set; } = null!;

    public virtual ICollection<Opcion> Opcions { get; set; } = new List<Opcion>();

    public virtual ICollection<Sesion> Sesions { get; set; } = new List<Sesion>();
}

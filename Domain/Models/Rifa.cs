using System;
using System.Collections;
using System.Collections.Generic;

namespace Api.Rifamos.BackEnd.Domain.Models;

public partial class Rifa
{

    public Rifa(){
        // RifaId =  RifaDTO.RifaId,
        // RifaDescripcion =  RifaDTO.RifaDescripcion,
        // FechaSorteo =  RifaDTO.FechaSorteo,
        // HoraSorteo =  RifaDTO.HoraSorteo,
        // Imagen =  RifaDTO.Imagen,
        // Sponsor =  RifaDTO.Sponsor,
        // EstadoRifa =  RifaDTO.EstadoRifa,
        // AuditoriaUsuarioIngreso =  RifaDTO.AuditoriaUsuarioIngreso,
        // AuditoriaFechaIngreso =  RifaDTO.AuditoriaFechaIngreso
    }

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

    public virtual EstadoRifa EstadoRifaNavigation { get; set; } = null!;

    public virtual ICollection<Opcion> Opcions { get; set; } = new List<Opcion>();

    public virtual ICollection<Precio> Precios { get; set; } = new List<Precio>();

    public virtual ICollection<Premio> Premios { get; set; } = new List<Premio>();
}

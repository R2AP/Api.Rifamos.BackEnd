using System;
using System.Collections.Generic;
using Api.Rifamos.BackEnd.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Api.Rifamos.BackEnd.Persistence.Contexts;

public partial class RifamosContext : DbContext
{
    public RifamosContext()
    {
    }

    public RifamosContext(DbContextOptions<RifamosContext> options)
        : base(options)
    {
    }

    public virtual DbSet<EstadoOpcion> EstadoOpcions { get; set; }

    public virtual DbSet<EstadoPago> EstadoPagos { get; set; }

    public virtual DbSet<EstadoRifa> EstadoRifas { get; set; }

    public virtual DbSet<EstadoVentum> EstadoVenta { get; set; }

    public virtual DbSet<Monedum> Moneda { get; set; }

    public virtual DbSet<Opcion> Opcions { get; set; }

    public virtual DbSet<Pago> Pagos { get; set; }

    public virtual DbSet<Precio> Precios { get; set; }

    public virtual DbSet<Premio> Premios { get; set; }

    public virtual DbSet<Rifa> Rifas { get; set; }

    public virtual DbSet<Sesion> Sesions { get; set; }

    public virtual DbSet<TipoComprobante> TipoComprobantes { get; set; }

    public virtual DbSet<TipoDocumento> TipoDocumentos { get; set; }

    public virtual DbSet<TipoEvento> TipoEventos { get; set; }

    public virtual DbSet<TipoPago> TipoPagos { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    public virtual DbSet<Ventum> Venta { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Server=localhost;Database=rifamos;User Id= postgres;Password=P132639%Rrap.2024;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<EstadoOpcion>(entity =>
        {
            entity.HasKey(e => e.EstadoOpcionId).HasName("EstadoOpcion_pkey");

            entity.ToTable("EstadoOpcion");

            entity.Property(e => e.EstadoOpcionId).HasColumnName("EstadoOpcionID");
            entity.Property(e => e.AuditoriaFechaIngreso).HasColumnType("timestamp without time zone");
            entity.Property(e => e.AuditoriaFechaModificacion).HasColumnType("timestamp without time zone");
            entity.Property(e => e.AuditoriaUsuarioIngreso).HasMaxLength(64);
            entity.Property(e => e.AuditoriaUsuarioModificacion).HasMaxLength(64);
            entity.Property(e => e.DescripcionEstadoOpcion).HasMaxLength(128);
        });

        modelBuilder.Entity<EstadoPago>(entity =>
        {
            entity.HasKey(e => e.EstadoPagoId).HasName("EstadoPago_pkey");

            entity.ToTable("EstadoPago");

            entity.Property(e => e.EstadoPagoId).HasColumnName("EstadoPagoID");
            entity.Property(e => e.AuditoriaFechaIngreso).HasColumnType("timestamp without time zone");
            entity.Property(e => e.AuditoriaFechaModificacion).HasColumnType("timestamp without time zone");
            entity.Property(e => e.AuditoriaUsuarioIngreso).HasMaxLength(64);
            entity.Property(e => e.AuditoriaUsuarioModificacion).HasMaxLength(64);
            entity.Property(e => e.DescripcionEstadoPago).HasMaxLength(128);
        });

        modelBuilder.Entity<EstadoRifa>(entity =>
        {
            entity.HasKey(e => e.EstadoRifaId).HasName("EstadoRifa_pkey");

            entity.ToTable("EstadoRifa");

            entity.Property(e => e.EstadoRifaId).HasColumnName("EstadoRifaID");
            entity.Property(e => e.AuditoriaFechaIngreso).HasColumnType("timestamp without time zone");
            entity.Property(e => e.AuditoriaFechaModificacion).HasColumnType("timestamp without time zone");
            entity.Property(e => e.AuditoriaUsuarioIngreso).HasMaxLength(64);
            entity.Property(e => e.AuditoriaUsuarioModificacion).HasMaxLength(64);
            entity.Property(e => e.DescripcionEstadoRifa).HasMaxLength(128);
        });

        modelBuilder.Entity<EstadoVentum>(entity =>
        {
            entity.HasKey(e => e.EstadoVentaId).HasName("EstadoVenta_pkey");

            entity.Property(e => e.EstadoVentaId).HasColumnName("EstadoVentaID");
            entity.Property(e => e.AuditoriaFechaIngreso).HasColumnType("timestamp without time zone");
            entity.Property(e => e.AuditoriaFechaModificacion).HasColumnType("timestamp without time zone");
            entity.Property(e => e.AuditoriaUsuarioIngreso).HasMaxLength(64);
            entity.Property(e => e.AuditoriaUsuarioModificacion).HasMaxLength(64);
            entity.Property(e => e.DescripcionEstadoVenta).HasMaxLength(128);
        });

        modelBuilder.Entity<Monedum>(entity =>
        {
            entity.HasKey(e => e.MonedaId).HasName("Moneda_pkey");

            entity.Property(e => e.MonedaId).HasColumnName("MonedaID");
            entity.Property(e => e.AuditoriaFechaIngreso).HasColumnType("timestamp without time zone");
            entity.Property(e => e.AuditoriaFechaModificacion).HasColumnType("timestamp without time zone");
            entity.Property(e => e.AuditoriaUsuarioIngreso).HasMaxLength(64);
            entity.Property(e => e.AuditoriaUsuarioModificacion).HasMaxLength(64);
            entity.Property(e => e.DescripcionMoneda).HasMaxLength(128);
        });

        modelBuilder.Entity<Opcion>(entity =>
        {
            entity.HasKey(e => e.OpcionId).HasName("Opcion_pkey");

            entity.ToTable("Opcion");

            entity.Property(e => e.OpcionId).HasColumnName("OpcionID");
            entity.Property(e => e.AuditoriaFechaIngreso).HasColumnType("timestamp without time zone");
            entity.Property(e => e.AuditoriaFechaModificacion).HasColumnType("timestamp without time zone");
            entity.Property(e => e.AuditoriaUsuarioIngreso).HasMaxLength(64);
            entity.Property(e => e.AuditoriaUsuarioModificacion).HasMaxLength(64);
            entity.Property(e => e.RifaId).HasColumnName("RifaID");
            entity.Property(e => e.TokenKey1).HasMaxLength(128);
            entity.Property(e => e.TokenKey2).HasMaxLength(128);
            entity.Property(e => e.TokenOpcion).HasMaxLength(128);
            entity.Property(e => e.UsuarioId).HasColumnName("UsuarioID");

            entity.HasOne(d => d.EstadoOpcionNavigation).WithMany(p => p.Opcions)
                .HasForeignKey(d => d.EstadoOpcion)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Opcion_EstadoOpcion_fkey");

            entity.HasOne(d => d.Rifa).WithMany(p => p.Opcions)
                .HasForeignKey(d => d.RifaId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Opcion_RifaID_fkey");

            entity.HasOne(d => d.Usuario).WithMany(p => p.Opcions)
                .HasForeignKey(d => d.UsuarioId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Opcion_UsuarioID_fkey");
        });

        modelBuilder.Entity<Pago>(entity =>
        {
            entity.HasKey(e => e.PagoId).HasName("Pago_pkey");

            entity.ToTable("Pago");

            entity.Property(e => e.PagoId).HasColumnName("PagoID");
            entity.Property(e => e.AuditoriaFechaIngreso).HasColumnType("timestamp without time zone");
            entity.Property(e => e.AuditoriaFechaModificacion).HasColumnType("timestamp without time zone");
            entity.Property(e => e.AuditoriaUsuarioIngreso).HasMaxLength(64);
            entity.Property(e => e.AuditoriaUsuarioModificacion).HasMaxLength(64);
            entity.Property(e => e.CodigoTransaccion)
                .HasMaxLength(32)
                .HasComment("Código ofrecido por la pasarela");
            entity.Property(e => e.Moneda).HasMaxLength(8);
            entity.Property(e => e.Monto).HasPrecision(18, 2);
            entity.Property(e => e.VentaId).HasColumnName("VentaID");

            entity.HasOne(d => d.EstadoPagoNavigation).WithMany(p => p.Pagos)
                .HasForeignKey(d => d.EstadoPago)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Pago_EstadoPago_fkey");

            entity.HasOne(d => d.TipoPagoNavigation).WithMany(p => p.Pagos)
                .HasForeignKey(d => d.TipoPago)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Pago_TipoPago_fkey");

            entity.HasOne(d => d.Venta).WithMany(p => p.Pagos)
                .HasForeignKey(d => d.VentaId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Pago_VentaID_fkey");
        });

        modelBuilder.Entity<Precio>(entity =>
        {
            entity.HasKey(e => e.PrecioId).HasName("Precio_pkey");

            entity.ToTable("Precio");

            entity.Property(e => e.PrecioId).HasColumnName("PrecioID");
            entity.Property(e => e.AuditoriaFechaIngreso).HasColumnType("timestamp without time zone");
            entity.Property(e => e.AuditoriaFechaModificacion).HasColumnType("timestamp without time zone");
            entity.Property(e => e.AuditoriaUsuarioIngreso).HasMaxLength(64);
            entity.Property(e => e.AuditoriaUsuarioModificacion).HasMaxLength(64);
            entity.Property(e => e.PrecioUnitario).HasPrecision(18, 2);
            entity.Property(e => e.RifaId).HasColumnName("RifaID");

            entity.HasOne(d => d.Rifa).WithMany(p => p.Precios)
                .HasForeignKey(d => d.RifaId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Precio_RifaID_fkey");
        });

        modelBuilder.Entity<Premio>(entity =>
        {
            entity.HasKey(e => e.PremioId).HasName("Premio_pkey");

            entity.ToTable("Premio");

            entity.Property(e => e.PremioId).HasColumnName("PremioID");
            entity.Property(e => e.AuditoriaFechaIngreso).HasColumnType("timestamp without time zone");
            entity.Property(e => e.AuditoriaFechaModificacion).HasColumnType("timestamp without time zone");
            entity.Property(e => e.AuditoriaUsuarioIngreso).HasMaxLength(64);
            entity.Property(e => e.AuditoriaUsuarioModificacion).HasMaxLength(64);
            entity.Property(e => e.PremioDescripcion).HasMaxLength(128);
            entity.Property(e => e.PremioDetalle).HasMaxLength(256);
            entity.Property(e => e.RifaId).HasColumnName("RifaID");
            entity.Property(e => e.Url).HasMaxLength(512);

            entity.HasOne(d => d.Rifa).WithMany(p => p.Premios)
                .HasForeignKey(d => d.RifaId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Premio_RifaID_fkey");
        });

        modelBuilder.Entity<Rifa>(entity =>
        {
            entity.HasKey(e => e.RifaId).HasName("Rifa_pkey");

            entity.ToTable("Rifa");

            entity.Property(e => e.RifaId).HasColumnName("RifaID");
            entity.Property(e => e.AuditoriaFechaIngreso).HasColumnType("timestamp without time zone");
            entity.Property(e => e.AuditoriaFechaModificacion).HasColumnType("timestamp without time zone");
            entity.Property(e => e.AuditoriaUsuarioIngreso).HasMaxLength(64);
            entity.Property(e => e.AuditoriaUsuarioModificacion).HasMaxLength(64);
            entity.Property(e => e.RifaDescripcion).HasMaxLength(128);
            entity.Property(e => e.Sponsor).HasMaxLength(128);

            entity.HasOne(d => d.EstadoRifaNavigation).WithMany(p => p.Rifas)
                .HasForeignKey(d => d.EstadoRifa)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Rifa_EstadoRifa_fkey");
        });

        modelBuilder.Entity<Sesion>(entity =>
        {
            entity.HasKey(e => e.SesionId).HasName("Sesion_pkey");

            entity.ToTable("Sesion");

            entity.Property(e => e.SesionId).HasColumnName("SesionID");
            entity.Property(e => e.AuditoriaFechaIngreso).HasColumnType("timestamp without time zone");
            entity.Property(e => e.AuditoriaFechaModificacion).HasColumnType("timestamp without time zone");
            entity.Property(e => e.AuditoriaUsuarioIngreso).HasMaxLength(64);
            entity.Property(e => e.AuditoriaUsuarioModificacion).HasMaxLength(64);
            entity.Property(e => e.Email).HasMaxLength(64);
            entity.Property(e => e.Ip)
                .HasMaxLength(16)
                .HasColumnName("IP");
            entity.Property(e => e.UsuarioId).HasColumnName("UsuarioID");

            entity.HasOne(d => d.TipoEventoNavigation).WithMany(p => p.Sesions)
                .HasForeignKey(d => d.TipoEvento)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Sesion_TipoEvento_fkey");

            entity.HasOne(d => d.Usuario).WithMany(p => p.Sesions)
                .HasForeignKey(d => d.UsuarioId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Sesion_UsuarioID_fkey");
        });

        modelBuilder.Entity<TipoComprobante>(entity =>
        {
            entity.HasKey(e => e.TipoComprobanteId).HasName("TipoComprobante_pkey");

            entity.ToTable("TipoComprobante");

            entity.Property(e => e.TipoComprobanteId).HasColumnName("TipoComprobanteID");
            entity.Property(e => e.AuditoriaFechaIngreso).HasColumnType("timestamp without time zone");
            entity.Property(e => e.AuditoriaFechaModificacion).HasColumnType("timestamp without time zone");
            entity.Property(e => e.AuditoriaUsuarioIngreso).HasMaxLength(64);
            entity.Property(e => e.AuditoriaUsuarioModificacion).HasMaxLength(64);
            entity.Property(e => e.DescripcionComprobante).HasMaxLength(128);
        });

        modelBuilder.Entity<TipoDocumento>(entity =>
        {
            entity.HasKey(e => e.TipoDocumentoId).HasName("TipoDocumento_pkey");

            entity.ToTable("TipoDocumento");

            entity.Property(e => e.TipoDocumentoId).HasColumnName("TipoDocumentoID");
            entity.Property(e => e.AuditoriaFechaIngreso).HasColumnType("timestamp without time zone");
            entity.Property(e => e.AuditoriaFechaModificacion).HasColumnType("timestamp without time zone");
            entity.Property(e => e.AuditoriaUsuarioIngreso).HasMaxLength(64);
            entity.Property(e => e.AuditoriaUsuarioModificacion).HasMaxLength(64);
            entity.Property(e => e.DescripcionTipoDocumento).HasMaxLength(128);
        });

        modelBuilder.Entity<TipoEvento>(entity =>
        {
            entity.HasKey(e => e.TipoEventoId).HasName("TipoEvento_pkey");

            entity.ToTable("TipoEvento");

            entity.Property(e => e.TipoEventoId).HasColumnName("TipoEventoID");
            entity.Property(e => e.AuditoriaFechaIngreso).HasColumnType("timestamp without time zone");
            entity.Property(e => e.AuditoriaFechaModificacion).HasColumnType("timestamp without time zone");
            entity.Property(e => e.AuditoriaUsuarioIngreso).HasMaxLength(64);
            entity.Property(e => e.AuditoriaUsuarioModificacion).HasMaxLength(64);
            entity.Property(e => e.DescripcionTipoEvento).HasMaxLength(128);
        });

        modelBuilder.Entity<TipoPago>(entity =>
        {
            entity.HasKey(e => e.TipoPagoId).HasName("TipoPago_pkey");

            entity.ToTable("TipoPago", tb => tb.HasComment("1. Tarjetas de crédito o débito.\r\n2. Pagos en efectivo.\r\n3. Transferencias bancarias"));

            entity.Property(e => e.TipoPagoId).HasColumnName("TipoPagoID");
            entity.Property(e => e.AuditoriaFechaIngreso).HasColumnType("timestamp without time zone");
            entity.Property(e => e.AuditoriaFechaModificacion).HasColumnType("timestamp without time zone");
            entity.Property(e => e.AuditoriaUsuarioIngreso).HasMaxLength(64);
            entity.Property(e => e.AuditoriaUsuarioModificacion).HasMaxLength(64);
            entity.Property(e => e.DescripcionTipoPago).HasMaxLength(128);
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.UsuarioId).HasName("Usuario_pkey");

            entity.ToTable("Usuario");

            entity.Property(e => e.UsuarioId).HasColumnName("UsuarioID");
            entity.Property(e => e.ApellidoMaterno).HasMaxLength(128);
            entity.Property(e => e.ApellidoPaterno).HasMaxLength(128);
            entity.Property(e => e.AuditoriaFechaIngreso).HasColumnType("timestamp without time zone");
            entity.Property(e => e.AuditoriaFechaModificacion).HasColumnType("timestamp without time zone");
            entity.Property(e => e.AuditoriaUsuarioIngreso).HasMaxLength(64);
            entity.Property(e => e.AuditoriaUsuarioModificacion).HasMaxLength(64);
            entity.Property(e => e.Email).HasMaxLength(325);
            entity.Property(e => e.Key1).HasMaxLength(128);
            entity.Property(e => e.Key2).HasMaxLength(128);
            entity.Property(e => e.Nombres).HasMaxLength(128);
            entity.Property(e => e.NumeroDocumento).HasMaxLength(16);
            entity.Property(e => e.Password).HasMaxLength(128);
            entity.Property(e => e.Telefono).HasMaxLength(18);

            entity.HasOne(d => d.TipoDocumentoNavigation).WithMany(p => p.Usuarios)
                .HasForeignKey(d => d.TipoDocumento)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Usuario_TipoDocumento_fkey");
        });

        modelBuilder.Entity<Ventum>(entity =>
        {
            entity.HasKey(e => e.VentaId).HasName("Venta_pkey");

            entity.Property(e => e.VentaId)
                .ValueGeneratedOnAdd()
                .HasColumnName("VentaID");
            entity.Property(e => e.AuditoriaFechaIngreso).HasColumnType("timestamp without time zone");
            entity.Property(e => e.AuditoriaFechaModificacion).HasColumnType("timestamp without time zone");
            entity.Property(e => e.AuditoriaUsuarioIngreso).HasMaxLength(64);
            entity.Property(e => e.AuditoriaUsuarioModificacion).HasMaxLength(64);
            entity.Property(e => e.Monto).HasPrecision(18, 2);
            entity.Property(e => e.NumeroComprobante).HasMaxLength(16);
            entity.Property(e => e.OpcionId).HasColumnName("OpcionID");
            entity.Property(e => e.SerieComprobante).HasMaxLength(8);

            entity.HasOne(d => d.EstadoVentaNavigation).WithMany(p => p.Venta)
                .HasForeignKey(d => d.EstadoVenta)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Venta_EstadoVenta_fkey");

            entity.HasOne(d => d.MonedaNavigation).WithMany(p => p.Venta)
                .HasForeignKey(d => d.Moneda)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Venta_Moneda_fkey");

            entity.HasOne(d => d.TipoComprobanteNavigation).WithMany(p => p.Venta)
                .HasForeignKey(d => d.TipoComprobante)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Venta_TipoComprobante_fkey");

            entity.HasOne(d => d.Venta).WithOne(p => p.Ventum)
                .HasForeignKey<Ventum>(d => d.VentaId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Venta_VentaID_fkey");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

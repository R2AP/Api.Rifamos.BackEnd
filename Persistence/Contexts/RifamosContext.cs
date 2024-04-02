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

    public virtual DbSet<EstadoVentum> EstadoVenta { get; set; }

    public virtual DbSet<Opcion> Opcions { get; set; }

    public virtual DbSet<Pago> Pagos { get; set; }

    public virtual DbSet<Precio> Precios { get; set; }

    public virtual DbSet<Premio> Premios { get; set; }

    public virtual DbSet<Rifa> Rifas { get; set; }

    public virtual DbSet<Sesion> Sesions { get; set; }

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
            entity.HasKey(e => e.CodigoEstadoOpcion).HasName("EstadoOpcion_pkey");

            entity.ToTable("EstadoOpcion");

            entity.Property(e => e.CodigoEstadoOpcion).HasMaxLength(8);
            entity.Property(e => e.AuditoriaFechaIngreso)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("auditoria_fecha_ingreso");
            entity.Property(e => e.AuditoriaFechaModificacion)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("auditoria_fecha_modificacion");
            entity.Property(e => e.AuditoriaUsuarioIngreso)
                .HasMaxLength(64)
                .HasColumnName("auditoria_usuario_ingreso");
            entity.Property(e => e.AuditoriaUsuarioModificacion)
                .HasMaxLength(64)
                .HasColumnName("auditoria_usuario_modificacion");
            entity.Property(e => e.DescripcionEstadoOpcion).HasMaxLength(128);
        });

        modelBuilder.Entity<EstadoPago>(entity =>
        {
            entity.HasKey(e => e.CodigoEstadoPago).HasName("EstadoPago_pkey");

            entity.ToTable("EstadoPago");

            entity.Property(e => e.CodigoEstadoPago).HasMaxLength(8);
            entity.Property(e => e.AuditoriaFechaIngreso)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("auditoria_fecha_ingreso");
            entity.Property(e => e.AuditoriaFechaModificacion)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("auditoria_fecha_modificacion");
            entity.Property(e => e.AuditoriaUsuarioIngreso)
                .HasMaxLength(64)
                .HasColumnName("auditoria_usuario_ingreso");
            entity.Property(e => e.AuditoriaUsuarioModificacion)
                .HasMaxLength(64)
                .HasColumnName("auditoria_usuario_modificacion");
            entity.Property(e => e.DescripcionEstadoPago).HasMaxLength(128);

            entity.HasOne(d => d.CodigoEstadoPagoNavigation).WithOne(p => p.EstadoPago)
                .HasPrincipalKey<Pago>(p => p.CodigoEstadoPago)
                .HasForeignKey<EstadoPago>(d => d.CodigoEstadoPago)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("EstadoPago_CodigoEstadoPago_fkey");
        });

        modelBuilder.Entity<EstadoVentum>(entity =>
        {
            entity.HasKey(e => e.CodigoEstadoVenta).HasName("EstadoVenta_pkey");

            entity.Property(e => e.CodigoEstadoVenta).HasMaxLength(8);
            entity.Property(e => e.AuditoriaFechaIngreso)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("auditoria_fecha_ingreso");
            entity.Property(e => e.AuditoriaFechaModificacion)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("auditoria_fecha_modificacion");
            entity.Property(e => e.AuditoriaUsuarioIngreso)
                .HasMaxLength(64)
                .HasColumnName("auditoria_usuario_ingreso");
            entity.Property(e => e.AuditoriaUsuarioModificacion)
                .HasMaxLength(64)
                .HasColumnName("auditoria_usuario_modificacion");
            entity.Property(e => e.DescripcionEstadoVenta).HasMaxLength(128);
        });

        modelBuilder.Entity<Opcion>(entity =>
        {
            entity.HasKey(e => e.OpcionId).HasName("Opcion_pkey");

            entity.ToTable("Opcion");

            entity.Property(e => e.OpcionId).HasColumnName("OpcionID");
            entity.Property(e => e.AuditoriaFechaIngreso)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("auditoria_fecha_ingreso");
            entity.Property(e => e.AuditoriaFechaModificacion)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("auditoria_fecha_modificacion");
            entity.Property(e => e.AuditoriaUsuarioIngreso)
                .HasMaxLength(64)
                .HasColumnName("auditoria_usuario_ingreso");
            entity.Property(e => e.AuditoriaUsuarioModificacion)
                .HasMaxLength(64)
                .HasColumnName("auditoria_usuario_modificacion");
            entity.Property(e => e.CodigoEstadoOpcion).HasMaxLength(8);
            entity.Property(e => e.RifaId).HasColumnName("RifaID");
            entity.Property(e => e.TokenOpcion).HasMaxLength(128);
            entity.Property(e => e.UsuarioId).HasColumnName("UsuarioID");

            entity.HasOne(d => d.CodigoEstadoOpcionNavigation).WithMany(p => p.Opcions)
                .HasForeignKey(d => d.CodigoEstadoOpcion)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Opcion_CodigoEstadoOpcion_fkey");

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

            entity.HasIndex(e => e.CodigoEstadoPago, "Pago_key").IsUnique();

            entity.Property(e => e.PagoId).HasColumnName("PagoID");
            entity.Property(e => e.AuditoriaFechaIngreso)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("auditoria_fecha_ingreso");
            entity.Property(e => e.AuditoriaFechaModificacion)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("auditoria_fecha_modificacion");
            entity.Property(e => e.AuditoriaUsuarioIngreso)
                .HasMaxLength(64)
                .HasColumnName("auditoria_usuario_ingreso");
            entity.Property(e => e.AuditoriaUsuarioModificacion)
                .HasMaxLength(64)
                .HasColumnName("auditoria_usuario_modificacion");
            entity.Property(e => e.CodigoEstadoPago).HasMaxLength(8);
            entity.Property(e => e.CodigoTipoPago).HasMaxLength(8);
            entity.Property(e => e.CodigoTransaccion)
                .HasMaxLength(32)
                .HasComment("Código ofrecido por la pasarela");
            entity.Property(e => e.Moneda).HasMaxLength(8);
            entity.Property(e => e.Monto).HasPrecision(18, 2);
            entity.Property(e => e.VentaId).HasColumnName("VentaID");

            entity.HasOne(d => d.CodigoTipoPagoNavigation).WithMany(p => p.Pagos)
                .HasForeignKey(d => d.CodigoTipoPago)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Pago_CodigoTipoPago_fkey");

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
            entity.Property(e => e.AuditoriaFechaIngreso)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("auditoria_fecha_ingreso");
            entity.Property(e => e.AuditoriaFechaModificacion)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("auditoria_fecha_modificacion");
            entity.Property(e => e.AuditoriaUsuarioIngreso)
                .HasMaxLength(64)
                .HasColumnName("auditoria_usuario_ingreso");
            entity.Property(e => e.AuditoriaUsuarioModificacion)
                .HasMaxLength(64)
                .HasColumnName("auditoria_usuario_modificacion");
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
            entity.Property(e => e.AuditoriaFechaIngreso)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("auditoria_fecha_ingreso");
            entity.Property(e => e.AuditoriaFechaModificacion)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("auditoria_fecha_modificacion");
            entity.Property(e => e.AuditoriaUsuarioIngreso)
                .HasMaxLength(64)
                .HasColumnName("auditoria_usuario_ingreso");
            entity.Property(e => e.AuditoriaUsuarioModificacion)
                .HasMaxLength(64)
                .HasColumnName("auditoria_usuario_modificacion");
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
            entity.Property(e => e.AuditoriaFechaIngreso)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("auditoria_fecha_ingreso");
            entity.Property(e => e.AuditoriaFechaModificacion)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("auditoria_fecha_modificacion");
            entity.Property(e => e.AuditoriaUsuarioIngreso)
                .HasMaxLength(64)
                .HasColumnName("auditoria_usuario_ingreso");
            entity.Property(e => e.AuditoriaUsuarioModificacion)
                .HasMaxLength(64)
                .HasColumnName("auditoria_usuario_modificacion");
            entity.Property(e => e.RifaDescripcion).HasMaxLength(128);
            entity.Property(e => e.Sponsor).HasMaxLength(128);
        });

        modelBuilder.Entity<Sesion>(entity =>
        {
            entity.HasKey(e => e.SesionId).HasName("Sesion_pkey");

            entity.ToTable("Sesion");

            entity.Property(e => e.SesionId).HasColumnName("SesionID");
            entity.Property(e => e.AuditoriaFechaIngreso)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("auditoria_fecha_ingreso");
            entity.Property(e => e.AuditoriaFechaModificacion)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("auditoria_fecha_modificacion");
            entity.Property(e => e.AuditoriaUsuarioIngreso)
                .HasMaxLength(64)
                .HasColumnName("auditoria_usuario_ingreso");
            entity.Property(e => e.AuditoriaUsuarioModificacion)
                .HasMaxLength(64)
                .HasColumnName("auditoria_usuario_modificacion");
            entity.Property(e => e.CodigoTipoEvento).HasMaxLength(8);
            entity.Property(e => e.Email).HasMaxLength(64);
            entity.Property(e => e.Ip)
                .HasMaxLength(16)
                .HasColumnName("IP");
            entity.Property(e => e.UsuarioId).HasColumnName("UsuarioID");

            entity.HasOne(d => d.CodigoTipoEventoNavigation).WithMany(p => p.Sesions)
                .HasForeignKey(d => d.CodigoTipoEvento)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Sesion_CodigoTipoEvento_fkey");

            entity.HasOne(d => d.Usuario).WithMany(p => p.Sesions)
                .HasForeignKey(d => d.UsuarioId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Sesion_UsuarioID_fkey");
        });

        modelBuilder.Entity<TipoDocumento>(entity =>
        {
            entity.HasKey(e => e.CodigoTipoDocumento).HasName("TipoDocumento_pkey");

            entity.ToTable("TipoDocumento");

            entity.Property(e => e.CodigoTipoDocumento).HasMaxLength(8);
            entity.Property(e => e.AuditoriaFechaIngreso)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("auditoria_fecha_ingreso");
            entity.Property(e => e.AuditoriaFechaModificacion)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("auditoria_fecha_modificacion");
            entity.Property(e => e.AuditoriaUsuarioIngreso)
                .HasMaxLength(64)
                .HasColumnName("auditoria_usuario_ingreso");
            entity.Property(e => e.AuditoriaUsuarioModificacion)
                .HasMaxLength(64)
                .HasColumnName("auditoria_usuario_modificacion");
            entity.Property(e => e.DescripcionTipoDocumento).HasMaxLength(128);
        });

        modelBuilder.Entity<TipoEvento>(entity =>
        {
            entity.HasKey(e => e.CodigoTipoEvento).HasName("TipoEvento_pkey");

            entity.ToTable("TipoEvento");

            entity.Property(e => e.CodigoTipoEvento).HasMaxLength(8);
            entity.Property(e => e.AuditoriaFechaIngreso)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("auditoria_fecha_ingreso");
            entity.Property(e => e.AuditoriaFechaModificacion)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("auditoria_fecha_modificacion");
            entity.Property(e => e.AuditoriaUsuarioIngreso)
                .HasMaxLength(64)
                .HasColumnName("auditoria_usuario_ingreso");
            entity.Property(e => e.AuditoriaUsuarioModificacion)
                .HasMaxLength(64)
                .HasColumnName("auditoria_usuario_modificacion");
            entity.Property(e => e.DescripcionTipoEvento).HasMaxLength(128);
        });

        modelBuilder.Entity<TipoPago>(entity =>
        {
            entity.HasKey(e => e.CodigoTipoPago).HasName("TipoPago_pkey");

            entity.ToTable("TipoPago", tb => tb.HasComment("1. Tarjetas de crédito o débito.\r\n2. Pagos en efectivo.\r\n3. Transferencias bancarias"));

            entity.Property(e => e.CodigoTipoPago).HasMaxLength(8);
            entity.Property(e => e.AuditoriaFechaIngreso)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("auditoria_fecha_ingreso");
            entity.Property(e => e.AuditoriaFechaModificacion)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("auditoria_fecha_modificacion");
            entity.Property(e => e.AuditoriaUsuarioIngreso)
                .HasMaxLength(64)
                .HasColumnName("auditoria_usuario_ingreso");
            entity.Property(e => e.AuditoriaUsuarioModificacion)
                .HasMaxLength(64)
                .HasColumnName("auditoria_usuario_modificacion");
            entity.Property(e => e.DescripcionTipoPago).HasMaxLength(128);
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.UsuarioId).HasName("Usuario_pkey");

            entity.ToTable("Usuario");

            entity.Property(e => e.UsuarioId).HasColumnName("UsuarioID");
            entity.Property(e => e.ApellidoMaterno).HasMaxLength(128);
            entity.Property(e => e.ApellidoPaterno).HasMaxLength(128);
            entity.Property(e => e.AuditoriaFechaIngreso)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("auditoria_fecha_ingreso");
            entity.Property(e => e.AuditoriaFechaModificacion)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("auditoria_fecha_modificacion");
            entity.Property(e => e.AuditoriaUsuarioIngreso)
                .HasMaxLength(64)
                .HasColumnName("auditoria_usuario_ingreso");
            entity.Property(e => e.AuditoriaUsuarioModificacion)
                .HasMaxLength(64)
                .HasColumnName("auditoria_usuario_modificacion");
            entity.Property(e => e.CodigoTipoDocumento).HasMaxLength(8);
            entity.Property(e => e.Email).HasMaxLength(325);
            entity.Property(e => e.Nombres).HasMaxLength(128);
            entity.Property(e => e.NumeroDocumento).HasMaxLength(16);
            entity.Property(e => e.Telefono).HasMaxLength(18);

            entity.HasOne(d => d.CodigoTipoDocumentoNavigation).WithMany(p => p.Usuarios)
                .HasForeignKey(d => d.CodigoTipoDocumento)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Usuario_CodigoTipoDocumento_fkey");
        });

        modelBuilder.Entity<Ventum>(entity =>
        {
            entity.HasKey(e => e.VentaId).HasName("Venta_pkey");

            entity.Property(e => e.VentaId)
                .ValueGeneratedOnAdd()
                .HasColumnName("VentaID");
            entity.Property(e => e.AuditoriaFechaIngreso)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("auditoria_fecha_ingreso");
            entity.Property(e => e.AuditoriaFechaModificacion)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("auditoria_fecha_modificacion");
            entity.Property(e => e.AuditoriaUsuarioIngreso)
                .HasMaxLength(64)
                .HasColumnName("auditoria_usuario_ingreso");
            entity.Property(e => e.AuditoriaUsuarioModificacion)
                .HasMaxLength(64)
                .HasColumnName("auditoria_usuario_modificacion");
            entity.Property(e => e.CodigoEstadoVenta).HasMaxLength(8);
            entity.Property(e => e.Moneda).HasMaxLength(8);
            entity.Property(e => e.Monto).HasPrecision(18, 2);
            entity.Property(e => e.NumeroComprobante).HasMaxLength(16);
            entity.Property(e => e.OpcionId).HasColumnName("OpcionID");
            entity.Property(e => e.SerieComprobante).HasMaxLength(8);
            entity.Property(e => e.TipoComprobante).HasMaxLength(8);

            entity.HasOne(d => d.CodigoEstadoVentaNavigation).WithMany(p => p.Venta)
                .HasForeignKey(d => d.CodigoEstadoVenta)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Venta_CodigoEstadoVenta_fkey");

            entity.HasOne(d => d.Venta).WithOne(p => p.Ventum)
                .HasForeignKey<Ventum>(d => d.VentaId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Venta_VentaID_fkey");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

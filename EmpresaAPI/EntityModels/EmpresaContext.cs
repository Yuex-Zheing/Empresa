using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace EmpresaAPI.EntityModels
{
    public partial class EmpresaContext : DbContext
    {
        public EmpresaContext()
        {
        }

        public EmpresaContext(DbContextOptions<EmpresaContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Cliente> Clientes { get; set; } = null!;
        public virtual DbSet<Cuenta> Cuentas { get; set; } = null!;
        public virtual DbSet<Movimiento> Movimientos { get; set; } = null!;
        public virtual DbSet<Persona> Personas { get; set; } = null!;

//        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//        {
//            if (!optionsBuilder.IsConfigured)
//            {
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
//                optionsBuilder.UseSqlServer(,);
//            }
//        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Cliente>(entity =>
            {
                entity.HasKey(e => e.IdCliente)
                    .HasName("PK__Clientes__D594664289836189");

                entity.Property(e => e.Clave).HasMaxLength(20);

                entity.Property(e => e.Estado).HasMaxLength(1);

                entity.HasOne(d => d.PersonaNavigation)
                    .WithMany(p => p.Clientes)
                    .HasForeignKey(d => d.Persona)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Clientes__Person__787EE5A0");
            });

            modelBuilder.Entity<Cuenta>(entity =>
            {
                entity.HasKey(e => e.IdCuenta)
                    .HasName("PK__Cuentas__D41FD70607214D27");

                entity.HasIndex(e => e.NumeroCuenta, "UQ__Cuentas__E039507BB65434C5")
                    .IsUnique();

                entity.Property(e => e.Estado).HasMaxLength(1);

                entity.Property(e => e.NumeroCuenta).HasMaxLength(10);

                entity.Property(e => e.SaldoInicial).HasColumnType("decimal(12, 3)");

                entity.Property(e => e.TipoCuenta).HasMaxLength(1);

                entity.HasOne(d => d.ClienteNavigation)
                    .WithMany(p => p.Cuenta)
                    .HasForeignKey(d => d.Cliente)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Cuentas__Cliente__7C4F7684");
            });

            modelBuilder.Entity<Movimiento>(entity =>
            {
                entity.HasKey(e => e.IdMovimiento)
                    .HasName("PK__Movimien__881A6AE0663CC5FB");

                entity.Property(e => e.Estado).HasMaxLength(1);

                entity.Property(e => e.Fecha).HasColumnType("datetime");

                entity.Property(e => e.MovDescripcion).HasMaxLength(200);

                entity.Property(e => e.Saldo).HasColumnType("decimal(12, 3)");

                entity.Property(e => e.TipoMovimiento).HasMaxLength(1);

                entity.Property(e => e.Valor).HasColumnType("decimal(12, 3)");

                entity.HasOne(d => d.CuentaNavigation)
                    .WithMany(p => p.Movimientos)
                    .HasForeignKey(d => d.Cuenta)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Movimient__Cuent__7F2BE32F");
            });

            modelBuilder.Entity<Persona>(entity =>
            {
                entity.HasKey(e => e.IdPersona)
                    .HasName("PK__Personas__2EC8D2AC80EF476D");

                entity.HasIndex(e => e.Identificacion, "UQ__Personas__D6F931E59581385A")
                    .IsUnique();

                entity.Property(e => e.Direccion).HasMaxLength(200);

                entity.Property(e => e.Genero).HasMaxLength(1);

                entity.Property(e => e.Identificacion).HasMaxLength(10);

                entity.Property(e => e.Nombre).HasMaxLength(100);

                entity.Property(e => e.Telefono).HasMaxLength(10);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}

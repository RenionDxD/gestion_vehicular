using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace GestionEstacionamientoRicardo.Models;

public partial class EstacionamientoDbContext : DbContext
{
    public EstacionamientoDbContext()
    {
    }

    public EstacionamientoDbContext(DbContextOptions<EstacionamientoDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<RegistroEstancium> RegistroEstancia { get; set; }

    

    public virtual DbSet<TiposVehiculo> TiposVehiculos { get; set; }

    public virtual DbSet<Vehiculo> Vehiculos { get; set; }

//    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
//        => optionsBuilder.UseSqlServer("Server=localhost;Database=EstacionamientoDB; integrated security=true;TrustServerCertificate=true;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<RegistroEstancium>(entity =>
        {
            entity.HasKey(e => e.EstanciaId).HasName("PK__Registro__225BA37FEE9218A5");

            entity.Property(e => e.EstanciaId).HasColumnName("EstanciaID");
            entity.Property(e => e.FechaEntrada)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.FechaSalida).HasColumnType("datetime");
            entity.Property(e => e.VehiculoId).HasColumnName("VehiculoID");

            entity.HasOne(d => d.Vehiculo).WithMany(p => p.RegistroEstancia)
                .HasForeignKey(d => d.VehiculoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__RegistroE__Vehic__3F466844");
        });

        

        modelBuilder.Entity<TiposVehiculo>(entity =>
        {
            entity.HasKey(e => e.TipoId).HasName("PK__TiposVeh__97099E97E66E512C");

            entity.ToTable("TiposVehiculo");

            entity.Property(e => e.TipoId).HasColumnName("TipoID");
            entity.Property(e => e.Descripcion).HasMaxLength(50);
            entity.Property(e => e.TarifaPorMinuto).HasColumnType("decimal(10, 2)");
        });

        modelBuilder.Entity<Vehiculo>(entity =>
        {
            entity.HasKey(e => e.VehiculoId).HasName("PK__Vehiculo__AA0886209236C3A6");

            entity.HasIndex(e => e.Placa, "UQ__Vehiculo__8310F99DB4E24E71").IsUnique();

            entity.Property(e => e.VehiculoId).HasColumnName("VehiculoID");
            entity.Property(e => e.Placa).HasMaxLength(10);
            entity.Property(e => e.TipoId).HasColumnName("TipoID");

            entity.HasOne(d => d.Tipo).WithMany(p => p.Vehiculos)
                .HasForeignKey(d => d.TipoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Vehiculos__TipoI__3B75D760");
        });

        modelBuilder.Entity<RegistroEstancium>().HasOne(re => re.Vehiculo).WithMany(v => v.RegistroEstancia).HasForeignKey(re => re.VehiculoId).OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Vehiculo>().HasOne(v => v.Tipo).WithMany(t => t.Vehiculos).HasForeignKey(v => v.TipoId).OnDelete(DeleteBehavior.Cascade); // Eliminación en cascada

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

using Microsoft.EntityFrameworkCore;
using Transportadora.Models;
using System.Collections.Generic;

namespace Transportadora.Data
{
    public class TransporteContext : DbContext
    {
        public DbSet<Cargador> Cargadores { get; set; }
        public DbSet<Autobus> Autobuses { get; set; }
        public DbSet<Horario> Horarios { get; set; }

        public TransporteContext(DbContextOptions<TransporteContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configuración de restricciones y relaciones
            modelBuilder.Entity<Cargador>()
                .Property(c => c.HoraInicio)
                .IsRequired();

            modelBuilder.Entity<Cargador>()
                .Property(c => c.HoraFin)
                .IsRequired();

            modelBuilder.Entity<Cargador>()
                .Property(c => c.EnUso)
                .IsRequired();

            modelBuilder.Entity<Cargador>()
                .HasMany(c => c.Autobuses)
                .WithOne(a => a.Cargador)
                .HasForeignKey(a => a.CargadorId);

            modelBuilder.Entity<Autobus>()
                .Property(a => a.Placa)
                .IsRequired();

            modelBuilder.Entity<Autobus>()
                .Property(a => a.EnOperacion)
                .IsRequired();

            modelBuilder.Entity<Autobus>()
                .Property(a => a.TiempoUltimoMantenimiento)
                .IsRequired();

            modelBuilder.Entity<Autobus>()
                .Property(a => a.HorasEnOperacion)
                .IsRequired();

            modelBuilder.Entity<Horario>()
                .Property(h => h.Hora)
                .IsRequired();

            modelBuilder.Entity<Horario>()
                .Property(h => h.EsHorarioPico)
                .IsRequired();

            modelBuilder.Entity<Horario>()
                .Property(h => h.BusesEnOperacion)
                .IsRequired();

            modelBuilder.Entity<Horario>()
                .Property(h => h.CargadoresEnUso)
                .IsRequired();

            // Configuración de índices si es necesario
            modelBuilder.Entity<Cargador>()
                .HasIndex(c => c.Id)
                .IsUnique();

            modelBuilder.Entity<Autobus>()
                .HasIndex(a => a.Id)
                .IsUnique();

            modelBuilder.Entity<Horario>()
                .HasIndex(h => h.Id)
                .IsUnique();

            base.OnModelCreating(modelBuilder);
        }
    }

}

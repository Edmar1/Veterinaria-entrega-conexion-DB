using lib_dominio.Entidades;
using lib_repositorios.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace lib_repositorios.Implementaciones
{
    public partial class Conexion : DbContext, IConexion
    {
        public string? StringConexion { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(this.StringConexion!, p => { });
            optionsBuilder.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
        }

        // Reordena tus DbSets para mayor claridad
        public DbSet<Sedes>? Sedes { get; set; }
        public DbSet<Salas>? Salas { get; set; }
        public DbSet<Equipos>? Equipos { get; set; }
        public DbSet<Empleados>? Empleados { get; set; }
        public DbSet<Veterinarios>? Veterinarios { get; set; }
        public DbSet<Propietarios>? Propietarios { get; set; }
        public DbSet<Tel_Propietarios>? Tel_Propietarios { get; set; }
        public DbSet<Mascotas>? Mascotas { get; set; }
        public DbSet<Citas>? Citas { get; set; }
        public DbSet<Pagos>? Pagos { get; set; }
        public DbSet<Insumos>? Insumos { get; set; }
        public DbSet<Medicamentos>? Medicamentos { get; set; }

        // Tablas de relación N:M
        public DbSet<Salas_Insumos>? Salas_Insumos { get; set; }
        public DbSet<Salas_Medicamentos>? Salas_Medicamentos { get; set; }
        public DbSet<Veterinarios_Mascotas>? Veterinarios_Mascotas { get; set; }
    }
}

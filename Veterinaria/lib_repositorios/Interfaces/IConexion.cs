using lib_dominio.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace lib_repositorios.Interfaces
{
    public interface IConexion
    {
        string? StringConexion { get; set; }

        DbSet<Sedes>? Sedes { get; set; }
        DbSet<Salas>? Salas { get; set; }
        DbSet<Equipos>? Equipos { get; set; }
        DbSet<Insumos>? Insumos { get; set; }
        DbSet<Medicamentos>? Medicamentos { get; set; }
        DbSet<Empleados>? Empleados { get; set; }
        DbSet<Veterinarios>? Veterinarios { get; set; }
        DbSet<Propietarios>? Propietarios { get; set; }
        DbSet<Tel_Propietarios>? Tel_Propietarios { get; set; }
        DbSet<Mascotas>? Mascotas { get; set; }
        DbSet<Citas>? Citas { get; set; }
        DbSet<Pagos>? Pagos { get; set; }
        DbSet<Salas_Insumos>? Salas_Insumos { get; set; }
        DbSet<Salas_Medicamentos>? Salas_Medicamentos { get; set; }
        DbSet<Veterinarios_Mascotas>? Veterinarios_Mascotas { get; set; }

        EntityEntry<T> Entry<T>(T entity) where T : class;
        int SaveChanges();
    }
}
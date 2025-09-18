using lib_aplicaciones.Interfaces;
using lib_dominio.Entidades;
using lib_repositorios.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace lib_aplicaciones.Implementaciones
{
    public class VeterinariosAplicacion : IVeterinariosAplicacion
    {
        private readonly IConexion conexion;

        public VeterinariosAplicacion(IConexion conexion)
        {
            this.conexion = conexion;
        }

        // Lógica de negocio: un veterinario no puede tener edad menor a 18
        public bool Guardar(Veterinarios entidad)
        {
            if (entidad.Edad < 18)
                throw new Exception("El veterinario debe ser mayor de edad");

            conexion.Veterinarios!.Add(entidad);
            conexion.SaveChanges();
            return true;
        }

        public bool Modificar(Veterinarios entidad)
        {
            var entry = conexion.Entry(entidad);
            entry.State = EntityState.Modified;
            conexion.SaveChanges();
            return true;
        }

        public bool Borrar(Veterinarios entidad)
        {
            conexion.Veterinarios!.Remove(entidad);
            conexion.SaveChanges();
            return true;
        }

        public List<Veterinarios> Listar()
        {
            return conexion.Veterinarios!.ToList();
        }
    }
}
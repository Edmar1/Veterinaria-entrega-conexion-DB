using lib_aplicaciones.Interfaces;
using lib_dominio.Entidades;
using lib_repositorios.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace lib_aplicaciones.Implementaciones
{
    public class PropietariosAplicacion : IPropietariosAplicacion
    {
        private readonly IConexion conexion;

        public PropietariosAplicacion(IConexion conexion)
        {
            this.conexion = conexion;
        }

        // Lógica de negocio: un propietario debe ser mayor de 18 años
        public bool Guardar(Propietarios entidad)
        {
            if (entidad.Edad < 18)
                throw new Exception("El propietario debe ser mayor de edad");

            conexion.Propietarios!.Add(entidad);
            conexion.SaveChanges();
            return true;
        }

        public bool Modificar(Propietarios entidad)
        {
            var entry = conexion.Entry(entidad);
            entry.State = EntityState.Modified;
            conexion.SaveChanges();
            return true;
        }

        public bool Borrar(Propietarios entidad)
        {
            conexion.Propietarios!.Remove(entidad);
            conexion.SaveChanges();
            return true;
        }

        public List<Propietarios> Listar()
        {
            return conexion.Propietarios!.ToList();
        }
    }
}
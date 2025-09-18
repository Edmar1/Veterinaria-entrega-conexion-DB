using lib_dominio.Entidades;
using lib_repositorios.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace lib_repositorios.Implementaciones
{
    public class EmpleadosAplicacion : IEmpleadosAplicacion
    {
        private IConexion? IConexion = null;

        public EmpleadosAplicacion(IConexion iConexion)
        {
            this.IConexion = iConexion;
        }

        public void Configurar(string StringConexion)
        {
            this.IConexion!.StringConexion = StringConexion;
        }

        public Empleados? Guardar(Empleados? entidad)
        {
            if (entidad == null)                       // Validación de negocio:
                throw new Exception("lbFaltaInformacion");
            // -> No se puede guardar un empleado sin datos.

            if (entidad.Id != 0)                       // Validación de negocio:
                throw new Exception("lbYaSeGuardo");
            // -> Un empleado nuevo debe tener Id = 0. 
            //    Si ya tiene Id, significa que ya existe en la BD.

            this.IConexion!.Empleados!.Add(entidad);
            this.IConexion.SaveChanges();
            return entidad;
        }

        public Empleados? Modificar(Empleados? entidad)
        {
            if (entidad == null)                       // Validación de negocio:
                throw new Exception("lbFaltaInformacion");
            // -> No se puede modificar un empleado vacío.

            if (entidad.Id == 0)                       // Validación de negocio:
                throw new Exception("lbNoSeGuardo");
            // -> Solo se puede modificar un empleado que ya exista en la BD.

            var entry = this.IConexion!.Entry<Empleados>(entidad);
            entry.State = EntityState.Modified;
            this.IConexion.SaveChanges();
            return entidad;
        }

        public Empleados? Borrar(Empleados? entidad)
        {
            if (entidad == null)                       // Validación de negocio:
                throw new Exception("lbFaltaInformacion");
            // -> No se puede borrar algo que no existe.

            if (entidad.Id == 0)                       // Validación de negocio:
                throw new Exception("lbNoSeGuardo");
            // -> Solo se puede borrar un empleado que ya fue guardado en la BD.

            this.IConexion!.Empleados!.Remove(entidad);
            this.IConexion.SaveChanges();
            return entidad;
        }
        public List<Empleados> Listar()
        {
            return this.IConexion!.Empleados!.Take(20).ToList();
        }

        public List<Empleados> BuscarPorCargo(string cargo)
        {
            return this.IConexion!.Empleados!
                .Where(x => x.Cargo == cargo)
                .ToList();
        }
    }
}

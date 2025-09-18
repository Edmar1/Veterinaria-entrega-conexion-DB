using lib_dominio.Entidades;
using lib_repositorios.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace lib_repositorios.Implementaciones
{
    public class Tel_PropietariosAplicacion : ITel_PropietariosAplicacion
    {
        private IConexion? conexion;

        public Tel_PropietariosAplicacion(IConexion conexion)
        {
            this.conexion = conexion;
        }

        public void Configurar(string StringConexion)
        {
            conexion!.StringConexion = StringConexion;
        }

        // Lógica de negocio: el teléfono debe tener al menos 7 caracteres
        public Tel_Propietarios? Guardar(Tel_Propietarios? entidad)
        {
            if (entidad == null || string.IsNullOrWhiteSpace(entidad.Telefono))
                throw new Exception("Debe ingresar un teléfono válido");

            if (entidad.Telefono.Length < 7)
                throw new Exception("El teléfono debe tener mínimo 7 dígitos");

            conexion!.Tel_Propietarios!.Add(entidad);
            conexion.SaveChanges();
            return entidad;
        }

        public Tel_Propietarios? Modificar(Tel_Propietarios? entidad)
        {
            if (entidad == null || entidad.Id == 0)
                throw new Exception("El registro no existe");

            var entry = conexion!.Entry(entidad);
            entry.State = EntityState.Modified;
            conexion.SaveChanges();
            return entidad;
        }

        public Tel_Propietarios? Borrar(Tel_Propietarios? entidad)
        {
            if (entidad == null || entidad.Id == 0)
                throw new Exception("El registro no existe");

            conexion!.Tel_Propietarios!.Remove(entidad);
            conexion.SaveChanges();
            return entidad;
        }

        public List<Tel_Propietarios> Listar()
        {
            return conexion!.Tel_Propietarios!.ToList();
        }

        public List<Tel_Propietarios> BuscarPorNumero(string numero)
        {
            return conexion!.Tel_Propietarios!
                .Where(t => t.Telefono.Contains(numero))
                .ToList();
        }
    }
}
using lib_dominio.Entidades;
using lib_repositorios.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace lib_repositorios.Implementaciones
{
    public class MascotasAplicacion : IMascotasAplicacion
    {
        private IConexion? conexion;

        public MascotasAplicacion(IConexion conexion)
        {
            this.conexion = conexion;
        }

        public void Configurar(string StringConexion)
        {
            this.conexion!.StringConexion = StringConexion;
        }

        // Lógica de negocio: No permitir guardar mascotas sin nombre ni especie
        public Mascotas? Guardar(Mascotas? entidad)
        {
            if (entidad == null || string.IsNullOrWhiteSpace(entidad.Nombre) || string.IsNullOrWhiteSpace(entidad.Especie))
                throw new Exception("La mascota debe tener nombre y especie.");

            entidad.Fecha_Registro = DateTime.Now; // Lógica de negocio: siempre registrar la fecha de creación
            conexion!.Mascotas!.Add(entidad);
            conexion.SaveChanges();
            return entidad;
        }

        // Lógica de negocio: Solo modificar si existe
        public Mascotas? Modificar(Mascotas? entidad)
        {
            if (entidad == null || entidad.Id <= 0)
                throw new Exception("La mascota no es válida para modificar.");

            var entry = conexion!.Entry(entidad);
            entry.State = EntityState.Modified;
            conexion.SaveChanges();
            return entidad;
        }

        // Lógica de negocio: No permitir borrar si no existe
        public Mascotas? Borrar(Mascotas? entidad)
        {
            if (entidad == null || entidad.Id <= 0)
                throw new Exception("La mascota no es válida para borrar.");

            conexion!.Mascotas!.Remove(entidad);
            conexion.SaveChanges();
            return entidad;
        }

        public List<Mascotas> Listar()
        {
            return conexion!.Mascotas!.ToList();
        }

        // Lógica de negocio: Buscar todas las mascotas de un propietario
        public List<Mascotas> BuscarPorPropietario(int propietarioId)
        {
            return conexion!.Mascotas!
                .Where(m => m.Id_Propietario == propietarioId)
                .ToList();
        }

        // Lógica de negocio: Buscar por especie (perros, gatos, etc.)
        public List<Mascotas> BuscarPorEspecie(string especie)
        {
            return conexion!.Mascotas!
                .Where(m => m.Especie.ToLower() == especie.ToLower())
                .ToList();
        }
    }
}

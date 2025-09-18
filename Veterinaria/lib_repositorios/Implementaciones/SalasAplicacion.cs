using lib_dominio.Entidades;
using lib_repositorios.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace lib_repositorios.Implementaciones
{
    public class SalasAplicacion : ISalasAplicacion
    {
        private IConexion? iConexion;

        public SalasAplicacion(IConexion conexion)
        {
            this.iConexion = conexion;
        }

        public void Configurar(string StringConexion)
        {
            this.iConexion!.StringConexion = StringConexion;
        }

        public Salas? Guardar(Salas? entidad)
        {
            // Lógica de negocio:
            // Una sala debe tener Nombre y Tipo obligatorios.
            if (entidad == null)
                throw new Exception("lbFaltaInformacion");

            if (string.IsNullOrWhiteSpace(entidad.Numero.ToString()) || string.IsNullOrWhiteSpace(entidad.Tipo))
                throw new Exception("lbSalaInvalida");

            if (entidad.Id != 0)
                throw new Exception("lbYaSeGuardo");

            iConexion!.Salas!.Add(entidad);
            iConexion.SaveChanges();
            return entidad;
        }

        public Salas? Modificar(Salas? entidad)
        {
            // Lógica de negocio:
            // Para modificar, la sala ya debe existir (Id > 0)
            if (entidad == null)
                throw new Exception("lbFaltaInformacion");

            if (entidad.Id == 0)
                throw new Exception("lbNoSeGuardo");

            var entry = iConexion!.Entry(entidad);
            entry.State = EntityState.Modified;
            iConexion.SaveChanges();
            return entidad;
        }

        public Salas? Borrar(Salas? entidad)
        {
            // Lógica de negocio:
            // Solo se puede borrar si existe en BD (Id > 0)
            if (entidad == null)
                throw new Exception("lbFaltaInformacion");

            if (entidad.Id == 0)
                throw new Exception("lbNoSeGuardo");

            iConexion!.Salas!.Remove(entidad);
            iConexion.SaveChanges();
            return entidad;
        }

        public List<Salas> Listar()
        {
            // Lógica de negocio:
            // Solo listar máximo 20 registros para evitar sobrecarga
            return iConexion!.Salas!.Take(20).ToList();
        }

        public List<Salas> BuscarPorTipo(string tipo)
        {
            // Lógica de negocio:
            // No permitir búsqueda con tipo vacío
            if (string.IsNullOrWhiteSpace(tipo))
                throw new Exception("lbTipoInvalido");

            return iConexion!.Salas!
                .Where(s => s.Tipo == tipo)
                .ToList();
        }
    }
}
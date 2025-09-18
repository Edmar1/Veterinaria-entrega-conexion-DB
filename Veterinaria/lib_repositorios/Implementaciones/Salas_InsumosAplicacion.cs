using lib_dominio.Entidades;
using lib_repositorios.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace lib_repositorios.Implementaciones
{
    public class Salas_InsumosAplicacion : ISalas_InsumosAplicacion
    {
        private IConexion? IConexion = null;

        public Salas_InsumosAplicacion(IConexion iConexion)
        {
            this.IConexion = iConexion;
        }

        public void Configurar(string StringConexion)
        {
            this.IConexion!.StringConexion = StringConexion;
        }

        public Salas_Insumos? Guardar(Salas_Insumos? entidad)
        {
            if (entidad == null)                         // Lógica de negocio:
                throw new Exception("lbFaltaInformacion");
            // -> No se puede guardar un registro vacío.

            if (entidad.Id != 0)                         // Lógica de negocio:
                throw new Exception("lbYaSeGuardo");
            // -> No se puede guardar si ya tiene Id.

            if (entidad.Id_Sala == 0 || entidad.Id_Insumo == 0) // Lógica de negocio:
                throw new Exception("lbDatosIncompletos");
            // -> Ambos campos son obligatorios para la relación.

            this.IConexion!.Salas_Insumos!.Add(entidad);
            this.IConexion.SaveChanges();
            return entidad;
        }

        public Salas_Insumos? Modificar(Salas_Insumos? entidad)
        {
            if (entidad == null)                         // Lógica de negocio:
                throw new Exception("lbFaltaInformacion");
            if (entidad.Id == 0)                         // Lógica de negocio:
                throw new Exception("lbNoSeGuardo");

            var entry = this.IConexion!.Entry<Salas_Insumos>(entidad);
            entry.State = EntityState.Modified;
            this.IConexion.SaveChanges();
            return entidad;
        }

        public Salas_Insumos? Borrar(Salas_Insumos? entidad)
        {
            if (entidad == null)                         // Lógica de negocio:
                throw new Exception("lbFaltaInformacion");
            if (entidad.Id == 0)                         // Lógica de negocio:
                throw new Exception("lbNoSeGuardo");

            this.IConexion!.Salas_Insumos!.Remove(entidad);
            this.IConexion.SaveChanges();
            return entidad;
        }

        public List<Salas_Insumos> Listar()
        {
            return this.IConexion!.Salas_Insumos!.Take(20).ToList();
        }

        public List<Salas_Insumos> BuscarPorSala(int idSala)
        {
            return this.IConexion!.Salas_Insumos!
                .Where(x => x.Id_Sala == idSala)
                .ToList();
        }
    }
}

using lib_dominio.Entidades;
using lib_repositorios.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace lib_repositorios.Implementaciones
{
    public class EquiposAplicacion : IEquiposAplicacion
    {
        private readonly IConexion? IConexion;

        public EquiposAplicacion(IConexion iConexion)
        {
            this.IConexion = iConexion;
        }

        public void Configurar(string StringConexion)
        {
            this.IConexion!.StringConexion = StringConexion;
        }

        public Equipos? Guardar(Equipos? entidad)
        {
            // Lógica de negocio: validar que el equipo tenga nombre, marca y fecha de adquisición
            if (entidad == null || string.IsNullOrWhiteSpace(entidad.Nombre)
                || string.IsNullOrWhiteSpace(entidad.Marca)
                || entidad.Fecha_Adquisicion == default)
                throw new Exception("lbEquipoInvalido");

            if (entidad.Id != 0)
                throw new Exception("lbYaSeGuardo");

            this.IConexion!.Equipos!.Add(entidad);
            this.IConexion.SaveChanges();
            return entidad;
        }

        public Equipos? Modificar(Equipos? entidad)
        {
            if (entidad == null)
                throw new Exception("lbFaltaInformacion");
            if (entidad.Id == 0)
                throw new Exception("lbNoSeGuardo");

            var entry = this.IConexion!.Entry(entidad);
            entry.State = EntityState.Modified;
            this.IConexion.SaveChanges();
            return entidad;
        }

        public Equipos? Borrar(Equipos? entidad)
        {
            if (entidad == null)
                throw new Exception("lbFaltaInformacion");
            if (entidad.Id == 0)
                throw new Exception("lbNoSeGuardo");

            this.IConexion!.Equipos!.Remove(entidad);
            this.IConexion.SaveChanges();
            return entidad;
        }

        public List<Equipos> Listar()
        {
            return this.IConexion!.Equipos!.Take(20).ToList();
        }
    }
}
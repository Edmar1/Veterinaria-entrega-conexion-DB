using lib_dominio.Entidades;
using lib_repositorios.Interfaces;
using Microsoft.EntityFrameworkCore;
namespace lib_repositorios.Implementaciones
{
    public class SedesAplicacion : ISedesAplicacion
    {
        private IConexion? IConexion = null;
        public SedesAplicacion(IConexion iConexion)
        {
            this.IConexion = iConexion;
        }
        public void Configurar(string StringConexion)
        {
            this.IConexion!.StringConexion = StringConexion;
        }
        public Sedes? Borrar(Sedes? entidad)
        {
            if (entidad == null)
                throw new Exception("lbFaltaInformacion");
            if (entidad!.Id == 0)
                throw new Exception("lbNoSeGuardo");
            this.IConexion!.Sedes!.Remove(entidad);
            this.IConexion.SaveChanges();
            return entidad;
        }
        public Sedes? Guardar(Sedes? entidad)
        {
            if (entidad == null)
                throw new Exception("lbFaltaInformacion");
            if (entidad.Id != 0)
                throw new Exception("lbYaSeGuardo");
            this.IConexion!.Sedes!.Add(entidad);
            this.IConexion.SaveChanges();
            return entidad;
        }
        public List<Sedes> Listar()
        {
            return this.IConexion!.Sedes!.Take(20).ToList();
        }
      
        public Sedes? Modificar(Sedes? entidad)
        {
            if (entidad == null)
                throw new Exception("lbFaltaInformacion");
            if (entidad!.Id == 0)
                throw new Exception("lbNoSeGuardo");
            var entry = this.IConexion!.Entry<Sedes>(entidad);
            entry.State = EntityState.Modified;
            this.IConexion.SaveChanges();
            return entidad;
        }

        // Nuevo método para buscar por teléfono
        public List<Sedes> BuscarPorTelefono(string telefono)
        {
            return this.IConexion!.Sedes!
                .Where(x => x.Telefono == telefono)
                .ToList();
        }
    }
}
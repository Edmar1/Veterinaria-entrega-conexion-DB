using lib_dominio.Entidades;
using lib_repositorios.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace lib_repositorios.Implementaciones
{
    public class Salas_MedicamentosAplicacion : ISalas_MedicamentosAplicacion
    {
        private IConexion? IConexion = null;

        public Salas_MedicamentosAplicacion(IConexion iConexion)
        {
            this.IConexion = iConexion;
        }

        public void Configurar(string StringConexion)
        {
            this.IConexion!.StringConexion = StringConexion;
        }

        public Salas_Medicamentos? Guardar(Salas_Medicamentos? entidad)
        {
            if (entidad == null)                       // Lógica de negocio:
                throw new Exception("lbFaltaInformacion");
            if (entidad.Id != 0)                       // Lógica de negocio:
                throw new Exception("lbYaSeGuardo");
            if (entidad.Id_Sala == 0 || entidad.Id_Medicamento == 0) // Lógica de negocio:
                throw new Exception("lbDatosIncompletos");

            this.IConexion!.Salas_Medicamentos!.Add(entidad);
            this.IConexion.SaveChanges();
            return entidad;
        }

        public Salas_Medicamentos? Modificar(Salas_Medicamentos? entidad)
        {
            if (entidad == null)                       // Lógica de negocio:
                throw new Exception("lbFaltaInformacion");
            if (entidad.Id == 0)                       // Lógica de negocio:
                throw new Exception("lbNoSeGuardo");

            var entry = this.IConexion!.Entry<Salas_Medicamentos>(entidad);
            entry.State = EntityState.Modified;
            this.IConexion.SaveChanges();
            return entidad;
        }

        public Salas_Medicamentos? Borrar(Salas_Medicamentos? entidad)
        {
            if (entidad == null)                       // Lógica de negocio:
                throw new Exception("lbFaltaInformacion");
            if (entidad.Id == 0)                       // Lógica de negocio:
                throw new Exception("lbNoSeGuardo");

            this.IConexion!.Salas_Medicamentos!.Remove(entidad);
            this.IConexion.SaveChanges();
            return entidad;
        }

        public List<Salas_Medicamentos> Listar()
        {
            return this.IConexion!.Salas_Medicamentos!.Take(20).ToList();
        }

        public List<Salas_Medicamentos> BuscarPorSala(int idSala)
        {
            return this.IConexion!.Salas_Medicamentos!
                .Where(x => x.Id_Sala == idSala)
                .ToList();
        }
    }
}

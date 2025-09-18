using lib_dominio.Entidades;
using lib_repositorios.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace lib_repositorios.Implementaciones
{
    public class CitasAplicacion : ICitasAplicacion
    {
        private IConexion? conexion;

        public CitasAplicacion(IConexion conexion)
        {
            this.conexion = conexion;
        }

        public void Configurar(string StringConexion)
        {
            this.conexion!.StringConexion = StringConexion;
        }

        public Citas? Guardar(Citas? entidad)
        {
            if (entidad == null)                            // Lógica de negocio:
                throw new Exception("lbFaltaInformacion");
            // -> No se puede guardar una cita sin datos.

            if (entidad.Id != 0)                            // Lógica de negocio:
                throw new Exception("lbYaSeGuardo");
            // -> Una cita nueva debe tener Id = 0. 
            //    Si ya tiene Id, significa que ya existe en la BD.

            if (string.IsNullOrWhiteSpace(entidad.Motivo))  // Lógica de negocio:
                throw new Exception("lbMotivoObligatorio");
            // -> El motivo de la cita no puede estar vacío.

            if (entidad.Fecha <= DateTime.Now)              // Lógica de negocio:
                throw new Exception("lbFechaInvalida");
            // -> La fecha de la cita debe ser futura.

            this.conexion!.Citas!.Add(entidad);
            this.conexion.SaveChanges();
            return entidad;
        }

        public Citas? Modificar(Citas? entidad)
        {
            if (entidad == null)                            // Lógica de negocio:
                throw new Exception("lbFaltaInformacion");
            // -> No se puede modificar una cita sin datos.

            if (entidad.Id == 0)                            // Lógica de negocio:
                throw new Exception("lbNoSeGuardo");
            // -> Solo se puede modificar una cita que ya exista en la BD.

            if (entidad.Fecha <= DateTime.Now)              // Lógica de negocio:
                throw new Exception("lbFechaInvalida");
            // -> No se puede reprogramar una cita a fechas pasadas.

            var entry = this.conexion!.Entry<Citas>(entidad);
            entry.State = EntityState.Modified;
            this.conexion.SaveChanges();
            return entidad;
        }

        public Citas? Borrar(Citas? entidad)
        {
            if (entidad == null)                            // Lógica de negocio:
                throw new Exception("lbFaltaInformacion");
            // -> No se puede borrar algo que no existe.

            if (entidad.Id == 0)                            // Lógica de negocio:
                throw new Exception("lbNoSeGuardo");
            // -> Solo se puede borrar una cita que ya fue guardada en la BD.

            this.conexion!.Citas!.Remove(entidad);
            this.conexion.SaveChanges();
            return entidad;
        }

        public List<Citas> Listar()
        {
            return this.conexion!.Citas!.ToList();
        }

        public List<Citas> BuscarPorFecha(DateTime fecha)
        {
            return this.conexion!.Citas!
                .Where(x => x.Fecha.Date == fecha.Date)
                .ToList();
        }
    }
}

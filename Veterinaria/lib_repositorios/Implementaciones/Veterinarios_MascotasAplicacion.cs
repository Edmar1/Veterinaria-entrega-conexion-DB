using lib_dominio.Entidades;
using lib_repositorios.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace lib_repositorios.Implementaciones
{
    public class Veterinarios_MascotasAplicacion : IVeterinarios_MascotasAplicacion
    {
        private IConexion? IConexion = null;

        public Veterinarios_MascotasAplicacion(IConexion iConexion)
        {
            this.IConexion = iConexion;
        }

        public void Configurar(string StringConexion)
        {
            this.IConexion!.StringConexion = StringConexion;
        }

        public Veterinarios_Mascotas? Guardar(Veterinarios_Mascotas? entidad)
        {
            if (entidad == null)                       // Lógica de negocio: no se puede guardar algo vacío
                throw new Exception("lbFaltaInformacion");
            if (entidad.Id != 0)                       // Lógica de negocio: solo se pueden guardar entidades nuevas
                throw new Exception("lbYaSeGuardo");
            if (entidad.Id_Veterinario == 0 || entidad.Id_Mascota == 0) // Lógica de negocio: relaciones incompletas
                throw new Exception("lbDatosIncompletos");

            this.IConexion!.Veterinarios_Mascotas!.Add(entidad);
            this.IConexion.SaveChanges();
            return entidad;
        }

        public Veterinarios_Mascotas? Modificar(Veterinarios_Mascotas? entidad)
        {
            if (entidad == null)                       // Lógica de negocio: no se puede modificar algo vacío
                throw new Exception("lbFaltaInformacion");
            if (entidad.Id == 0)                       // Lógica de negocio: solo se pueden modificar entidades guardadas
                throw new Exception("lbNoSeGuardo");

            var entry = this.IConexion!.Entry<Veterinarios_Mascotas>(entidad);
            entry.State = EntityState.Modified;
            this.IConexion.SaveChanges();
            return entidad;
        }

        public Veterinarios_Mascotas? Borrar(Veterinarios_Mascotas? entidad)
        {
            if (entidad == null)                       // Lógica de negocio: no se puede borrar algo vacío
                throw new Exception("lbFaltaInformacion");
            if (entidad.Id == 0)                       // Lógica de negocio: solo se pueden borrar entidades guardadas
                throw new Exception("lbNoSeGuardo");

            this.IConexion!.Veterinarios_Mascotas!.Remove(entidad);
            this.IConexion.SaveChanges();
            return entidad;
        }

        public List<Veterinarios_Mascotas> Listar()
        {
            return this.IConexion!.Veterinarios_Mascotas!.Take(20).ToList();
        }

        public List<Veterinarios_Mascotas> BuscarPorVeterinario(int idVeterinario)
        {
            return this.IConexion!.Veterinarios_Mascotas!
                .Where(x => x.Id_Veterinario == idVeterinario)
                .ToList();
        }
    }
}

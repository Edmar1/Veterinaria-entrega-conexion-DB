using lib_dominio.Entidades;
using lib_repositorios.Implementaciones;
using lib_repositorios.Interfaces;
using Microsoft.EntityFrameworkCore;
using ut_presentacion.Nucleo;

namespace ut_presentacion.Repositorios
{
    [TestClass]
    public class MascotasPrueba
    {
        private readonly IConexion? iConexion;
        private List<Mascotas>? lista;
        private Mascotas? entidad;
        // Lista para rastrear propietarios creados durante el test
        private List<int> propietariosCreados = new List<int>();

        public MascotasPrueba()
        {
            iConexion = new Conexion();
            iConexion.StringConexion = Configuracion.ObtenerValor("StringConexion");
        }

        [TestMethod]
        public void Ejecutar()
        {
            Assert.AreEqual(true, Guardar());
            Assert.AreEqual(true, Modificar());
            Assert.AreEqual(true, Listar());
            Assert.AreEqual(true, Borrar());
        }

        public bool Listar()
        {
            this.lista = this.iConexion!.Mascotas!.ToList();
            return lista.Count > 0;
        }

        public bool Guardar()
        {
            var propietario = EntidadesNucleo.Propietarios();
            this.iConexion!.Propietarios!.Add(propietario!);
            this.iConexion!.SaveChanges();

            // Rastrear el propietario creado para limpieza posterior
            propietariosCreados.Add(propietario.Id);

            this.entidad = EntidadesNucleo.Mascotas();
            this.iConexion!.Mascotas!.Add(this.entidad);
            this.iConexion!.SaveChanges();
            return true;
        }

        public bool Modificar()
        {
            this.entidad!.Nombre = "Mascota Modificada";
            var entry = this.iConexion!.Entry<Mascotas>(this.entidad);
            entry.State = EntityState.Modified;
            this.iConexion!.SaveChanges();
            return true;
        }

        public bool Borrar()
        {
            // Borrar la mascota
            this.iConexion!.Mascotas!.Remove(this.entidad!);
            this.iConexion!.SaveChanges();

            // Limpiar propietarios creados durante el test para evitar datos huérfanos
            foreach (var propietarioId in propietariosCreados)
            {
                var propietario = this.iConexion.Propietarios!.Find(propietarioId);
                if (propietario != null)
                {
                    this.iConexion.Propietarios.Remove(propietario);
                }
            }

            // Solo hacer SaveChanges si hay propietarios que eliminar
            if (propietariosCreados.Count > 0)
            {
                this.iConexion.SaveChanges();
                propietariosCreados.Clear(); // Limpiar la lista de rastreo
            }

            return true;
        }
    }
}
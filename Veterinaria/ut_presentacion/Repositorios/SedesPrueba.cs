using lib_dominio.Entidades;
using lib_repositorios.Implementaciones;
using lib_repositorios.Interfaces;
using Microsoft.EntityFrameworkCore;
using ut_presentacion.Nucleo;

namespace ut_presentacion.Repositorios
{
    [TestClass]
    public class SedesPrueba
    {
        private readonly IConexion? iConexion;
        private List<Sedes>? lista;
        private Sedes? entidad;

        public SedesPrueba()
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
            this.lista = this.iConexion!.Sedes!.ToList();
            return lista.Count > 0;
        }

        public bool Guardar()
        {
            // Aquí se puede usar una factoría como EntidadesNucleo.Sedes() 
            // si ya la tienes definida para generar datos de prueba
            this.entidad = new Sedes()
            {
                Nombre = "Sede Central AWSD",
                Direccion = "Calle Principal #123",
                Barrio = "Bogotá",
                Parqueadero = true,
                Telefono = "123123123"
            };

            this.iConexion!.Sedes!.Add(this.entidad);
            this.iConexion!.SaveChanges();
            return true;
        }

        public bool Modificar()
        {
            this.entidad!.Nombre = "Sede Central Actualizada";
            var entry = this.iConexion!.Entry<Sedes>(this.entidad);
            entry.State = EntityState.Modified;
            this.iConexion!.SaveChanges();
            return true;
        }

        public bool Borrar()
        {
            this.iConexion!.Sedes!.Remove(this.entidad!);
            this.iConexion!.SaveChanges();
            return true;
        }
    }
}

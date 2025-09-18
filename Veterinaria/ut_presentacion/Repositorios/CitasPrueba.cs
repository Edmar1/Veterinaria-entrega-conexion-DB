using lib_dominio.Entidades;
using lib_repositorios.Implementaciones;
using lib_repositorios.Interfaces;
using Microsoft.EntityFrameworkCore;
using ut_presentacion.Nucleo;

namespace ut_presentacion.Repositorios
{
    [TestClass]
    public class CitasPrueba
    {
        private readonly IConexion? iConexion;
        private List<Citas>? lista;
        private Citas? entidad;

        public CitasPrueba()
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
            this.lista = this.iConexion!.Citas!.ToList();
            return lista.Count > 0;
        }

        public bool Guardar()
        {

            this.entidad = EntidadesNucleo.Citas()!;
            this.iConexion!.Citas!.Add(this.entidad);
            this.iConexion!.SaveChanges();
            return true;
        }

        public bool Modificar()
        {
            this.entidad!.Motivo = "Motivo Actualizado";
            var entry = this.iConexion!.Entry<Citas>(this.entidad);
            entry.State = EntityState.Modified;
            this.iConexion!.SaveChanges();
            return true;
        }

        public bool Borrar()
        {
            this.iConexion!.Citas!.Remove(this.entidad!);
            this.iConexion!.SaveChanges();
            return true;
        }
    }
}
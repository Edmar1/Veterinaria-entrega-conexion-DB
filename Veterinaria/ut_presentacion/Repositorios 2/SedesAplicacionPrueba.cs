using lib_dominio.Entidades;
using lib_repositorios.Implementaciones;
using lib_repositorios.Interfaces;
using Microsoft.EntityFrameworkCore;
using ut_presentacion.Nucleo;

namespace ut_presentacion.Repositorios
{
    [TestClass]
    public class SedesAplicacionPrueba
    {
        private readonly IConexion? iConexion;
        private ISedesAplicacion? app;
        private List<Sedes>? lista;
        private Sedes? entidad;

        public SedesAplicacionPrueba()
        {
            iConexion = new Conexion();
            iConexion.StringConexion = Configuracion.ObtenerValor("StringConexion");
            app = new SedesAplicacion(iConexion);
        }

        [TestMethod]
        public void Ejecutar()
        {
            Assert.AreEqual(true, Guardar());
            Assert.AreEqual(true, Modificar());
            Assert.AreEqual(true, Listar());
            Assert.AreEqual(true, BuscarPorTelefono());
            Assert.AreEqual(true, Borrar());
        }

        public bool Listar()
        {
            this.lista = app!.Listar();
            return lista.Count > 0;
        }

        public bool Guardar()
        {
            this.entidad = EntidadesNucleo.Sedes()!;
            var guardada = app!.Guardar(this.entidad);
            return guardada != null && guardada.Id > 0;
        }

        public bool Modificar()
        {
            if (this.entidad == null) return false;
            this.entidad.Barrio = "Barrio Actualizado";
            var modificada = app!.Modificar(this.entidad);
            return modificada != null && modificada.Barrio == "Barrio Actualizado";
        }

        public bool BuscarPorTelefono()
        {
            if (this.entidad == null) return false;
            var resultado = app!.BuscarPorTelefono(this.entidad.Telefono!);
            return resultado.Any();
        }

        public bool Borrar()
        {
            if (this.entidad == null) return false;
            var borrada = app!.Borrar(this.entidad);
            return borrada != null;
        }
    }
}

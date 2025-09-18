using lib_dominio.Entidades;
using lib_repositorios.Implementaciones;
using lib_repositorios.Interfaces;
using Microsoft.EntityFrameworkCore;
using ut_presentacion.Nucleo;

namespace ut_presentacion.Repositorios
{
    [TestClass]
    public class Tel_PropietariosAplicacionPrueba
    {
        private readonly IConexion? iConexion;
        private ITel_PropietariosAplicacion? app;
        private Tel_Propietarios? entidad;

        public Tel_PropietariosAplicacionPrueba()
        {
            iConexion = new Conexion();
            iConexion.StringConexion = Configuracion.ObtenerValor("StringConexion");
            app = new Tel_PropietariosAplicacion(iConexion);
        }

        [TestMethod]
        public void Ejecutar()
        {
            Assert.AreEqual(true, Guardar());
            Assert.AreEqual(true, Modificar());
            Assert.AreEqual(true, Listar());
            Assert.AreEqual(true, BuscarPorNumero());
            Assert.AreEqual(true, Borrar());
        }

        public bool Guardar()
        {
            entidad = new Tel_Propietarios()
            {
                Telefono = "3216549870",  // Lógica de negocio: mínimo 7 dígitos
                Id_Propietario = 1        // Debe existir en BD
            };

            var guardado = app!.Guardar(entidad);
            return guardado != null && guardado.Id > 0;
        }

        public bool Modificar()
        {
            if (entidad == null) return false;

            entidad.Telefono = "3100000000";
            var modificado = app!.Modificar(entidad);
            return modificado != null && modificado.Telefono == "3100000000";
        }

        public bool Listar()
        {
            var lista = app!.Listar();
            return lista.Count > 0;
        }

        public bool BuscarPorNumero()
        {
            if (entidad == null) return false;

            var resultado = app!.BuscarPorNumero("310");
            return resultado.Any();
        }

        public bool Borrar()
        {
            if (entidad == null) return false;

            var borrado = app!.Borrar(entidad);
            return borrado != null;
        }
    }
}
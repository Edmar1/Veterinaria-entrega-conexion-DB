using lib_dominio.Entidades;
using lib_repositorios.Implementaciones;
using lib_repositorios.Interfaces;
using Microsoft.EntityFrameworkCore;
using ut_presentacion.Nucleo;

namespace ut_presentacion.Repositorios
{
    [TestClass]
    public class PagosAplicacionPrueba
    {
        private readonly IConexion? iConexion;
        private IPagosAplicacion? app;
        private Pagos? entidad;

        public PagosAplicacionPrueba()
        {
            iConexion = new Conexion();
            iConexion.StringConexion = Configuracion.ObtenerValor("StringConexion");
            app = new PagosAplicacion(iConexion);
        }

        [TestMethod]
        public void Ejecutar()
        {
            Assert.AreEqual(true, Guardar());
            Assert.AreEqual(true, Modificar());
            Assert.AreEqual(true, Listar());
            Assert.AreEqual(true, BuscarPorEstado());
            Assert.AreEqual(true, Borrar());
        }

        public bool Guardar()
        {
            // Crear pago válido
            entidad = new Pagos
            {
                Fecha = DateTime.Now,
                Metodo = "Tarjeta",
                Valor = 150000,
                Estado = "Pendiente",
                Id_Propietario = 1
            };

            var guardado = app!.Guardar(entidad);
            return guardado != null && guardado.Id > 0;
        }

        public bool Modificar()
        {
            if (entidad == null) return false;
            entidad.Estado = "Pagado";

            var modificado = app!.Modificar(entidad);
            return modificado != null && modificado.Estado == "Pagado";
        }

        public bool Listar()
        {
            var lista = app!.Listar();
            return lista.Count > 0;
        }

        public bool BuscarPorEstado()
        {
            if (entidad == null) return false;
            var resultado = app!.BuscarPorEstado(entidad.Estado);
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

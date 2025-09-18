using lib_dominio.Entidades;
using lib_repositorios.Implementaciones;
using lib_repositorios.Interfaces;
using Microsoft.EntityFrameworkCore;
using ut_presentacion.Nucleo;

namespace ut_presentacion.Repositorios
{
    [TestClass]
    public class CitasAplicacionPrueba
    {
        private readonly IConexion? iConexion;
        private ICitasAplicacion? app;
        private Citas? entidad;

        public CitasAplicacionPrueba()
        {
            iConexion = new Conexion();
            iConexion.StringConexion = Configuracion.ObtenerValor("StringConexion");
            app = new CitasAplicacion(iConexion);
        }

        [TestMethod]
        public void Ejecutar()
        {
            Assert.AreEqual(true, Guardar());
            Assert.AreEqual(true, Modificar());
            Assert.AreEqual(true, Listar());
            Assert.AreEqual(true, BuscarPorFecha());
            Assert.AreEqual(true, Borrar());
        }

        public bool Guardar()
        {
            // Crear cita válida
            entidad = new Citas
            {
                Motivo = "Vacunación",
                Fecha = DateTime.Now.AddDays(2),
                Hora = "10:00 AM",
                Sede = "Principal",
                Id_Propietario = 1
            };

            var guardado = app!.Guardar(entidad);
            return guardado != null && guardado.Id > 0;
        }

        public bool Modificar()
        {
            if (entidad == null) return false;
            entidad.Motivo = "Control Actualizado";
            entidad.Fecha = DateTime.Now.AddDays(5);

            var modificado = app!.Modificar(entidad);
            return modificado != null && modificado.Motivo == "Control Actualizado";
        }

        public bool Listar()
        {
            var lista = app!.Listar();
            return lista.Count > 0;
        }

        public bool BuscarPorFecha()
        {
            if (entidad == null) return false;
            var resultado = app!.BuscarPorFecha(entidad.Fecha);
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

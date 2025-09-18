using lib_dominio.Entidades;
using lib_repositorios.Implementaciones;
using lib_repositorios.Interfaces;
using Microsoft.EntityFrameworkCore;
using ut_presentacion.Nucleo;

namespace ut_presentacion.Repositorios
{
    [TestClass]
    public class EmpleadosAplicacionPrueba
    {
        private readonly IConexion? iConexion;
        private IEmpleadosAplicacion? app;
        private Empleados? entidad;

        public EmpleadosAplicacionPrueba()
        {
            iConexion = new Conexion();
            iConexion.StringConexion = Configuracion.ObtenerValor("StringConexion");
            app = new EmpleadosAplicacion(iConexion);
        }

        [TestMethod]
        public void Ejecutar()
        {
            Assert.AreEqual(true, Guardar());
            Assert.AreEqual(true, Modificar());
            Assert.AreEqual(true, Listar());
            Assert.AreEqual(true, BuscarPorCargo());
            Assert.AreEqual(true, Borrar());
        }

        public bool Guardar()
        {
            // Asegurar una sede existente
            var sede = iConexion!.Sedes!.FirstOrDefault();
            if (sede == null)
            {
                sede = EntidadesNucleo.Sedes()!;
                iConexion.Sedes!.Add(sede);
                iConexion.SaveChanges();
            }

            // Crear empleado ligado a la sede
            entidad = new Empleados
            {
                Documento = "123456",
                Nombre = "Juan Pérez",
                Cargo = "Veterinario",
                Fecha_Ingreso = DateTime.Now,
                Horario = "Lunes a Viernes 8-5",
                Telefono = "3001234567",
                Id_Sede = sede.Id
            };

            var guardado = app!.Guardar(entidad);
            return guardado != null && guardado.Id > 0;
        }

        public bool Modificar()
        {
            if (entidad == null) return false;
            entidad.Cargo = "Administrador";
            var modificado = app!.Modificar(entidad);
            return modificado != null && modificado.Cargo == "Administrador";
        }

        public bool Listar()
        {
            var lista = app!.Listar();
            return lista.Count > 0;
        }

        public bool BuscarPorCargo()
        {
            if (entidad == null) return false;
            var resultado = app!.BuscarPorCargo(entidad.Cargo);
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

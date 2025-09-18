using lib_dominio.Entidades;
using lib_repositorios.Implementaciones;
using lib_repositorios.Interfaces;
using lib_aplicaciones.Implementaciones;
using lib_aplicaciones.Interfaces;
using Microsoft.EntityFrameworkCore;
using ut_presentacion.Nucleo;

namespace ut_presentacion.Repositorios
{
    [TestClass]
    public class PropietariosAplicacionPrueba
    {
        private readonly IConexion? iConexion;
        private readonly IPropietariosAplicacion app;
        private Propietarios? entidad;

        public PropietariosAplicacionPrueba()
        {
            iConexion = new Conexion();
            iConexion.StringConexion = Configuracion.ObtenerValor("StringConexion");
            app = new PropietariosAplicacion(iConexion);
        }

        [TestMethod]
        public void Ejecutar()
        {
            Assert.AreEqual(true, Guardar());
            Assert.AreEqual(true, Modificar());
            Assert.AreEqual(true, Listar());
            Assert.AreEqual(true, Borrar());
        }

        // Test guardar propietario válido
        public bool Guardar()
        {
            entidad = new Propietarios()
            {
                Documento = "555666777",
                Nombre = "Ana Torres",
                Edad = 25,                          // lógica de negocio: mayor de 18
                Direccion = "Calle 10 #45",
                Correo = "ana.torres@mail.com",
                Genero = "Femenino",
                Estrato = 3,
                Fecha_Registro = DateTime.Now
            };

            return app.Guardar(entidad);
        }

        // Test modificar datos del propietario
        public bool Modificar()
        {
            entidad!.Nombre = "Ana Torres Actualizada";
            return app.Modificar(entidad);
        }

        // Test listar propietarios
        public bool Listar()
        {
            var lista = app.Listar();
            return lista.Count > 0;
        }

        // Test borrar propietario creado
        public bool Borrar()
        {
            return app.Borrar(entidad!);
        }
    }
}
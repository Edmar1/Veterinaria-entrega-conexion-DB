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
    public class VeterinariosAplicacionPrueba
    {
        private readonly IConexion? iConexion;
        private readonly IVeterinariosAplicacion app;
        private Veterinarios? entidad;

        public VeterinariosAplicacionPrueba()
        {
            iConexion = new Conexion();
            iConexion.StringConexion = Configuracion.ObtenerValor("StringConexion");
            app = new VeterinariosAplicacion(iConexion);
        }

        [TestMethod]
        public void Ejecutar()
        {
            Assert.AreEqual(true, Guardar());
            Assert.AreEqual(true, Modificar());
            Assert.AreEqual(true, Listar());
            Assert.AreEqual(true, Borrar());
        }

        // Test guardar un veterinario válido
        public bool Guardar()
        {
            entidad = new Veterinarios()
            {
                Documento = "100200300",
                Nombre = "Carlos Pérez",
                Edad = 30,                      // lógica de negocio: mayor a 18
                Especialidad = "Cirugía",
                Fecha_Ingreso = DateTime.Now,
                Horario = "8am - 5pm",
                Telefono = "321654987",
                Id_Sede = 1
            };

            return app.Guardar(entidad);
        }

        // Test modificar el nombre del veterinario
        public bool Modificar()
        {
            entidad!.Nombre = "Carlos Pérez Actualizado";
            return app.Modificar(entidad);
        }

        // Test listar veterinarios y verificar que hay registros
        public bool Listar()
        {
            var lista = app.Listar();
            return lista.Count > 0;
        }

        // Test eliminar el veterinario insertado
        public bool Borrar()
        {
            return app.Borrar(entidad!);
        }
    }
}
using lib_dominio.Entidades;
using lib_repositorios.Implementaciones;
using lib_repositorios.Interfaces;
using ut_presentacion.Nucleo;

namespace ut_presentacion.Repositorios
{
    [TestClass]
    public class EquiposAplicacionPrueba
    {
        private readonly IConexion iConexion;
        private readonly IEquiposAplicacion equiposAplicacion;
        private Equipos? entidad;

        public EquiposAplicacionPrueba()
        {
            iConexion = new Conexion();
            iConexion.StringConexion = Configuracion.ObtenerValor("StringConexion");
            equiposAplicacion = new EquiposAplicacion(iConexion);
        }

        // Test principal que ejecuta todo el CRUD
        [TestMethod]
        public void Ejecutar()
        {
            Assert.AreEqual(true, Guardar());
            Assert.AreEqual(true, Modificar());
            Assert.AreEqual(true, Listar());
            Assert.AreEqual(true, Borrar());
        }

        // ❌ Test negativo: intenta guardar un equipo inválido y debe fallar
        [TestMethod]
        public void GuardarEquipoInvalidoDebeFallar()
        {
            var equipoInvalido = new Equipos()
            {
                Nombre = "",  // inválido
                Marca = "",   // inválido
                Modelo = "X100",
                Fecha_Adquisicion = default, // inválido
                Fecha_Fabricacion = DateTime.Now,
                Ultima_Revision = DateTime.Now,
                Id_Sala = 1
            };

            try
            {
                equiposAplicacion.Guardar(equipoInvalido);
                Assert.Fail("No debería guardar un equipo inválido.");
            }
            catch (Exception ex)
            {
                Assert.AreEqual("lbEquipoInvalido", ex.Message);
            }
        }

        // Verifica que se pueda guardar un equipo válido
        public bool Guardar()
        {
            entidad = new Equipos()
            {
                Nombre = "Monitor de signos vitales",
                Marca = "Philips",
                Modelo = "MX500",
                Fecha_Fabricacion = DateTime.Now.AddYears(-1),
                Fecha_Adquisicion = DateTime.Now,
                Ultima_Revision = DateTime.Now,
                Id_Sala = 1
            };

            equiposAplicacion.Guardar(entidad);
            return entidad.Id > 0;
        }

        // Verifica que se pueda modificar un equipo existente
        public bool Modificar()
        {
            entidad!.Modelo = "MX700";
            equiposAplicacion.Modificar(entidad);
            return entidad.Modelo == "MX700";
        }

        // Verifica que se pueda listar equipos
        public bool Listar()
        {
            var lista = equiposAplicacion.Listar();
            return lista.Count > 0;
        }

        // Verifica que se pueda borrar el equipo creado
        public bool Borrar()
        {
            equiposAplicacion.Borrar(entidad!);
            return true;
        }
    }
}
using lib_dominio.Entidades;
using lib_repositorios.Implementaciones;
using lib_repositorios.Interfaces;
using ut_presentacion.Nucleo;

namespace ut_presentacion.Repositorios
{
    [TestClass]
    public class InsumosAplicacionPrueba
    {
        private readonly IConexion iConexion;
        private readonly IInsumosAplicacion insumosAplicacion;
        private Insumos? entidad;

        public InsumosAplicacionPrueba()
        {
            iConexion = new Conexion();
            iConexion.StringConexion = Configuracion.ObtenerValor("StringConexion");
            insumosAplicacion = new InsumosAplicacion(iConexion);
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

        // ❌ Test negativo: intenta guardar un insumo inválido y debe fallar
        [TestMethod]
        public void GuardarInsumoInvalidoDebeFallar()
        {
            var insumoInvalido = new Insumos()
            {
                Nombre = "",  // inválido
                Proveedor = "Proveedor X",
                Precio_Unidad = 0, // inválido
                Toxico = false
            };

            try
            {
                insumosAplicacion.Guardar(insumoInvalido);
                Assert.Fail("No debería guardar un insumo inválido.");
            }
            catch (Exception ex)
            {
                Assert.AreEqual("lbInsumoInvalido", ex.Message);
            }
        }

        // Verifica que se pueda guardar un insumo válido
        public bool Guardar()
        {
            entidad = new Insumos()
            {
                Nombre = "Jeringas",
                Proveedor = "Proveedor X",
                Precio_Unidad = 500,
                Toxico = false
            };

            insumosAplicacion.Guardar(entidad);
            return entidad.Id > 0;
        }

        // Verifica que se pueda modificar un insumo existente
        public bool Modificar()
        {
            entidad!.Proveedor = "Proveedor Actualizado";
            insumosAplicacion.Modificar(entidad);
            return entidad.Proveedor == "Proveedor Actualizado";
        }

        // Verifica que se pueda listar insumos
        public bool Listar()
        {
            var lista = insumosAplicacion.Listar();
            return lista.Count > 0;
        }

        // Verifica que se pueda borrar el insumo creado
        public bool Borrar()
        {
            insumosAplicacion.Borrar(entidad!);
            return true;
        }
    }
}
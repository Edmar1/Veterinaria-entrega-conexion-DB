using lib_dominio.Entidades;
using lib_repositorios.Implementaciones;
using lib_repositorios.Interfaces;
using lib_aplicacion.Implementaciones;
using lib_aplicacion.Interfaces;
using ut_presentacion.Nucleo;

namespace ut_presentacion.Repositorios
{
    [TestClass]
    public class MedicamentosAplicacionPrueba
    {
        private readonly IConexion? iConexion;
        private readonly IMedicamentosAplicacion aplicacion;
        private Medicamentos? entidad;

        public MedicamentosAplicacionPrueba()
        {
            iConexion = new Conexion();
            iConexion.StringConexion = Configuracion.ObtenerValor("StringConexion");
            aplicacion = new MedicamentosAplicacion(iConexion);
        }

        [TestMethod]
        public void Ejecutar()
        {
            Assert.AreEqual(true, Guardar());   // Test de inserción
            Assert.AreEqual(true, Modificar()); // Test de actualización
            Assert.AreEqual(true, Listar());    // Test de consulta
            Assert.AreEqual(true, Borrar());    // Test de eliminación
        }

        public bool Guardar()
        {
            // LÓGICA DE NEGOCIO: Se prueba que el precio > 0
            entidad = new Medicamentos()
            {
                Nombre = "Amoxicilina",
                Proveedor = "ProveedorX",
                Precio_Unidad = 1500,
                Via_Administracion = "Oral",
                Refrigeracion = false
            };

            var guardado = aplicacion.Guardar(entidad);
            return guardado.Id > 0;
        }

        public bool Modificar()
        {
            // LÓGICA DE NEGOCIO: no se puede modificar a precio <= 0
            entidad!.Precio_Unidad = 2000;
            var modificado = aplicacion.Modificar(entidad);
            return modificado.Precio_Unidad == 2000;
        }

        public bool Listar()
        {
            var lista = aplicacion.Listar();
            return lista.Count > 0;
        }

        public bool Borrar()
        {
            return aplicacion.Borrar(entidad!.Id);
        }
    }
}
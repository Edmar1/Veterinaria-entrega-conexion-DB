using lib_dominio.Entidades;
using lib_repositorios.Implementaciones;
using lib_repositorios.Interfaces;
using Microsoft.EntityFrameworkCore;
using ut_presentacion.Nucleo;

namespace ut_presentacion.Repositorios
{
    [TestClass]
    public class SalasAplicacionPrueba
    {
        private readonly IConexion iConexion;
        private readonly ISalasAplicacion salasAplicacion;
        private Salas? entidad;

        public SalasAplicacionPrueba()
        {
            iConexion = new Conexion();
            iConexion.StringConexion = Configuracion.ObtenerValor("StringConexion");
            salasAplicacion = new SalasAplicacion(iConexion);
        }

        // Test principal que ejecuta CRUD completo (Guardar, Modificar, Listar, Borrar)
        [TestMethod]
        public void Ejecutar()
        {
            Assert.AreEqual(true, Guardar());
            Assert.AreEqual(true, Modificar());
            Assert.AreEqual(true, Listar());
            Assert.AreEqual(true, Borrar());
        }

        // ❌ Test negativo: intenta guardar una sala inválida y debe lanzar excepción
        [TestMethod]
        public void GuardarSalaInvalidaDebeFallar()
        {
            var salaInvalida = new Salas()
            {
                Numero = 0, // inválido
                Tipo = "",  // inválido
                Area = 20.5m,
                Id_Sede = 1
            };

            try
            {
                salasAplicacion.Guardar(salaInvalida);
                Assert.Fail("No debería guardar una sala inválida.");
            }
            catch (Exception ex)
            {
                Assert.AreEqual("lbSalaInvalida", ex.Message);
            }
        }

        // Verifica que se pueda guardar una sala válida
        public bool Guardar()
        {
            entidad = new Salas()
            {
                Numero = 101,
                Tipo = "Consulta",
                Area = 25.5m,
                Id_Sede = 1
            };

            salasAplicacion.Guardar(entidad);
            return entidad.Id > 0;
        }

        // Verifica que se pueda modificar una sala existente
        public bool Modificar()
        {
            entidad!.Tipo = "Quirúrgica";
            salasAplicacion.Modificar(entidad);
            return entidad.Tipo == "Quirúrgica";
        }

        // Verifica que se pueda listar salas (que la lista no esté vacía)
        public bool Listar()
        {
            var lista = salasAplicacion.Listar();
            return lista.Count > 0;
        }

        // Verifica que se pueda borrar la sala creada
        public bool Borrar()
        {
            salasAplicacion.Borrar(entidad!);
            return true;
        }
    }
}
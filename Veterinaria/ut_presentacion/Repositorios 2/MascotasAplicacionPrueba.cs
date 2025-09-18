using lib_dominio.Entidades;
using lib_repositorios.Implementaciones;
using lib_repositorios.Interfaces;
using ut_presentacion.Nucleo;

namespace ut_presentacion.Repositorios
{
    [TestClass]
    public class MascotasAplicacionPrueba
    {
        private readonly IConexion? iConexion;
        private IMascotasAplicacion? app;
        private List<Mascotas>? lista;
        private Mascotas? entidad;

        public MascotasAplicacionPrueba()
        {
            iConexion = new Conexion();
            iConexion.StringConexion = Configuracion.ObtenerValor("StringConexion");
            app = new MascotasAplicacion(iConexion);
        }

        [TestMethod]
        public void Ejecutar()
        {
            Assert.AreEqual(true, Guardar());                // Test guardar mascota válida
            Assert.AreEqual(true, Modificar());              // Test modificar mascota existente
            Assert.AreEqual(true, Listar());                 // Test listar mascotas
            Assert.AreEqual(true, BuscarPorPropietario());   // Test buscar por propietario
            Assert.AreEqual(true, BuscarPorEspecie());       // Test buscar por especie
            Assert.AreEqual(true, Borrar());                 // Test borrar mascota
        }

        public bool Listar()
        {
            this.lista = app!.Listar();
            return lista.Count > 0;
        }

        public bool Guardar()
        {
            this.entidad = new Mascotas()
            {
                Nombre = "Firulais",
                Especie = "Perro",  // Lógica de negocio: especie obligatoria
                Genero = "Macho",
                Peso = 12.5m,
                Esterilizado = true,
                Fecha_Adquisicion = DateTime.Now.AddMonths(-2),
                Id_Propietario = 1
            };

            var guardada = app!.Guardar(this.entidad);
            return guardada != null && guardada.Id > 0;
        }

        public bool Modificar()
        {
            if (this.entidad == null) return false;
            this.entidad.Peso = 14.2m; // Lógica de negocio: permitir actualizar el peso
            var modificada = app!.Modificar(this.entidad);
            return modificada != null && modificada.Peso == 14.2m;
        }

        public bool BuscarPorPropietario()
        {
            if (this.entidad == null) return false;
            var resultado = app!.BuscarPorPropietario(this.entidad.Id_Propietario);
            return resultado.Any();
        }

        public bool BuscarPorEspecie()
        {
            var resultado = app!.BuscarPorEspecie("Perro");
            return resultado.Any();
        }

        public bool Borrar()
        {
            if (this.entidad == null) return false;
            var borrada = app!.Borrar(this.entidad); // Lógica de negocio: borrar solo si existe
            return borrada != null;
        }
    }
}

using lib_dominio.Entidades;
using lib_repositorios.Implementaciones;
using lib_repositorios.Interfaces;
using Microsoft.EntityFrameworkCore;
using ut_presentacion.Nucleo;

namespace ut_presentacion.Repositorios
{
    [TestClass]
    public class Salas_InsumosAplicacionPrueba
    {
        private readonly IConexion? iConexion;
        private ISalas_InsumosAplicacion? app;
        private Salas_Insumos? entidad;

        public Salas_InsumosAplicacionPrueba()
        {
            iConexion = new Conexion();
            iConexion.StringConexion = Configuracion.ObtenerValor("StringConexion");
            app = new Salas_InsumosAplicacion(iConexion);
        }

        [TestMethod]
        public void Ejecutar()
        {
            Assert.AreEqual(true, Guardar());
            Assert.AreEqual(true, Modificar());
            Assert.AreEqual(true, Listar());
            Assert.AreEqual(true, BuscarPorSala());
            Assert.AreEqual(true, Borrar());
        }

        public bool Guardar()
        {
            // Asegurar que exista sala e insumo
            var sala = iConexion!.Salas!.FirstOrDefault() ?? EntidadesNucleo.Salas()!;
            if (sala.Id == 0) { iConexion.Salas!.Add(sala); iConexion.SaveChanges(); }

            var insumo = iConexion!.Insumos!.FirstOrDefault() ?? EntidadesNucleo.Insumos()!;
            if (insumo.Id == 0) { iConexion.Insumos!.Add(insumo); iConexion.SaveChanges(); }

            entidad = new Salas_Insumos
            {
                Id_Sala = sala.Id,
                Id_Insumo = insumo.Id
            };

            var guardado = app!.Guardar(entidad);
            return guardado != null && guardado.Id > 0;
        }

        public bool Modificar()
        {
            if (entidad == null) return false;
            // 🔹 Lógica de negocio simple: se podría cambiar la relación de insumo
            var insumo = iConexion!.Insumos!.Skip(1).FirstOrDefault();
            if (insumo != null) entidad.Id_Insumo = insumo.Id;

            var modificado = app!.Modificar(entidad);
            return modificado != null && modificado.Id_Insumo == entidad.Id_Insumo;
        }

        public bool Listar()
        {
            var lista = app!.Listar();
            return lista.Count > 0;
        }

        public bool BuscarPorSala()
        {
            if (entidad == null) return false;
            var resultado = app!.BuscarPorSala(entidad.Id_Sala);
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

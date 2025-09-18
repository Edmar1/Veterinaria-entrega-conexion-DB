using lib_dominio.Entidades;
using lib_repositorios.Implementaciones;
using lib_repositorios.Interfaces;
using Microsoft.EntityFrameworkCore;
using ut_presentacion.Nucleo;

namespace ut_presentacion.Repositorios
{
    [TestClass]
    public class Salas_MedicamentosAplicacionPrueba
    {
        private readonly IConexion? iConexion;
        private ISalas_MedicamentosAplicacion? app;
        private Salas_Medicamentos? entidad;

        public Salas_MedicamentosAplicacionPrueba()
        {
            iConexion = new Conexion();
            iConexion.StringConexion = Configuracion.ObtenerValor("StringConexion");
            app = new Salas_MedicamentosAplicacion(iConexion);
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
            // Asegurar sala y medicamento existentes
            var sala = iConexion!.Salas!.FirstOrDefault() ?? EntidadesNucleo.Salas()!;
            if (sala.Id == 0) { iConexion.Salas!.Add(sala); iConexion.SaveChanges(); }

            var medicamento = iConexion!.Medicamentos!.FirstOrDefault() ?? EntidadesNucleo.Medicamentos()!;
            if (medicamento.Id == 0) { iConexion.Medicamentos!.Add(medicamento); iConexion.SaveChanges(); }

            entidad = new Salas_Medicamentos
            {
                Id_Sala = sala.Id,
                Id_Medicamento = medicamento.Id
            };

            var guardado = app!.Guardar(entidad);
            return guardado != null && guardado.Id > 0;
        }

        public bool Modificar()
        {
            if (entidad == null) return false;
            // 🔹 Lógica de negocio: cambiar la relación de medicamento
            var medicamento = iConexion!.Medicamentos!.Skip(1).FirstOrDefault();
            if (medicamento != null) entidad.Id_Medicamento = medicamento.Id;

            var modificado = app!.Modificar(entidad);
            return modificado != null && modificado.Id_Medicamento == entidad.Id_Medicamento;
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

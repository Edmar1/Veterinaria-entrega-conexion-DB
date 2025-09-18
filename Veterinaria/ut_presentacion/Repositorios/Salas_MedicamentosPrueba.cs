using lib_dominio.Entidades;
using lib_repositorios.Implementaciones;
using lib_repositorios.Interfaces;
using Microsoft.EntityFrameworkCore;
using ut_presentacion.Nucleo;

namespace ut_presentacion.Repositorios
{
    [TestClass]
    public class Salas_MedicamentosPrueba
    {
        private readonly IConexion? iConexion;
        private List<Salas_Medicamentos>? lista;
        private Salas_Medicamentos? entidad;
        // Lista para rastrear medicamentos creados durante el test
        private List<int> medicamentosCreados = new List<int>();

        public Salas_MedicamentosPrueba()
        {
            iConexion = new Conexion();
            iConexion.StringConexion = Configuracion.ObtenerValor("StringConexion");
        }

        [TestMethod]
        public void Ejecutar()
        {
            Assert.AreEqual(true, Guardar());
            Assert.AreEqual(true, Modificar());
            Assert.AreEqual(true, Listar());
            Assert.AreEqual(true, Borrar());
        }

        public bool Listar()
        {
            lista = iConexion!.Salas_Medicamentos!.ToList();
            return lista.Count > 0;
        }

        public bool Guardar()
        {
            entidad = EntidadesNucleo.Salas_Medicamentos();
            iConexion.Salas_Medicamentos!.Add(entidad);
            iConexion.SaveChanges();
            return true;
        }

        public bool Modificar()
        {
            if (this.entidad == null) return false;

            // Crear un nuevo Medicamento válido
            var nuevoMed = EntidadesNucleo.Medicamentos();
            if (nuevoMed == null) return false;

            this.iConexion!.Medicamentos!.Add(nuevoMed);
            this.iConexion.SaveChanges(); // ahora nuevoMed.Id es válido

            // Rastrear el medicamento creado para limpieza posterior
            medicamentosCreados.Add(nuevoMed.Id);

            // Reasignar la FK
            this.entidad.Id_Medicamento = nuevoMed.Id;
            // Marcar solo la propiedad FK como modificada (evita sobrescribir todo)
            this.iConexion.Entry(this.entidad).Property(e => e.Id_Medicamento).IsModified = true;
            this.iConexion.SaveChanges();
            return true;
        }

        public bool Borrar()
        {
            // Borrar la relación Salas_Medicamentos
            this.iConexion!.Salas_Medicamentos!.Remove(this.entidad!);
            this.iConexion!.SaveChanges();

            // Limpiar medicamentos creados durante el test para evitar datos huérfanos
            foreach (var medicamentoId in medicamentosCreados)
            {
                var medicamento = this.iConexion.Medicamentos!.Find(medicamentoId);
                if (medicamento != null)
                {
                    this.iConexion.Medicamentos.Remove(medicamento);
                }
            }

            // Solo hacer SaveChanges si hay medicamentos que eliminar
            if (medicamentosCreados.Count > 0)
            {
                this.iConexion.SaveChanges();
                medicamentosCreados.Clear(); // Limpiar la lista de rastreo
            }

            return true;
        }
    }
}
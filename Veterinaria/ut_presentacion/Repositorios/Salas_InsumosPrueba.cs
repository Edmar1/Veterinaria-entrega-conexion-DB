using lib_dominio.Entidades;
using lib_repositorios.Implementaciones;
using lib_repositorios.Interfaces;
using Microsoft.EntityFrameworkCore;
using ut_presentacion.Nucleo;

namespace ut_presentacion.Repositorios
{
    [TestClass]
    public class Salas_InsumosPrueba
    {
        private readonly IConexion? iConexion;
        private List<Salas_Insumos>? lista;
        private Salas_Insumos? entidad;
        // Lista para rastrear insumos creados durante el test
        private List<int> insumosCreados = new List<int>();

        public Salas_InsumosPrueba()
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
            lista = iConexion!.Salas_Insumos!.ToList();
            return lista.Count > 0;
        }

        public bool Guardar()
        {
            entidad = EntidadesNucleo.Salas_Insumos();
            iConexion.Salas_Insumos!.Add(entidad);
            iConexion.SaveChanges();
            return true;
        }

        public bool Modificar()
        {
            if (entidad == null) return false;

            // Crear un nuevo Insumo válido para reasignar la FK (evita usar un Id inexistente)
            var nuevoInsumo = EntidadesNucleo.Insumos();
            if (nuevoInsumo == null) return false;

            iConexion!.Insumos!.Add(nuevoInsumo);
            iConexion.SaveChanges(); // ahora nuevoInsumo.Id es válido (PK generada)

            // Rastrear el insumo creado para limpieza posterior
            insumosCreados.Add(nuevoInsumo.Id);

            entidad.Id_Insumo = nuevoInsumo.Id;
            // Solo marcar la propiedad FK como modificada
            iConexion.Entry(entidad).Property(e => e.Id_Insumo).IsModified = true;
            iConexion.SaveChanges();
            return true;
        }

        public bool Borrar()
        {
            if (entidad == null) return false;

            // Borrar la relación Salas_Insumos
            iConexion!.Salas_Insumos!.Remove(entidad);
            iConexion.SaveChanges();

            // Limpiar insumos creados durante el test para evitar datos huérfanos
            foreach (var insumoId in insumosCreados)
            {
                var insumo = iConexion.Insumos!.Find(insumoId);
                if (insumo != null)
                {
                    iConexion.Insumos.Remove(insumo);
                }
            }

            // Solo hacer SaveChanges si hay insumos que eliminar
            if (insumosCreados.Count > 0)
            {
                iConexion.SaveChanges();
                insumosCreados.Clear(); // Limpiar la lista de rastreo
            }

            return true;
        }
    }
}
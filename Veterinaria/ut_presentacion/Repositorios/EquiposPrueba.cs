using lib_dominio.Entidades;
using lib_repositorios.Implementaciones;
using lib_repositorios.Interfaces;
using Microsoft.EntityFrameworkCore;
using ut_presentacion.Nucleo;

namespace ut_presentacion.Repositorios
{
    [TestClass]
    public class EquiposPrueba
    {
        private readonly IConexion? iConexion;
        private List<Equipos>? lista;
        private Equipos? entidad;
        // Lista para rastrear salas creadas durante el test
        private List<int> salasCreadas = new List<int>();

        public EquiposPrueba()
        {
            iConexion = new Conexion();
            iConexion.StringConexion = Configuracion.ObtenerValor("StringConexion");
        }

        [TestMethod]
        public void Ejecutar()
        {
            Assert.AreEqual(true, Guardar() > 0);
            Assert.AreEqual(true, Modificar());
            Assert.AreEqual(true, Listar());
            Assert.AreEqual(true, Borrar());
        }

        public bool Listar()
        {
            this.lista = this.iConexion!.Equipos!.ToList();
            return lista.Count > 0;
        }

        public int Guardar()
        {
            // 2. Crear una Sala (dependencia de Equipo)
            var sala = EntidadesNucleo.Salas();
            this.iConexion!.Salas!.Add(sala!);
            this.iConexion.SaveChanges();

            // Rastrear la sala creada para limpieza posterior
            salasCreadas.Add(sala.Id);

            // 3. Crear un equipo asociado a la sala recién creada
            this.entidad = EntidadesNucleo.Equipos();
            this.iConexion!.Equipos!.Add(this.entidad!);
            this.iConexion.SaveChanges();
            return this.entidad.Id;
        }

        public bool Modificar()
        {
            // Con el tracking activado, EF Core detecta los cambios automáticamente
            var entidadRecargada = this.iConexion!.Equipos!.Find(this.entidad!.Id);
            if (entidadRecargada == null) return false;
            entidadRecargada.Nombre = "Equipo Actualizado";
            this.iConexion.SaveChanges();
            var entidadVerificada = this.iConexion.Equipos.AsNoTracking().FirstOrDefault(e => e.Id == this.entidad.Id);
            return entidadVerificada?.Nombre == "Equipo Actualizado";
        }

        public bool Borrar()
        {
            // Borrar el equipo
            var entidadRecargada = this.iConexion!.Equipos!.Find(this.entidad!.Id);
            if (entidadRecargada == null) return false;
            this.iConexion.Equipos.Remove(entidadRecargada);
            this.iConexion.SaveChanges();

            // Limpiar salas creadas durante el test para evitar datos huérfanos
            foreach (var salaId in salasCreadas)
            {
                var sala = this.iConexion.Salas!.Find(salaId);
                if (sala != null)
                {
                    this.iConexion.Salas.Remove(sala);
                }
            }

            // Solo hacer SaveChanges si hay salas que eliminar
            if (salasCreadas.Count > 0)
            {
                this.iConexion.SaveChanges();
                salasCreadas.Clear(); // Limpiar la lista de rastreo
            }

            var existe = this.iConexion.Equipos.Any(e => e.Id == this.entidad.Id);
            return !existe;
        }
    }
}
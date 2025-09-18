using lib_dominio.Entidades;
using lib_repositorios.Implementaciones;
using lib_repositorios.Interfaces;
using Microsoft.EntityFrameworkCore;
using ut_presentacion.Nucleo;

namespace ut_presentacion.Repositorios
{
    [TestClass]
    public class Veterinarios_MascotasPrueba
    {
        private readonly IConexion? iConexion;
        private List<Veterinarios_Mascotas>? lista;
        private Veterinarios_Mascotas? entidad;

        public Veterinarios_MascotasPrueba()
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
            this.lista = this.iConexion!.Veterinarios_Mascotas!.ToList();
            return lista.Count > 0;
        }

        public bool Guardar()
        {
            
            entidad = EntidadesNucleo.Veterinarios_Mascotas();
            iConexion.Veterinarios_Mascotas!.Add(entidad);
            iConexion.SaveChanges();

            return entidad.Id > 0;
        }

        public bool Modificar()
        {
            if (this.entidad == null) return false;
            this.entidad.Fecha = this.entidad.Fecha.AddDays(1);
            var entry = this.iConexion!.Entry(this.entidad);
            entry.Property(e => e.Fecha).IsModified = true;
            this.iConexion.SaveChanges();
            return true;
        }

        public bool Borrar()
        {
            if (this.entidad == null) return false;
            this.iConexion!.Veterinarios_Mascotas!.Remove(this.entidad);
            this.iConexion.SaveChanges();
            return true;
        }
    }
}
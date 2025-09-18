using lib_dominio.Entidades;
using lib_repositorios.Implementaciones;
using lib_repositorios.Interfaces;
using Microsoft.EntityFrameworkCore;
using ut_presentacion.Nucleo;

namespace ut_presentacion.Repositorios
{
    [TestClass]
    public class Tel_PropietariosPrueba
    {
        private readonly IConexion? iConexion;
        private List<Tel_Propietarios>? lista;
        private Tel_Propietarios? entidad;

        public Tel_PropietariosPrueba()
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
            this.lista = this.iConexion!.Tel_Propietarios!.ToList();
            return lista.Count > 0;
        }

        public bool Guardar()
        {
            // 1. Asegurar que exista un propietario
            var propietario = this.iConexion!.Propietarios!.FirstOrDefault();
            if (propietario == null)
            {
                propietario = EntidadesNucleo.Propietarios();
                this.iConexion.Propietarios.Add(propietario);
                this.iConexion.SaveChanges();
            }

            // 2. Crear el teléfono asociado
            this.entidad = EntidadesNucleo.Tel_Propietarios();
            this.entidad.Id_Propietario = propietario.Id; // FK directa
                                                          // no hace falta setear la navegación, pero puedes hacerlo si quieres
                                                          // this.entidad.Propietario = propietario;

            this.iConexion.Tel_Propietarios!.Add(this.entidad);
            this.iConexion.SaveChanges();

            return true;
        }

        public bool Modificar()
        {
            // Telefono es int en el modelo; usar un valor que quepa en int
            this.entidad!.Telefono = "300111223";
            var entry = this.iConexion!.Entry<Tel_Propietarios>(this.entidad);
            entry.State = EntityState.Modified;
            this.iConexion!.SaveChanges();
            return true;
        }

        public bool Borrar()
        {
            this.iConexion!.Tel_Propietarios!.Remove(this.entidad!);
            this.iConexion!.SaveChanges();
            return true;
        }
    }
}
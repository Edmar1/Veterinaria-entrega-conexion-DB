using lib_dominio.Entidades;
using lib_repositorios.Implementaciones;
using lib_repositorios.Interfaces;
using Microsoft.EntityFrameworkCore;
using ut_presentacion.Nucleo;

namespace ut_presentacion.Repositorios
{
    [TestClass]
    public class Veterinarios_MascotasAplicacionPrueba
    {
        private readonly IConexion? iConexion;
        private IVeterinarios_MascotasAplicacion? app;
        private Veterinarios_Mascotas? entidad;

        public Veterinarios_MascotasAplicacionPrueba()
        {
            iConexion = new Conexion();
            iConexion.StringConexion = Configuracion.ObtenerValor("StringConexion");
            app = new Veterinarios_MascotasAplicacion(iConexion);
        }

        [TestMethod]
        public void Ejecutar()
        {
            Assert.AreEqual(true, Guardar());
            Assert.AreEqual(true, Modificar());
            Assert.AreEqual(true, Listar());
            Assert.AreEqual(true, BuscarPorVeterinario());
            Assert.AreEqual(true, Borrar());
        }

        public bool Guardar()
        {
            // 🔹 Lógica de negocio: asegurar que existan un veterinario y una mascota
            var veterinario = iConexion!.Veterinarios!.FirstOrDefault() ?? EntidadesNucleo.Veterinarios()!;
            if (veterinario.Id == 0) { iConexion.Veterinarios!.Add(veterinario); iConexion.SaveChanges(); }

            var mascota = iConexion!.Mascotas!.FirstOrDefault() ?? EntidadesNucleo.Mascotas()!;
            if (mascota.Id == 0) { iConexion.Mascotas!.Add(mascota); iConexion.SaveChanges(); }

            entidad = new Veterinarios_Mascotas
            {
                Fecha = DateTime.Now,
                Id_Veterinario = veterinario.Id,
                Id_Mascota = mascota.Id
            };

            var guardado = app!.Guardar(entidad);
            return guardado != null && guardado.Id > 0;
        }

        public bool Modificar()
        {
            if (entidad == null) return false;
            // 🔹 Lógica de negocio: actualizar la fecha de atención
            entidad.Fecha = DateTime.Now.AddDays(1);
            var modificado = app!.Modificar(entidad);
            return modificado != null && modificado.Fecha == entidad.Fecha;
        }

        public bool Listar()
        {
            var lista = app!.Listar();
            return lista.Count > 0;
        }

        public bool BuscarPorVeterinario()
        {
            if (entidad == null) return false;
            var resultado = app!.BuscarPorVeterinario(entidad.Id_Veterinario);
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

using lib_dominio.Entidades;
using lib_repositorios.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace lib_repositorios.Implementaciones
{
    public class InsumosAplicacion : IInsumosAplicacion
    {
        private readonly IConexion? IConexion;

        public InsumosAplicacion(IConexion iConexion)
        {
            this.IConexion = iConexion;
        }

        public void Configurar(string StringConexion)
        {
            this.IConexion!.StringConexion = StringConexion;
        }

        public Insumos? Guardar(Insumos? entidad)
        {
            // Lógica de negocio: validar que el insumo tenga nombre y precio mayor a 0
            if (entidad == null || string.IsNullOrWhiteSpace(entidad.Nombre) || entidad.Precio_Unidad <= 0)
                throw new Exception("lbInsumoInvalido");

            if (entidad.Id != 0)
                throw new Exception("lbYaSeGuardo");

            this.IConexion!.Insumos!.Add(entidad);
            this.IConexion.SaveChanges();
            return entidad;
        }

        public Insumos? Modificar(Insumos? entidad)
        {
            if (entidad == null)
                throw new Exception("lbFaltaInformacion");
            if (entidad.Id == 0)
                throw new Exception("lbNoSeGuardo");

            var entry = this.IConexion!.Entry(entidad);
            entry.State = EntityState.Modified;
            this.IConexion.SaveChanges();
            return entidad;
        }

        public Insumos? Borrar(Insumos? entidad)
        {
            if (entidad == null)
                throw new Exception("lbFaltaInformacion");
            if (entidad.Id == 0)
                throw new Exception("lbNoSeGuardo");

            this.IConexion!.Insumos!.Remove(entidad);
            this.IConexion.SaveChanges();
            return entidad;
        }

        public List<Insumos> Listar()
        {
            return this.IConexion!.Insumos!.Take(20).ToList();
        }
    }
}
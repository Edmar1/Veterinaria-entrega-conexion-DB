using lib_aplicacion.Interfaces;
using lib_dominio.Entidades;
using lib_repositorios.Interfaces;

namespace lib_aplicacion.Implementaciones
{
    public class MedicamentosAplicacion : IMedicamentosAplicacion
    {
        private readonly IConexion _conexion;

        public MedicamentosAplicacion(IConexion conexion)
        {
            _conexion = conexion;
        }

        public Medicamentos Guardar(Medicamentos entidad)
        {
            // LÓGICA DE NEGOCIO:
            // No permitir guardar medicamentos con precio menor o igual a 0
            if (entidad.Precio_Unidad <= 0)
                throw new Exception("El precio por unidad debe ser mayor que 0.");

            // LÓGICA DE NEGOCIO:
            // Si requiere refrigeración, debe especificar vía de administración distinta de vacío
            if (entidad.Refrigeracion && string.IsNullOrWhiteSpace(entidad.Via_Administracion))
                throw new Exception("Si el medicamento requiere refrigeración, debe tener vía de administración.");

            _conexion.Medicamentos!.Add(entidad);
            _conexion.SaveChanges();
            return entidad;
        }

        public Medicamentos Modificar(Medicamentos entidad)
        {
            var existente = _conexion.Medicamentos!.Find(entidad.Id);
            if (existente == null) throw new Exception("El medicamento no existe.");

            // LÓGICA DE NEGOCIO:
            // No permitir modificar un medicamento a precio 0
            if (entidad.Precio_Unidad <= 0)
                throw new Exception("El precio por unidad debe ser mayor que 0.");

            existente.Nombre = entidad.Nombre;
            existente.Proveedor = entidad.Proveedor;
            existente.Precio_Unidad = entidad.Precio_Unidad;
            existente.Via_Administracion = entidad.Via_Administracion;
            existente.Refrigeracion = entidad.Refrigeracion;

            _conexion.SaveChanges();
            return existente;
        }

        public bool Borrar(int id)
        {
            var entidad = _conexion.Medicamentos!.Find(id);
            if (entidad == null) return false;

            _conexion.Medicamentos.Remove(entidad);
            _conexion.SaveChanges();
            return true;
        }

        public List<Medicamentos> Listar()
        {
            return _conexion.Medicamentos!.ToList();
        }
    }
}
using lib_dominio.Entidades;
using lib_repositorios.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace lib_repositorios.Implementaciones
{
    public class PagosAplicacion : IPagosAplicacion
    {
        private IConexion? IConexion = null;

        public PagosAplicacion(IConexion iConexion)
        {
            this.IConexion = iConexion;
        }

        public void Configurar(string StringConexion)
        {
            this.IConexion!.StringConexion = StringConexion;
        }

        public Pagos? Guardar(Pagos? entidad)
        {
            if (entidad == null)                           // Lógica de negocio:
                throw new Exception("lbFaltaInformacion");
            // -> No se puede guardar un pago sin información.

            if (entidad.Id != 0)                           // Lógica de negocio:
                throw new Exception("lbYaSeGuardo");
            // -> Un pago nuevo debe tener Id = 0.

            if (entidad.Valor <= 0)                        // Lógica de negocio:
                throw new Exception("lbValorInvalido");
            // -> No se puede registrar un pago con valor cero o negativo.

            if (string.IsNullOrWhiteSpace(entidad.Estado)) // Lógica de negocio:
                throw new Exception("lbEstadoObligatorio");
            // -> El estado del pago es obligatorio.

            this.IConexion!.Pagos!.Add(entidad);
            this.IConexion.SaveChanges();
            return entidad;
        }

        public Pagos? Modificar(Pagos? entidad)
        {
            if (entidad == null)                           // Lógica de negocio:
                throw new Exception("lbFaltaInformacion");
            // -> No se puede modificar un pago vacío.

            if (entidad.Id == 0)                           // Lógica de negocio:
                throw new Exception("lbNoSeGuardo");
            // -> Solo se puede modificar un pago que ya exista.

            var entry = this.IConexion!.Entry<Pagos>(entidad);
            entry.State = EntityState.Modified;
            this.IConexion.SaveChanges();
            return entidad;
        }

        public Pagos? Borrar(Pagos? entidad)
        {
            if (entidad == null)                           // Lógica de negocio:
                throw new Exception("lbFaltaInformacion");
            // -> No se puede borrar un pago inexistente.

            if (entidad.Id == 0)                           // Lógica de negocio:
                throw new Exception("lbNoSeGuardo");
            // -> Solo se puede borrar un pago que ya fue guardado.

            this.IConexion!.Pagos!.Remove(entidad);
            this.IConexion.SaveChanges();
            return entidad;
        }

        public List<Pagos> Listar()
        {
            return this.IConexion!.Pagos!.Take(20).ToList();
        }

        public List<Pagos> BuscarPorEstado(string estado)
        {
            return this.IConexion!.Pagos!
                .Where(x => x.Estado == estado)
                .ToList();
        }
    }
}

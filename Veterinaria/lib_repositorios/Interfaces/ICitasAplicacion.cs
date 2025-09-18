using lib_dominio.Entidades;

namespace lib_repositorios.Interfaces
{
    public interface ICitasAplicacion
    {
        void Configurar(string StringConexion);
        Citas? Guardar(Citas? entidad);
        Citas? Modificar(Citas? entidad);
        Citas? Borrar(Citas? entidad);
        List<Citas> Listar();
        List<Citas> BuscarPorFecha(DateTime fecha);
    }
}
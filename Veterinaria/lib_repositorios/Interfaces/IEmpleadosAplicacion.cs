using lib_dominio.Entidades;

namespace lib_repositorios.Interfaces
{
    public interface IEmpleadosAplicacion
    {
        void Configurar(string StringConexion);
        Empleados? Guardar(Empleados? entidad);
        Empleados? Modificar(Empleados? entidad);
        Empleados? Borrar(Empleados? entidad);
        List<Empleados> Listar();
        List<Empleados> BuscarPorCargo(string cargo);
    }
}
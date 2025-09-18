using lib_dominio.Entidades;

namespace lib_aplicaciones.Interfaces
{
    public interface IPropietariosAplicacion
    {
        bool Guardar(Propietarios entidad);
        bool Modificar(Propietarios entidad);
        bool Borrar(Propietarios entidad);
        List<Propietarios> Listar();
    }
}

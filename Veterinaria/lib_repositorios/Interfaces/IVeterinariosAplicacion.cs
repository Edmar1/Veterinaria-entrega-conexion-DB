using lib_dominio.Entidades;

namespace lib_aplicaciones.Interfaces
{
    public interface IVeterinariosAplicacion
    {
        bool Guardar(Veterinarios entidad);
        bool Modificar(Veterinarios entidad);
        bool Borrar(Veterinarios entidad);
        List<Veterinarios> Listar();
    }
}
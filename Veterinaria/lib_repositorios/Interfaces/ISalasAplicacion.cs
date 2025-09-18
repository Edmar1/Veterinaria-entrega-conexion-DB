using lib_dominio.Entidades;

namespace lib_repositorios.Interfaces
{
    public interface ISalasAplicacion
    {
        void Configurar(string StringConexion);
        Salas? Guardar(Salas? entidad);
        Salas? Modificar(Salas? entidad);
        Salas? Borrar(Salas? entidad);
        List<Salas> Listar();

     
    }
}
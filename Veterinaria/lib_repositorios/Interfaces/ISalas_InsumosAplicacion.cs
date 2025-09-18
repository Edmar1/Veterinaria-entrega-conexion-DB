using lib_dominio.Entidades;

namespace lib_repositorios.Interfaces
{
    public interface ISalas_InsumosAplicacion
    {
        void Configurar(string StringConexion);
        Salas_Insumos? Guardar(Salas_Insumos? entidad);
        Salas_Insumos? Modificar(Salas_Insumos? entidad);
        Salas_Insumos? Borrar(Salas_Insumos? entidad);
        List<Salas_Insumos> Listar();
        List<Salas_Insumos> BuscarPorSala(int idSala);
    }
}

using lib_dominio.Entidades;

namespace lib_repositorios.Interfaces
{
    public interface IInsumosAplicacion
    {
        void Configurar(string StringConexion);
        Insumos? Guardar(Insumos? entidad);
        Insumos? Modificar(Insumos? entidad);
        Insumos? Borrar(Insumos? entidad);
        List<Insumos> Listar();
    }
}
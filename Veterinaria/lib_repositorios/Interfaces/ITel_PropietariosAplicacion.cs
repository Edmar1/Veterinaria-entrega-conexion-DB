using lib_dominio.Entidades;

namespace lib_repositorios.Interfaces
{
    public interface ITel_PropietariosAplicacion
    {
        void Configurar(string StringConexion);
        Tel_Propietarios? Guardar(Tel_Propietarios? entidad);
        Tel_Propietarios? Modificar(Tel_Propietarios? entidad);
        Tel_Propietarios? Borrar(Tel_Propietarios? entidad);
        List<Tel_Propietarios> Listar();

        // Nueva lógica de negocio: buscar teléfonos por número parcial
        List<Tel_Propietarios> BuscarPorNumero(string numero);
    }
}
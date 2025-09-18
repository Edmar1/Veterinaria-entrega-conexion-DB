using lib_dominio.Entidades;

namespace lib_repositorios.Interfaces
{
    public interface ISedesAplicacion
    {
        void Configurar(string StringConexion);
        Sedes? Guardar(Sedes? entidad);
        Sedes? Modificar(Sedes? entidad);
        Sedes? Borrar(Sedes? entidad);
        List<Sedes> Listar(); 

        // Nuevo método para buscar por teléfono
        List<Sedes> BuscarPorTelefono(string telefono);
    }
} 

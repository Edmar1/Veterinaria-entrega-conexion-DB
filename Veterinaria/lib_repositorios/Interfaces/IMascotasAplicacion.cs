using lib_dominio.Entidades;

namespace lib_repositorios.Interfaces
{
    public interface IMascotasAplicacion
    {
        void Configurar(string StringConexion);
        Mascotas? Guardar(Mascotas? entidad);
        Mascotas? Modificar(Mascotas? entidad);
        Mascotas? Borrar(Mascotas? entidad);
        List<Mascotas> Listar();

        // Lógica de negocio: buscar mascotas por propietario
        List<Mascotas> BuscarPorPropietario(int propietarioId);

        // Lógica de negocio: buscar mascotas por especie
        List<Mascotas> BuscarPorEspecie(string especie);
    }
}
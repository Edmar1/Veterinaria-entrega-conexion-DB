using lib_dominio.Entidades;

namespace lib_repositorios.Interfaces
{
    public interface IVeterinarios_MascotasAplicacion
    {
        void Configurar(string StringConexion);
        Veterinarios_Mascotas? Guardar(Veterinarios_Mascotas? entidad);
        Veterinarios_Mascotas? Modificar(Veterinarios_Mascotas? entidad);
        Veterinarios_Mascotas? Borrar(Veterinarios_Mascotas? entidad);
        List<Veterinarios_Mascotas> Listar();
        List<Veterinarios_Mascotas> BuscarPorVeterinario(int idVeterinario);
    }
}


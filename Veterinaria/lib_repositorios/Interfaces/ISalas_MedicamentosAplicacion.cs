using lib_dominio.Entidades;

namespace lib_repositorios.Interfaces
{
    public interface ISalas_MedicamentosAplicacion
    {
        void Configurar(string StringConexion);
        Salas_Medicamentos? Guardar(Salas_Medicamentos? entidad);
        Salas_Medicamentos? Modificar(Salas_Medicamentos? entidad);
        Salas_Medicamentos? Borrar(Salas_Medicamentos? entidad);
        List<Salas_Medicamentos> Listar();
        List<Salas_Medicamentos> BuscarPorSala(int idSala);
    }
}
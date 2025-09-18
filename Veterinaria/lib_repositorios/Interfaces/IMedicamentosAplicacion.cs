using lib_dominio.Entidades;

namespace lib_aplicacion.Interfaces
{
    public interface IMedicamentosAplicacion
    {
        Medicamentos Guardar(Medicamentos entidad);
        Medicamentos Modificar(Medicamentos entidad);
        bool Borrar(int id);
        List<Medicamentos> Listar();
    }
}
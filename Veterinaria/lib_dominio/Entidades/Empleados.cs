using System.ComponentModel.DataAnnotations.Schema;

namespace lib_dominio.Entidades
{
    public class Empleados
    {
        public int Id { get; set; }
        public string Documento { get; set; } = null!;
        public string Nombre { get; set; } = null!;
        public string Cargo { get; set; } = null!;
        public DateTime Fecha_Ingreso { get; set; }
        public string Horario { get; set; } = null!;
        public string Telefono { get; set; } = null!;
        public int Id_Sede { get; set; }
        [ForeignKey(nameof(Id_Sede))]
        public Sedes Sede { get; set; } = null!;                  // Un empleado pertenece a una sede (N:1)

    }
}

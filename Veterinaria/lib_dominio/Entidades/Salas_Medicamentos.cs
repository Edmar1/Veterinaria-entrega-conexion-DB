using System.ComponentModel.DataAnnotations.Schema;

namespace lib_dominio.Entidades
{
    public class Salas_Medicamentos
    {
        public int Id { get; set; }
        public int Id_Sala { get; set; }
        public int Id_Medicamento { get; set; }

        [ForeignKey(nameof(Id_Sala))]
        public Salas Sala { get; set; } = null!;                   // Relación entre Sala y Medicamento (N:M)

        [ForeignKey(nameof(Id_Medicamento))]
        public Medicamentos Medicamento { get; set; } = null!;     // Relación entre Medicamento y Sala (N:M)
    }
}
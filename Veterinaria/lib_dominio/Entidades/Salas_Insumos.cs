using System.ComponentModel.DataAnnotations.Schema;

namespace lib_dominio.Entidades
{
    public class Salas_Insumos
    {
        public int Id { get; set; }

        public int Id_Sala { get; set; }
        public int Id_Insumo { get; set; }

        // El ForeignKey debe ir sobre la propiedad de navegación, no sobre el campo entero.
        [ForeignKey(nameof(Id_Sala))]
        public Salas Sala { get; set; } = null!;            // Relación entre Sala e Insumo (N:M)

        [ForeignKey(nameof(Id_Insumo))]
        public Insumos Insumo { get; set; } = null!;        // Relación entre Insumo y Sala (N:M)
    }
}

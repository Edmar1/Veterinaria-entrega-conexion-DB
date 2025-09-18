using System.ComponentModel.DataAnnotations.Schema;
using lib_dominio.Entidades;

namespace lib_dominio.Entidades
{
    public class Citas
    {
        public int Id { get; set; }
        public string Motivo { get; set; } = null!;
        public DateTime Fecha { get; set; }
        public string Hora { get; set; } = null!;
        public string Sede { get; set; } = null!;

        public int Id_Propietario { get; set; }
        [ForeignKey(nameof(Id_Propietario))]
        public Propietarios Propietario { get; set; } = null!;
    }
}

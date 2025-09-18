using System.ComponentModel.DataAnnotations.Schema;

namespace lib_dominio.Entidades
{
    public class Tel_Propietarios
    {
        public int Id { get; set; }
        public string Telefono { get; set; } = null!;


        public int Id_Propietario { get; set; }

        [ForeignKey(nameof(Id_Propietario))]
        public Propietarios Propietario { get; set; } = null!;
    }
}

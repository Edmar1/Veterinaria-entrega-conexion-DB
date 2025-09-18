using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace lib_dominio.Entidades
{
    public class Sedes
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string Nombre { get; set; } = null!;
        public string Direccion { get; set; } = null!;
        public string Barrio { get; set; } = null!;
        public bool? Parqueadero { get; set; }
        public string Telefono { get; set; } = null!;

        public List<Salas> Salas { get; set; } = new();
        public List<Empleados> Empleados { get; set; } = new();
        public List<Veterinarios> Veterinarios { get; set; } = new();
    }
}

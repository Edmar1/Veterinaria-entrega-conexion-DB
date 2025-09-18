using System.ComponentModel.DataAnnotations.Schema;

namespace lib_dominio.Entidades
{
    public class Veterinarios_Mascotas
    {
        public int Id { get; set; }
        public DateTime Fecha { get; set; }

        public int Id_Veterinario { get; set; }
        [ForeignKey(nameof(Id_Veterinario))]
        public Veterinarios Veterinario { get; set; } = null!;  // FK a Veterinarios

        public int Id_Mascota { get; set; }
        [ForeignKey(nameof(Id_Mascota))]
        public Mascotas Mascota { get; set; } = null!;          // FK a Mascotas
    }
}

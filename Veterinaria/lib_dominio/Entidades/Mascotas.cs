using System.ComponentModel.DataAnnotations.Schema;

namespace lib_dominio.Entidades
{
    public class Mascotas
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = null!;
        public string Especie { get; set; } = null!;
        public string? Genero { get; set; }
        public decimal Peso { get; set; }
        public bool? Esterilizado { get; set; }
        public DateTime? Fecha_Adquisicion { get; set; }  
        public DateTime Fecha_Registro { get; set; }      
        public int Id_Propietario { get; set; }
        [ForeignKey(nameof(Id_Propietario))]

        public Propietarios Propietario { get; set; } = null!;      // Una mascota pertenece a un propietario (N:1)
        public List<Veterinarios_Mascotas> Veterinarios_Mascotas { get; set; } = new(); // Una mascota puede ser atendida por muchos veterinarios (N:M)
    }
}

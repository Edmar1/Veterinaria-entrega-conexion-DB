using System.ComponentModel.DataAnnotations.Schema;

namespace lib_dominio.Entidades
{
    public class Veterinarios
    {
        public int Id { get; set; }
        public string Documento { get; set; } = null!;
        public string Nombre { get; set; } = null!;
        public int Edad { get; set; }
        public string? Especialidad { get; set; }
        public DateTime Fecha_Ingreso { get; set; }
        public string Horario { get; set; } = null!;
        public string Telefono { get; set; } = null!;

        
        public int Id_Sede { get; set; }   // ← Esta es la FK real
        [ForeignKey(nameof(Id_Sede))]
        public Sedes Sede { get; set; } = null!;

        public List<Veterinarios_Mascotas> Veterinarios_Mascotas { get; set; } = new();
    }
}

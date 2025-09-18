using System.ComponentModel.DataAnnotations.Schema;

namespace lib_dominio.Entidades
{
    public class Equipos
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = null!;
        public string Marca { get; set; } = null!;
        public string Modelo { get; set; } = null!;
        public DateTime? Fecha_Fabricacion { get; set; }
        public DateTime Fecha_Adquisicion { get; set; }
        public DateTime Ultima_Revision { get; set; }

     
      
        public int Id_Sala { get; set; }
        [ForeignKey(nameof(Id_Sala))]
        public Salas Sala { get; set; } = null!;
    }
}

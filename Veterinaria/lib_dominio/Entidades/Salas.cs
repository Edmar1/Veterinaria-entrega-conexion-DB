using System.ComponentModel.DataAnnotations.Schema;

namespace lib_dominio.Entidades
{
    public class Salas
    {
        public int Id { get; set; }
        public int Numero { get; set; }
        public string Tipo { get; set; } = null!;
        public decimal? Area { get; set; }

        
        public int Id_Sede { get; set; }
        
        [ForeignKey(nameof(Id_Sede))]
        public Sedes? Sede { get; set; } = null!;                  // Una sala pertenece a una sede (N:1)
        public List<Equipos> Equipos { get; set; } = new();       // Una sala puede tener muchos equipos (1:N)
        public List<Salas_Insumos> SalasInsumos { get; set; } = new(); // Una sala puede tener muchos insumos (N:M)
        public List<Salas_Medicamentos> SalasMedicamentos { get; set; } = new(); // Una sala puede tener muchos medicamentos (N:M)
    }
}
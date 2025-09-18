using System.ComponentModel.DataAnnotations.Schema;

namespace lib_dominio.Entidades
{
    public class Pagos
    {
        public int Id { get; set; }
        public DateTime Fecha { get; set; }
        public string? Metodo { get; set; }
        public int Valor { get; set; }
        public string Estado { get; set; } = null!;

        public int Id_Propietario { get; set; }
        [ForeignKey(nameof(Id_Propietario))]

        public Propietarios Propietario { get; set; } = null!;      // Un pago pertenece a un propietario (N:1)
    }
}
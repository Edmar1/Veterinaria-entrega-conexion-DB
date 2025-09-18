namespace lib_dominio.Entidades
{
    public class Medicamentos
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = null!;
        public string Proveedor { get; set; } = null!;
        public int Precio_Unidad { get; set; }           
        public string Via_Administracion { get; set; } = null!; 
        public bool Refrigeracion { get; set; }

        public List<Salas_Medicamentos> SalasMedicamentos { get; set; } = new(); // Un medicamento puede estar en muchas salas (N:M)
    }
}

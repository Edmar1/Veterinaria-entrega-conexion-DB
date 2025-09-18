namespace lib_dominio.Entidades
{
    public class Insumos
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = null!;
        public string Proveedor { get; set; } = null!;
        public int Precio_Unidad { get; set; }
        public bool Toxico { get; set; }

        public List<Salas_Insumos> SalasInsumos { get; set; } = new(); // Un insumo puede estar en muchas salas (N:M)
    }
}

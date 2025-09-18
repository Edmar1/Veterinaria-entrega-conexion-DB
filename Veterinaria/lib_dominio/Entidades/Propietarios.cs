namespace lib_dominio.Entidades
{
    public class Propietarios
    {
        public int Id { get; set; }
        public string Documento { get; set; } = null!;
        public string Nombre { get; set; } = null!;
        public int Edad { get; set; }
        public string Direccion { get; set; } = null!;
        public string Correo { get; set; } = null!;
        public string Genero { get; set; } = null!;
        public int Estrato { get; set; }
        public DateTime Fecha_Registro { get; set; }

        public List<Tel_Propietarios> Tel_Propietarios { get; set; } = new(); // Un propietario puede tener muchos teléfonos (1:N)
        public List<Mascotas> Mascotas { get; set; } = new();               // Un propietario puede tener muchas mascotas (1:N)
        public List<Citas> Citas { get; set; } = new();                     // Un propietario puede tener muchas citas (1:N)
        public List<Pagos> Pagos { get; set; } = new();                     // Un propietario puede tener muchos pagos (1:N)
    }
}

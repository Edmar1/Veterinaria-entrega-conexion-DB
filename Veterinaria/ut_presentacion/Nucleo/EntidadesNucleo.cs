using lib_dominio.Entidades;

namespace ut_presentacion.Nucleo
{
    public class EntidadesNucleo
    {
        public static Sedes? Sedes()
        {
            var entidad = new Sedes();
            entidad.Nombre = "Pruebas-" + DateTime.Now.ToString("yyyyMMddhhmmssfff");
            entidad.Direccion = "Pruebas-";
            entidad.Barrio = "Prueba";
            entidad.Parqueadero = true;
            entidad.Telefono = "1231232";
            return entidad;
        }
        // MODIFICADO: Ahora requiere el Id de la Sede
        public static Salas? Salas()
        {
            var entidad = new Salas();
            entidad.Numero = new Random().Next(1, 50);
            entidad.Tipo = "Consulta";
            entidad.Area = 25.5m;
            entidad.Id_Sede = 1; // Se asigna el Id recibido
            return entidad;
        }

        public static Equipos? Equipos()
        {
            var entidad = new Equipos();
            entidad.Nombre = "Rayos X";
            entidad.Marca = "Siemens";
            entidad.Modelo = "RX-2025";
            entidad.Fecha_Fabricacion = DateTime.Now.AddYears(-5);
            entidad.Fecha_Adquisicion = DateTime.Now.AddYears(-1);
            entidad.Ultima_Revision = DateTime.Now;
            entidad.Id_Sala = 1;
            return entidad;
        }

        public static Insumos? Insumos()
        {
            var entidad = new Insumos();
            entidad.Nombre = "Guantes";
            entidad.Proveedor = "Proveedor-1";
            entidad.Precio_Unidad = 500;
            entidad.Toxico = false;
            return entidad;
        }

        public static Medicamentos? Medicamentos()
        {
            var entidad = new Medicamentos();
            entidad.Nombre = "Amoxicilina";
            entidad.Proveedor = "Proveedor-2";
            entidad.Precio_Unidad = 2000;
            entidad.Via_Administracion = "Oral";
            entidad.Refrigeracion = false;
            return entidad;
        }

        public static Empleados? Empleados()
        {
            var entidad = new Empleados();
            entidad.Documento = "12312323231";
            entidad.Nombre = "Empleado-" + DateTime.Now.ToString("yyyyMMddhhmmssfff");
            entidad.Cargo = "Recepcionista";
            entidad.Fecha_Ingreso = DateTime.Now.AddYears(-1);
            entidad.Horario = "Lunes a Viernes 8am - 5pm";
            entidad.Telefono = "2312312";
            entidad.Id_Sede = 1;
            return entidad;
        }

        public static Veterinarios? Veterinarios()
        {
            var entidad = new Veterinarios();
            entidad.Documento = "3124242131";
            entidad.Nombre = "Veterinario-" + DateTime.Now.ToString("yyyyMMddhhmmssfff");
            entidad.Edad = 35;
            entidad.Especialidad = "Cirugía";
            entidad.Fecha_Ingreso = DateTime.Now.AddYears(-3);
            entidad.Horario = "Turnos rotativos";
            entidad.Id_Sede = 1;
            entidad.Telefono = "23110";

            return entidad;
        }

        public static Propietarios? Propietarios()
        {
            var entidad = new Propietarios();
            entidad.Documento = "123124142412";
            entidad.Nombre = "Propietario-" + DateTime.Now.ToString("yyyyMMddhhmmssfff");
            entidad.Edad = 30;
            entidad.Direccion = "Calle Falsa #123";
            entidad.Correo = "propietario" + DateTime.Now.ToString("yyyyMMddhhmmssfff") + "@mail.com";
            entidad.Genero = "M";
            entidad.Estrato = 3;
            entidad.Fecha_Registro = DateTime.Now;
            return entidad;
        }

        public static Tel_Propietarios? Tel_Propietarios()
        {
            var entidad = new Tel_Propietarios
            {
                Telefono = "320520356" // valor de prueba
            };
            return entidad;
        }

        public static Mascotas? Mascotas()
        {
            var entidad = new Mascotas();
            entidad.Nombre = "Firulais";
            entidad.Especie = "Perro";
            entidad.Genero = "M";
            entidad.Peso = 25.3m;
            entidad.Esterilizado = true;
            entidad.Fecha_Adquisicion = DateTime.Now.AddYears(-2);
            entidad.Fecha_Registro = DateTime.Now;
            entidad.Id_Propietario = 1; // <<-- Se asigna el ID aquí
            return entidad;
        }

        public static Citas? Citas()
        {
            var entidad = new Citas();
            entidad.Motivo = "Chequeo General";
            entidad.Fecha = DateTime.Now.Date.AddDays(1);
            entidad.Hora = "10:00 AM";
            entidad.Sede = "Sede Central";     
            entidad.Id_Propietario = 1;

            return entidad;
        }
        // ...
        // MODIFICADO: El parámetro se llamaba idpagos, ahora es propietarioId para evitar confusión
        public static Pagos? Pagos()
        {
            var entidad = new Pagos();
            entidad.Fecha = DateTime.Now;
            entidad.Metodo = "Tarjeta";
            entidad.Valor = 50000;
            entidad.Estado = "Pagado";
            entidad.Id_Propietario = 1; // <<-- Se asigna el ID aquí
            return entidad;
        }

        public static Salas_Insumos? Salas_Insumos()
        {
            var entidad = new Salas_Insumos();

            entidad.Id_Sala = 1;
            entidad.Id_Insumo = 1;
            
            return entidad;
        }

        public static Salas_Medicamentos? Salas_Medicamentos()
        {
            var entidad = new Salas_Medicamentos();

           entidad.Id_Sala = 1;
           entidad.Id_Medicamento = 1;
            
            return entidad;
        }



       public static Veterinarios_Mascotas? Veterinarios_Mascotas()
        {
            var entidad = new Veterinarios_Mascotas();

            entidad.Fecha = DateTime.Now;
            entidad.Id_Veterinario = 1;
            entidad.Id_Mascota = 1;
            

            return entidad;
        }
    }
}
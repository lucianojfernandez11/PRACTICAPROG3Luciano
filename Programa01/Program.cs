using System;

namespace ProgramGeneral
{
    class Program
    {
        static void Main(string[] args)
        {
         
            Console.WriteLine("\n--- REGISTRO DE ALUMNO ---");

            Console.Write("Ingresa el nombre del alumno: ");
            string nombreAlumno = Console.ReadLine();

            Console.Write("Ingresa la edad del alumno: ");
            int edadAlumno = int.Parse(Console.ReadLine());

            Console.Write("Ingresa la nota final: ");
            double nota = double.Parse(Console.ReadLine());

            Console.WriteLine("\nNombre: " + nombreAlumno);
            Console.WriteLine("Edad el próximo año: " + (edadAlumno + 1));
            Console.WriteLine("Nota final: " + nota);


            Console.WriteLine("\n--- REGISTRO DE VEHÍCULO ---");

            Console.Write("Ingresa la marca del vehículo: ");
            string marca = Console.ReadLine();

            Console.Write("Ingresa el año del vehículo: ");
            int anio = int.Parse(Console.ReadLine());

            Console.Write("Ingresa el precio del vehículo: ");
            double precioVehiculo = double.Parse(Console.ReadLine());

            Console.WriteLine("\nMarca: " + marca);
            Console.WriteLine("Año siguiente: " + (anio + 1));
            Console.WriteLine("Precio: $" + precioVehiculo);


            Console.WriteLine("\n--- REGISTRO DE EMPLEADO ---");

            Console.Write("Ingresa el nombre del empleado: ");
            string nombreEmpleado = Console.ReadLine();

            Console.Write("Ingresa las horas trabajadas: ");
            int horas = int.Parse(Console.ReadLine());

            Console.Write("Ingresa el pago por hora: ");
            double pagoHora = double.Parse(Console.ReadLine());

            double sueldo = horas * pagoHora;

            Console.WriteLine("\nEmpleado: " + nombreEmpleado);
            Console.WriteLine("Horas trabajadas: " + horas);
            Console.WriteLine("Sueldo total: $" + sueldo);


    
            Console.WriteLine("\n--- REGISTRO DE MASCOTA ---");

            Console.Write("Ingresa el nombre de la mascota: ");
            string nombreMascota = Console.ReadLine();

            Console.Write("Ingresa la edad de la mascota: ");
            int edadMascota = int.Parse(Console.ReadLine());

            Console.Write("Ingresa el peso (kg): ");
            double peso = double.Parse(Console.ReadLine());

            Console.WriteLine("\nMascota: " + nombreMascota);
            Console.WriteLine("Edad el próximo año: " + (edadMascota + 1));
            Console.WriteLine("Peso: " + peso + " kg");


         
            Console.WriteLine("\n--- REGISTRO DE COMPRA ---");

            Console.Write("Ingresa el nombre del producto: ");
            string producto = Console.ReadLine();

            Console.Write("Ingresa la cantidad comprada: ");
            int cantidad = int.Parse(Console.ReadLine());

            Console.Write("Ingresa el precio unitario: ");
            double precio = double.Parse(Console.ReadLine());

            double total = cantidad * precio;

            Console.WriteLine("\nProducto: " + producto);
            Console.WriteLine("Cantidad: " + cantidad);
            Console.WriteLine("Total a pagar: $" + total);


            Console.WriteLine("\nPresiona ENTER para finalizar el programa.");
            Console.ReadLine();
        }
    }
}
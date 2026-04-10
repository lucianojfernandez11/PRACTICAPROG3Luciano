using System;

namespace Programa07
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== EJERCICIO 1: TIENDA ===");

            const decimal TASA_IVA1 = 0.21m;
            const char SIMBOLO1 = '$';

            decimal precio1 = 1000m;
            decimal iva1 = precio1 * TASA_IVA1;
            decimal total1 = precio1 + iva1;

            Console.WriteLine("Precio: " + SIMBOLO1 + precio1);
            Console.WriteLine("IVA: " + SIMBOLO1 + iva1);
            Console.WriteLine("Total: " + SIMBOLO1 + total1);

            Console.WriteLine("\n----------------------\n");

            Console.WriteLine("=== EJERCICIO 2: SUPERMERCADO ===");

            const decimal TASA_IVA2 = 0.21m;
            const char SIMBOLO2 = '$';

            decimal precio2 = 2500.75m;
            decimal iva2 = precio2 * TASA_IVA2;
            decimal total2 = precio2 + iva2;

            Console.WriteLine("Precio: " + SIMBOLO2 + precio2);
            Console.WriteLine("IVA: " + SIMBOLO2 + iva2);
            Console.WriteLine("Total: " + SIMBOLO2 + total2);

            Console.WriteLine("\n----------------------\n");

            Console.WriteLine("=== EJERCICIO 3: ELECTRÓNICA ===");

            const decimal TASA_IVA3 = 0.21m;
            const char SIMBOLO3 = '$';

            decimal precio3 = 8000m;
            decimal iva3 = precio3 * TASA_IVA3;
            decimal total3 = precio3 + iva3;

            Console.WriteLine("Precio: " + SIMBOLO3 + precio3);
            Console.WriteLine("IVA: " + SIMBOLO3 + iva3);
            Console.WriteLine("Total: " + SIMBOLO3 + total3);

            Console.WriteLine("\n----------------------\n");

            Console.WriteLine("=== EJERCICIO 4: ROPA ===");

            const decimal TASA_IVA4 = 0.21m;
            const char SIMBOLO4 = '$';

            decimal precio4 = 3200.30m;
            decimal iva4 = precio4 * TASA_IVA4;
            decimal total4 = precio4 + iva4;

            Console.WriteLine("Precio: " + SIMBOLO4 + precio4);
            Console.WriteLine("IVA: " + SIMBOLO4 + iva4);
            Console.WriteLine("Total: " + SIMBOLO4 + total4);

            Console.WriteLine("\n----------------------\n");

            Console.WriteLine("=== EJERCICIO 5: LIBRERÍA ===");

            const decimal TASA_IVA5 = 0.21m;
            const char SIMBOLO5 = '$';

            decimal precio5 = 950.99m;
            decimal iva5 = precio5 * TASA_IVA5;
            decimal total5 = precio5 + iva5;

            Console.WriteLine("Precio: " + SIMBOLO5 + precio5);
            Console.WriteLine("IVA: " + SIMBOLO5 + iva5);
            Console.WriteLine("Total: " + SIMBOLO5 + total5);

            Console.WriteLine("\nFIN");

            Console.ReadLine();
        }
    }
}
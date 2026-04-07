using System;

namespace Programa03
{
    class Program
    {
        static void Main(string[] args)
        {
           
            Console.WriteLine("--- BLOQUE 1: NÚMEROS CON DECIMALES (double) ---");

            double estatura = 1.75;
            Console.WriteLine("1. TIPO DECIMAL COMÚN (double):");
            Console.WriteLine("   Usado para medidas y cálculos generales.");
            Console.WriteLine("   Valor guardado: " + estatura + " metros");


            Console.WriteLine("\n--- BLOQUE 2: NÚMEROS FINANCIEROS (decimal) ---");

            decimal precioProducto = 199.99m;
            Console.WriteLine("2. TIPO DECIMAL FINANCIERO (decimal):");
            Console.WriteLine("   Ideal para dinero. Requiere la letra 'm'.");
            Console.WriteLine("   Valor guardado: $" + precioProducto);


    
            Console.WriteLine("\n--- BLOQUE 3: NÚMEROS DECIMALES (float) ---");

            float temperatura = 36.5f;
            Console.WriteLine("3. TIPO FLOAT (float):");
            Console.WriteLine("   Usa menos memoria que double.");
            Console.WriteLine("   Necesita la letra 'f'.");
            Console.WriteLine("   Valor guardado: " + temperatura + " °C");

            Console.WriteLine("\n--- BLOQUE 4: OPERACIONES ---");

            double numero1 = 5.5;
            double numero2 = 2.2;
            double resultado = numero1 + numero2;

            Console.WriteLine("4. Operación con decimales:");
            Console.WriteLine("   " + numero1 + " + " + numero2 + " = " + resultado);

            Console.WriteLine("\n--- BLOQUE 5: CONVERSIÓN DE TIPOS ---");

            int entero = 10;
            double convertido = entero;

            Console.WriteLine("5. Conversión automática:");
            Console.WriteLine("   Entero: " + entero);
            Console.WriteLine("   Convertido a double: " + convertido);


            Console.WriteLine("\nPresiona ENTER para cerrar.");
            Console.ReadLine();
        }
    }
}
using System;

namespace Programa02
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("--- BLOQUE 1: NÚMEROS ENTEROS ---");

            int edad = 28;

            Console.WriteLine("1. TIPO ENTERO (int):");
            Console.WriteLine("   Para números sin decimales, como edad o cantidad.");
            Console.WriteLine("   Valor guardado: " + edad);

            Console.WriteLine("\n--- BLOQUE 2: NÚMEROS DECIMALES ---");

            double altura = 1.75;

            Console.WriteLine("2. TIPO DECIMAL (double):");
            Console.WriteLine("   Para números con decimales.");
            Console.WriteLine("   Valor guardado: " + altura);


            Console.WriteLine("\n--- BLOQUE 3: TEXTO ---");

            string nombre = "Carlos";

            Console.WriteLine("3. TIPO TEXTO (string):");
            Console.WriteLine("   Para guardar palabras o frases.");
            Console.WriteLine("   Valor guardado: " + nombre);

            Console.WriteLine("\n--- BLOQUE 4: BOOLEANO ---");

            bool esMayorDeEdad = true;

            Console.WriteLine("4. TIPO BOOLEANO (bool):");
            Console.WriteLine("   Solo permite verdadero o falso.");
            Console.WriteLine("   Valor guardado: " + esMayorDeEdad);

            Console.WriteLine("\n--- BLOQUE 5: CARÁCTER ---");

            char letra = 'A';

            Console.WriteLine("5. TIPO CARÁCTER (char):");
            Console.WriteLine("   Guarda un solo carácter.");
            Console.WriteLine("   Valor guardado: " + letra);


            Console.WriteLine("\nPresiona ENTER para cerrar.");
            Console.ReadLine();
        }
    }
}
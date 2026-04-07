using System;

namespace Programa04
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("--- BLOQUE 1: TEXTO (string) ---");

            string nombre = "Ana López";
            Console.WriteLine("1. TIPO TEXTO (string):");
            Console.WriteLine("   Guarda palabras o frases.");
            Console.WriteLine("   Valor guardado: " + nombre);


           
            Console.WriteLine("\n--- BLOQUE 2: CARÁCTER (char) ---");

            char inicialNombre = 'A';
            Console.WriteLine("2. TIPO CARÁCTER (char):");
            Console.WriteLine("   Guarda un solo carácter.");
            Console.WriteLine("   Valor guardado: '" + inicialNombre + "'");


           
            Console.WriteLine("\n--- BLOQUE 3: UNIÓN DE TEXTOS ---");

            string apellido = "García";
            string nombreCompleto = nombre + " " + apellido;

            Console.WriteLine("3. Concatenación de strings:");
            Console.WriteLine("   Nombre completo: " + nombreCompleto);


          
            Console.WriteLine("\n--- BLOQUE 4: LONGITUD DEL TEXTO ---");

            int cantidadLetras = nombreCompleto.Length;

            Console.WriteLine("4. Cantidad de caracteres:");
            Console.WriteLine("   El texto tiene " + cantidadLetras + " caracteres");


            Console.WriteLine("\n--- BLOQUE 5: FORMATO DE TEXTO ---");

            Console.WriteLine("5. Texto en MAYÚSCULAS: " + nombreCompleto.ToUpper());
            Console.WriteLine("   Texto en minúsculas: " + nombreCompleto.ToLower());


            Console.WriteLine("\nPresiona ENTER para cerrar.");
            Console.ReadLine();
        }
    }
}
using System;

class Programa
{
    static void Main(string[] args)
    {
        int opcion;

        do
        {
            Console.Clear();
            Console.WriteLine("=== MENU FOREACH ===");
            Console.WriteLine("1. Mostrar frutas");
            Console.WriteLine("2. Sumar números");
            Console.WriteLine("3. Mostrar número mayor");
            Console.WriteLine("4. Contar pares e impares");
            Console.WriteLine("5. Mostrar palabras con longitud");
            Console.WriteLine("0. Salir");
            Console.Write("Seleccione una opción: ");

            opcion = int.Parse(Console.ReadLine());
            Console.Clear();

            if (opcion == 1)
            {
                string[] frutas = { "Manzana", "Banana", "Uva", "Naranja" };

                Console.WriteLine("Lista de frutas:");
                foreach (string fruta in frutas)
                {
                    Console.WriteLine("- " + fruta);
                }
            }
            else if (opcion == 2)
            {
                int[] numeros = { 10, 20, 30, 40, 50 };
                int suma = 0;

                foreach (int num in numeros)
                {
                    suma += num;
                }

                Console.WriteLine("La suma total es: " + suma);
            }
            else if (opcion == 3)
            {
                int[] numeros = { 15, 8, 22, 5, 30 };
                int mayor = numeros[0];

                foreach (int num in numeros)
                {
                    if (num > mayor)
                    {
                        mayor = num;
                    }
                }

                Console.WriteLine("El número mayor es: " + mayor);
            }
            else if (opcion == 4)
            {
                int[] numeros = { 1, 2, 3, 4, 5, 6 };

                int pares = 0;
                int impares = 0;

                foreach (int num in numeros)
                {
                    if (num % 2 == 0)
                        pares++;
                    else
                        impares++;
                }

                Console.WriteLine("Cantidad de pares: " + pares);
                Console.WriteLine("Cantidad de impares: " + impares);
            }
            else if (opcion == 5)
            {
                string[] palabras = { "Hola", "Programación", "CSharp", "ChatGPT" };

                Console.WriteLine("Palabras y su longitud:");
                foreach (string palabra in palabras)
                {
                    Console.WriteLine(palabra + " - " + palabra.Length + " letras");
                }
            }

            if (opcion != 0)
            {
                Console.WriteLine("\nPresione Enter para continuar...");
                Console.ReadLine();
            }

        } while (opcion != 0);
    }
}
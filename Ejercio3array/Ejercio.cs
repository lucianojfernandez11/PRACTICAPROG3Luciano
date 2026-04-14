using System;

namespace GuiaArreglos
{
    class Ejercicios
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== EJERCICIO 1: RECORRER NÚMEROS ===");

            int[] numeros = { 5, 10, 15, 20 };

            int cantidad1 = numeros.Length;
            Console.WriteLine("El arreglo tiene " + cantidad1 + " elementos");
            Console.WriteLine("----------------------------------");

            for (int i = 0; i < numeros.Length; i++)
            {
                Console.WriteLine("Índice " + i + ": " + numeros[i]);
            }

            Console.WriteLine("\n=== EJERCICIO 2: MOSTRAR NOMBRES ===");

            string[] nombres = { "Ana", "Luis", "Pedro", "Sofía" };

            int cantidad2 = nombres.Length;
            Console.WriteLine("Cantidad de nombres: " + cantidad2);
            Console.WriteLine("----------------------------------");

            for (int i = 0; i < nombres.Length; i++)
            {
                Console.WriteLine("Índice " + i + ": " + nombres[i]);
            }

            Console.WriteLine("\n=== EJERCICIO 3: SUMA DE ELEMENTOS ===");

            int[] numeros2 = { 2, 4, 6, 8 };

            int suma = 0;

            for (int i = 0; i < numeros2.Length; i++)
            {
                suma += numeros2[i];
            }

            Console.WriteLine("La suma total es: " + suma);

            Console.WriteLine("\n=== EJERCICIO 4: MAYOR DEL ARREGLO ===");

            int[] numeros3 = { 12, 7, 25, 3, 18 };

            int mayor = numeros3[0];

            for (int i = 0; i < numeros3.Length; i++)
            {
                if (numeros3[i] > mayor)
                {
                    mayor = numeros3[i];
                }
            }

            Console.WriteLine("El número mayor es: " + mayor);

            Console.WriteLine("\n=== EJERCICIO 5: PROMEDIO ===");

            int[] notas = { 6, 7, 8, 9, 10 };

            int sumaNotas = 0;

            for (int i = 0; i < notas.Length; i++)
            {
                sumaNotas += notas[i];
            }

            double promedio = (double)sumaNotas / notas.Length;

            Console.WriteLine("El promedio es: " + promedio);

            Console.WriteLine("\nPresione Enter para finalizar...");
            Console.ReadLine();
        }
    }
}
using System;

class Programa
{
    static void Main()
    {
        int opcion;

        do
        {
            Console.Clear();
            Console.WriteLine("=== MENU DE EJERCICIOS ===");
            Console.WriteLine("1. Cargar y mostrar números");
            Console.WriteLine("2. Suma de elementos");
            Console.WriteLine("3. Número mayor");
            Console.WriteLine("4. Contar pares e impares");
            Console.WriteLine("5. Invertir arreglo");
            Console.WriteLine("0. Salir");
            Console.Write("Seleccione una opción: ");

            opcion = int.Parse(Console.ReadLine());

            Console.Clear();

            if (opcion == 1)
            {
                int[] numeros = new int[5];

                for (int i = 0; i < numeros.Length; i++)
                {
                    Console.Write("Ingrese un número: ");
                    numeros[i] = int.Parse(Console.ReadLine());
                }

                Console.WriteLine("\nNúmeros ingresados:");
                for (int i = 0; i < numeros.Length; i++)
                {
                    Console.WriteLine(numeros[i]);
                }
            }
            else if (opcion == 2)
            {
                int[] numeros = { 10, 20, 30, 40, 50 };
                int suma = 0;

                for (int i = 0; i < numeros.Length; i++)
                {
                    suma += numeros[i];
                }

                Console.WriteLine("La suma total es: " + suma);
            }
            else if (opcion == 3)
            {
                int[] numeros = { 15, 8, 22, 5, 30 };
                int mayor = numeros[0];

                for (int i = 1; i < numeros.Length; i++)
                {
                    if (numeros[i] > mayor)
                    {
                        mayor = numeros[i];
                    }
                }

                Console.WriteLine("El número mayor es: " + mayor);
            }
            else if (opcion == 4)
            {
                int[] numeros = { 1, 2, 3, 4, 5, 6 };

                int pares = 0;
                int impares = 0;

                for (int i = 0; i < numeros.Length; i++)
                {
                    if (numeros[i] % 2 == 0)
                        pares++;
                    else
                        impares++;
                }

                Console.WriteLine("Cantidad de pares: " + pares);
                Console.WriteLine("Cantidad de impares: " + impares);
            }
            else if (opcion == 5)
            {
                int[] numeros = { 1, 2, 3, 4, 5 };

                Console.WriteLine("Arreglo invertido:");

                for (int i = numeros.Length - 1; i >= 0; i--)
                {
                    Console.WriteLine(numeros[i]);
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
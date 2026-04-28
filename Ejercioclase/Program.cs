using System;

class Program 
{
    static void Main()
    {
        string[] nombres = new string[20];
        int[] edades = new int[20];

        for (int i = 0; i < 20; i++)
        {
            Console.Write("Ingrese nombre: ");
            nombres[i] = Console.ReadLine();

            Console.Write("Ingrese edad: ");
            edades[i] = int.Parse(Console.ReadLine());
        }

        Console.WriteLine("\nRESULTADOS:");

        for (int i = 0; i < 30; i++)
        {
            if (edades[i] > 30)
            {
                Console.WriteLine(nombres[i] + " puede ingresar al boliche ");
            }
            else
            {
                Console.WriteLine(nombres[i] + " A LA CASA ");
            }
        }
    }
}
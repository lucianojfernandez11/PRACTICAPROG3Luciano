using System;

namespace Programa06
{
    class Program
    {
        static void Main(string[] args)
        {
            // ===== EJERCICIO 1 =====
            const string APP1 = "CineControl";
            const string VERSION1 = "v1.0";
            const int EDAD_MIN1 = 16;

            Console.WriteLine("=== " + APP1 + " (" + VERSION1 + ") ===");
            Console.WriteLine("Prohibido menores de " + EDAD_MIN1 + " años.");

            const int EDAD1 = 15;
            const bool ACCESO1 = EDAD1 >= EDAD_MIN1;

            Console.WriteLine("Cliente: " + EDAD1 + " años");
            Console.WriteLine("¿Puede ingresar?: " + ACCESO1);

            Console.WriteLine("\n----------------------\n");


            // ===== EJERCICIO 2 =====
            const string APP2 = "GymSystem";
            const string VERSION2 = "v2.3";
            const int EDAD_MIN2 = 14;

            Console.WriteLine("=== " + APP2 + " (" + VERSION2 + ") ===");
            Console.WriteLine("Edad mínima requerida: " + EDAD_MIN2);

            const int EDAD2 = 18;
            const bool ACCESO2 = EDAD2 >= EDAD_MIN2;

            Console.WriteLine("Cliente: " + EDAD2 + " años");
            Console.WriteLine("¿Puede entrenar?: " + ACCESO2);

            Console.WriteLine("\n----------------------\n");


            // ===== EJERCICIO 3 =====
            const string APP3 = "BarControl";
            const string VERSION3 = "v1.5";
            const int EDAD_MIN3 = 18;

            Console.WriteLine("=== " + APP3 + " (" + VERSION3 + ") ===");
            Console.WriteLine("Venta prohibida a menores de " + EDAD_MIN3);

            const int EDAD3 = 17;
            const bool ACCESO3 = EDAD3 >= EDAD_MIN3;

            Console.WriteLine("Cliente: " + EDAD3 + " años");
            Console.WriteLine("¿Puede comprar?: " + ACCESO3);

            Console.WriteLine("\n----------------------\n");


            // ===== EJERCICIO 4 =====
            const string APP4 = "AppRegistro";
            const string VERSION4 = "v3.0";
            const int EDAD_MIN4 = 13;

            Console.WriteLine("=== " + APP4 + " (" + VERSION4 + ") ===");
            Console.WriteLine("Edad mínima: " + EDAD_MIN4);

            const int EDAD4 = 12;
            const bool ACCESO4 = EDAD4 >= EDAD_MIN4;

            Console.WriteLine("Usuario: " + EDAD4 + " años");
            Console.WriteLine("¿Puede registrarse?: " + ACCESO4);

            Console.WriteLine("\n----------------------\n");


            // ===== EJERCICIO 5 =====
            const string APP5 = "CasinoVIP";
            const string VERSION5 = "v5.1";
            const int EDAD_MIN5 = 21;

            Console.WriteLine("=== " + APP5 + " (" + VERSION5 + ") ===");
            Console.WriteLine("Solo mayores de " + EDAD_MIN5);

            const int EDAD5 = 25;
            const bool ACCESO5 = EDAD5 >= EDAD_MIN5;

            Console.WriteLine("Cliente: " + EDAD5 + " años");
            Console.WriteLine("¿Puede ingresar?: " + ACCESO5);

            Console.WriteLine("\nFIN");

            Console.ReadLine();
        }
    }
}
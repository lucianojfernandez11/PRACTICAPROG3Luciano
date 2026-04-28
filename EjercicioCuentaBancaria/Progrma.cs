using System;
using System.Collections.Generic;

public class CuentaBancaria
{
    private decimal saldo;
    private List<string> historial;

    public decimal Saldo
    {
        get { return saldo; }
    }

    public IReadOnlyList<string> Historial
    {
        get { return historial.AsReadOnly(); }
    }

    public string TipoCuenta { get; private set; }

    public CuentaBancaria(string tipoCuenta)
    {
        saldo = 0;
        historial = new List<string>();
        TipoCuenta = tipoCuenta;
        historial.Add("Cuenta creada.");
    }

    public void Depositar(decimal monto)
    {
        if (monto <= 0)
        {
            Console.WriteLine("El monto debe ser mayor a 0.");
            return;
        }

        saldo += monto;
        historial.Add($"Depósito: +{monto}");
    }

    public void Retirar(decimal monto)
    {
        if (monto <= 0)
        {
            Console.WriteLine("El monto debe ser mayor a 0.");
            return;
        }

        if (monto > saldo)
        {
            Console.WriteLine("Saldo insuficiente.");
            return;
        }

        saldo -= monto;
        historial.Add($"Retiro: -{monto}");
    }

    public void AplicarInteres()
    {
        decimal interes = 0;

        if (TipoCuenta == "Ahorro")
        {
            interes = saldo * 0.03m;
        }
        else if (TipoCuenta == "Corriente")
        {
            interes = 0;
        }

        saldo += interes;
        historial.Add($"Interés aplicado: +{interes}");
    }
}

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("=== SISTEMA BANCARIO ===");

        Console.Write("Ingrese tipo de cuenta (Ahorro / Corriente): ");
        string tipo = Console.ReadLine();

        CuentaBancaria cuenta = new CuentaBancaria(tipo);

        int opcion;

        do
        {
            Console.WriteLine("\n1. Depositar");
            Console.WriteLine("2. Retirar");
            Console.WriteLine("3. Ver saldo");
            Console.WriteLine("4. Aplicar interés");
            Console.WriteLine("5. Ver historial");
            Console.WriteLine("0. Salir");
            Console.Write("Opción: ");

            opcion = int.Parse(Console.ReadLine());

            switch (opcion)
            {
                case 1:
                    Console.Write("Monto a depositar: ");
                    decimal dep = decimal.Parse(Console.ReadLine());
                    cuenta.Depositar(dep);
                    break;

                case 2:
                    Console.Write("Monto a retirar: ");
                    decimal ret = decimal.Parse(Console.ReadLine());
                    cuenta.Retirar(ret);
                    break;

                case 3:
                    Console.WriteLine($"Saldo actual: {cuenta.Saldo}");
                    break;

                case 4:
                    cuenta.AplicarInteres();
                    Console.WriteLine("Interés aplicado.");
                    break;

                case 5:
                    Console.WriteLine("Historial:");
                    foreach (var item in cuenta.Historial)
                    {
                        Console.WriteLine(item);
                    }
                    break;
            }

        } while (opcion != 0);

        Console.WriteLine("Fin del programa.");
    }
}
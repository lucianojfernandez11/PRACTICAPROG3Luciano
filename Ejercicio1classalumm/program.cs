using System;

class Alumno
{
    public string Nombre;
    public double Nota1;
    public double Nota2;

    public Alumno(string nombre, double nota1, double nota2)
    {
        Nombre = nombre;
        Nota1 = nota1;
        Nota2 = nota2;
    }
    public double CalcularPromedio()
    {
        return (Nota1 + Nota2) / 2;
    }

    public bool EstaAprobado()
    {
        return CalcularPromedio() >= 6;
    }
    public void MostrarEstado()
    {
        double promedio = CalcularPromedio();

        Console.WriteLine("Nombre: " + Nombre);
        Console.WriteLine("Promedio: " + promedio);

        if (EstaAprobado())
        {
            Console.WriteLine("Estado: Aprobado");
        }
        else
        {
            Console.WriteLine("Estado: Desaprobado");
        }
    }
}

class Program
{
    static void Main()
    {
        Alumno alumno1 = new Alumno("Juan", 7, 5);
        alumno1.MostrarEstado();
    }
}
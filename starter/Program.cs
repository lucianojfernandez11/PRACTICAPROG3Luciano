using Practico.Datos;

// RF1 — Selección de motor
// Acepta argumento de línea de comandos o muestra menú interactivo.

Motor motor;

if (args.Length > 0)
{
    motor = args[0].ToLower() switch
    {
        "postgres"   => Motor.Postgres,
        "sqlserver"  => Motor.SqlServer,
        "mysql"      => Motor.MySql,
        _ => throw new ArgumentException($"Motor desconocido: '{args[0]}'. Usá: postgres | sqlserver | mysql")
    };
}
else
{
    Console.WriteLine("Elegí el motor de base de datos:");
    Console.WriteLine("  1. PostgreSQL");
    Console.WriteLine("  2. SQL Server");
    Console.WriteLine("  3. MySQL");
    Console.Write("Opción: ");
    string opcion = Console.ReadLine() ?? "";
    motor = opcion switch
    {
        "1" => Motor.Postgres,
        "2" => Motor.SqlServer,
        "3" => Motor.MySql,
        _   => throw new ArgumentException($"Opción inválida: '{opcion}'. Elegí 1, 2 o 3.")
    };
}

string nombreMotor = motor switch
{
    Motor.Postgres  => "PostgreSQL",
    Motor.SqlServer => "SQL Server",
    Motor.MySql     => "MySQL",
    _               => motor.ToString()
};

Console.WriteLine();
Console.WriteLine($"===== MOTOR: {nombreMotor} =====");
Console.WriteLine();

// Factory entrega la estrategia correcta; el Program solo habla con IAccesoDatos
IAccesoDatos acceso = FabricaDeMotor.Crear(motor);

acceso.CrearEstructura();
Console.WriteLine();

acceso.InsertarDatosPrueba();
Console.WriteLine();

acceso.EjecutarOperaciones();
Console.WriteLine();

acceso.DemostrarRollback();
Console.WriteLine();

Console.WriteLine($"===== FIN ({nombreMotor}) =====");

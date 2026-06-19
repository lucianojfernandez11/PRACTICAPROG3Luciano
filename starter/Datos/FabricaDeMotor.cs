namespace Practico.Datos;

/// <summary>
/// Factory: centraliza la creación de la estrategia concreta según el motor elegido.
/// Agregar un nuevo motor = agregar una clase + un case acá. El Program no cambia.
/// </summary>
public static class FabricaDeMotor
{
    public static IAccesoDatos Crear(Motor motor) => motor switch
    {
        Motor.Postgres  => new AccesoPostgres(),
        Motor.SqlServer => new AccesoSqlServer(),
        Motor.MySql     => new AccesoMySql(),
        _ => throw new ArgumentOutOfRangeException(nameof(motor), $"Motor no soportado: {motor}")
    };
}

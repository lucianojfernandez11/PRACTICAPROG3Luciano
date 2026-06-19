namespace Practico.Datos;

/// <summary>
/// Interfaz Strategy: define el contrato que cada motor debe cumplir.
/// El Program solo conoce esta interfaz, nunca las implementaciones concretas.
/// </summary>
public interface IAccesoDatos
{
    /// <summary>RF2 — Crea la base 'practico' (si no existe) y las 5 tablas.</summary>
    void CrearEstructura();

    /// <summary>RF3 — Inserta datos de prueba dentro de una transacción.</summary>
    void InsertarDatosPrueba();

    /// <summary>RF4 — C1, C2, U1, D1 dentro de una transacción con commit.</summary>
    void EjecutarOperaciones();

    /// <summary>RF5 — UPDATE + excepción forzada + ROLLBACK + verificación.</summary>
    void DemostrarRollback();
}

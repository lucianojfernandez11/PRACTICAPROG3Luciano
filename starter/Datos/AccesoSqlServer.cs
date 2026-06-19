using Microsoft.Data.SqlClient;

namespace Practico.Datos;

/// <summary>
/// Estrategia concreta para SQL Server.
/// Usa Microsoft.Data.SqlClient como proveedor ADO.NET y el dialecto T-SQL:
///   - IDENTITY(1,1) para autoincrementales
///   - SCOPE_IDENTITY() para recuperar el id generado
///   - @param como marcadores de parámetros
///   - master como base de administración
/// </summary>
public class AccesoSqlServer : IAccesoDatos
{
    private const string CnxAdmin =
        "Server=localhost,1433;Database=master;User Id=sa;Password=Curso.NET2026;TrustServerCertificate=True";

    private const string CnxPractico =
        "Server=localhost,1433;Database=practico;User Id=sa;Password=Curso.NET2026;TrustServerCertificate=True";

    // -------------------------------------------------------------------------
    // RF2 — Crear estructura
    // -------------------------------------------------------------------------
    public void CrearEstructura()
    {
        Console.WriteLine("RF2 — Crear estructura");

        // Crear la base contra 'master' (fuera de transacción: DDL implica COMMIT)
        using (var cnx = new SqlConnection(CnxAdmin))
        {
            cnx.Open();
            using var cmd = cnx.CreateCommand();
            cmd.CommandText = "IF DB_ID('practico') IS NULL CREATE DATABASE practico";
            cmd.ExecuteNonQuery();
            Console.WriteLine("Base 'practico' lista.");
        }

        // Crear las 5 tablas contra 'practico' (DDL fuera de transacción)
        using (var cnx = new SqlConnection(CnxPractico))
        {
            cnx.Open();
            using var cmd = cnx.CreateCommand();

            // Ejecutamos cada sentencia DDL por separado (SQL Server es estricto con GO)
            var sentencias = new[]
            {
                "DROP TABLE IF EXISTS detalle_pedido",
                "DROP TABLE IF EXISTS pedidos",
                "DROP TABLE IF EXISTS productos",
                "DROP TABLE IF EXISTS clientes",
                "DROP TABLE IF EXISTS categorias",
                @"CREATE TABLE categorias (
                    id      INT IDENTITY(1,1) PRIMARY KEY,
                    nombre  NVARCHAR(60) NOT NULL UNIQUE
                )",
                @"CREATE TABLE clientes (
                    id      INT IDENTITY(1,1) PRIMARY KEY,
                    nombre  NVARCHAR(80)  NOT NULL,
                    email   NVARCHAR(120) NOT NULL UNIQUE
                )",
                @"CREATE TABLE productos (
                    id           INT IDENTITY(1,1) PRIMARY KEY,
                    nombre       NVARCHAR(100)  NOT NULL,
                    precio       DECIMAL(10,2)  NOT NULL CHECK (precio >= 0),
                    stock        INT            NOT NULL DEFAULT 0,
                    categoria_id INT            NOT NULL
                        CONSTRAINT fk_prod_cat FOREIGN KEY REFERENCES categorias(id)
                )",
                @"CREATE TABLE pedidos (
                    id         INT IDENTITY(1,1) PRIMARY KEY,
                    cliente_id INT           NOT NULL
                        CONSTRAINT fk_ped_cli FOREIGN KEY REFERENCES clientes(id) ON DELETE CASCADE,
                    fecha      DATETIME2     NOT NULL DEFAULT SYSDATETIME(),
                    estado     NVARCHAR(20)  NOT NULL DEFAULT 'pendiente'
                )",
                @"CREATE TABLE detalle_pedido (
                    pedido_id       INT           NOT NULL
                        CONSTRAINT fk_det_ped FOREIGN KEY REFERENCES pedidos(id) ON DELETE CASCADE,
                    producto_id     INT           NOT NULL
                        CONSTRAINT fk_det_prod FOREIGN KEY REFERENCES productos(id),
                    cantidad        INT           NOT NULL CHECK (cantidad > 0),
                    precio_unitario DECIMAL(10,2) NOT NULL,
                    CONSTRAINT pk_detalle PRIMARY KEY (pedido_id, producto_id)
                )"
            };

            foreach (var sql in sentencias)
            {
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();
            }

            Console.WriteLine("Estructura (5 tablas) creada.");
        }
    }

    // -------------------------------------------------------------------------
    // RF3 — Insertar datos de prueba (dentro de UNA transacción)
    // -------------------------------------------------------------------------
    public void InsertarDatosPrueba()
    {
        Console.WriteLine("RF3 — Insertar datos de prueba");

        using var cnx = new SqlConnection(CnxPractico);
        cnx.Open();
        using var tx = cnx.BeginTransaction();
        try
        {
            using var cmd = cnx.CreateCommand();
            cmd.Transaction = tx;

            // Helper: insert y retorna el SCOPE_IDENTITY() de forma segura
            int Insert(string sql, Action<SqlCommand> parametrizar)
            {
                parametrizar(cmd);
                cmd.CommandText = sql + "; SELECT CAST(SCOPE_IDENTITY() AS INT)";
                return Convert.ToInt32(cmd.ExecuteScalar());
            }

            // --- Categorías ---
            cmd.Parameters.Clear();
            cmd.Parameters.Add(new SqlParameter("@n", ""));

            cmd.Parameters["@n"].Value = "Electrónica";
            int catElectronica = Insert("INSERT INTO categorias (nombre) VALUES (@n)", _ => { });

            cmd.Parameters["@n"].Value = "Libros";
            int catLibros = Insert("INSERT INTO categorias (nombre) VALUES (@n)", _ => { });

            cmd.Parameters["@n"].Value = "Hogar";
            int catHogar = Insert("INSERT INTO categorias (nombre) VALUES (@n)", _ => { });

            // --- Productos ---
            cmd.CommandText = "INSERT INTO productos (nombre, precio, stock, categoria_id) VALUES (@nombre, @precio, @stock, @cat); SELECT CAST(SCOPE_IDENTITY() AS INT)";
            cmd.Parameters.Clear();
            cmd.Parameters.Add(new SqlParameter("@nombre", ""));
            cmd.Parameters.Add(new SqlParameter("@precio", 0m));
            cmd.Parameters.Add(new SqlParameter("@stock", 0));
            cmd.Parameters.Add(new SqlParameter("@cat", 0));

            int InsertarProducto(string nombre, decimal precio, int stock, int cat)
            {
                cmd.Parameters["@nombre"].Value = nombre;
                cmd.Parameters["@precio"].Value = precio;
                cmd.Parameters["@stock"].Value = stock;
                cmd.Parameters["@cat"].Value = cat;
                return Convert.ToInt32(cmd.ExecuteScalar());
            }

            int pNotebook = InsertarProducto("Notebook 14\"", 850000m, 10, catElectronica);
            int pMouse = InsertarProducto("Mouse inalámbrico", 12000m, 50, catElectronica);
            int pTeclado = InsertarProducto("Teclado mecánico", 35000m, 30, catElectronica);
            InsertarProducto("Clean Code", 28000m, 20, catLibros);
            InsertarProducto("Lámpara LED escritorio", 15000m, 25, catHogar);

            // --- Clientes ---
            cmd.CommandText = "INSERT INTO clientes (nombre, email) VALUES (@n, @e); SELECT CAST(SCOPE_IDENTITY() AS INT)";
            cmd.Parameters.Clear();
            cmd.Parameters.Add(new SqlParameter("@n", ""));
            cmd.Parameters.Add(new SqlParameter("@e", ""));

            cmd.Parameters["@n"].Value = "Ana Gómez";
            cmd.Parameters["@e"].Value = "ana@example.com";
            int cli1 = Convert.ToInt32(cmd.ExecuteScalar());

            cmd.Parameters["@n"].Value = "Bruno Díaz";
            cmd.Parameters["@e"].Value = "bruno@example.com";
            int cli2 = Convert.ToInt32(cmd.ExecuteScalar());

            // --- Pedidos ---
            cmd.CommandText = "INSERT INTO pedidos (cliente_id, estado) VALUES (@c, @e); SELECT CAST(SCOPE_IDENTITY() AS INT)";
            cmd.Parameters.Clear();
            cmd.Parameters.Add(new SqlParameter("@c", 0));
            cmd.Parameters.Add(new SqlParameter("@e", ""));

            cmd.Parameters["@c"].Value = cli1;
            cmd.Parameters["@e"].Value = "pagado";
            int ped1 = Convert.ToInt32(cmd.ExecuteScalar());

            cmd.Parameters["@c"].Value = cli2;
            cmd.Parameters["@e"].Value = "pendiente";
            int ped2 = Convert.ToInt32(cmd.ExecuteScalar());

            // --- Detalle ---
            cmd.CommandText = @"
                INSERT INTO detalle_pedido (pedido_id, producto_id, cantidad, precio_unitario)
                VALUES (@ped, @prod, @cant, @pu)";
            cmd.Parameters.Clear();
            cmd.Parameters.Add(new SqlParameter("@ped", 0));
            cmd.Parameters.Add(new SqlParameter("@prod", 0));
            cmd.Parameters.Add(new SqlParameter("@cant", 0));
            cmd.Parameters.Add(new SqlParameter("@pu", 0m));

            void InsertarDetalle(int ped, int prod, int cant, decimal pu)
            {
                cmd.Parameters["@ped"].Value = ped;
                cmd.Parameters["@prod"].Value = prod;
                cmd.Parameters["@cant"].Value = cant;
                cmd.Parameters["@pu"].Value = pu;
                cmd.ExecuteNonQuery();
            }

            InsertarDetalle(ped1, pMouse, 2, 12000m);
            InsertarDetalle(ped1, pNotebook, 1, 850000m);
            InsertarDetalle(ped1, pTeclado, 1, 35000m);
            InsertarDetalle(ped2, pMouse, 1, 12000m);
            InsertarDetalle(ped2, pTeclado, 2, 35000m);

            tx.Commit();
            Console.WriteLine("Datos de prueba insertados (commit).");
        }
        catch
        {
            tx.Rollback();
            throw;
        }
    }

    // -------------------------------------------------------------------------
    // RF4 — Operaciones en transacción
    // -------------------------------------------------------------------------
    public void EjecutarOperaciones()
    {
        Console.WriteLine("RF4 — Ejecutar operaciones (C1, C2, U1, D1)");

        using var cnx = new SqlConnection(CnxPractico);
        cnx.Open();
        using var tx = cnx.BeginTransaction();
        try
        {
            using var cmd = cnx.CreateCommand();
            cmd.Transaction = tx;

            // C1: INNER JOIN productos + categorias
            Console.WriteLine("[C1] Productos con su categoría:");
            cmd.CommandText = @"
                SELECT p.id, p.nombre, p.precio, c.nombre AS categoria
                FROM productos p
                INNER JOIN categorias c ON p.categoria_id = c.id
                ORDER BY p.id";
            using (var reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    Console.WriteLine($"  #{reader.GetInt32(0)} {reader.GetString(1)} — ${reader.GetDecimal(2):F2} [{reader.GetString(3)}]");
                }
            }

            // C2: Detalle pedido #1 con total
            Console.WriteLine("[C2] Detalle y total del pedido #1:");
            cmd.CommandText = @"
                SELECT pr.nombre, d.cantidad, d.precio_unitario,
                       d.cantidad * d.precio_unitario AS subtotal
                FROM detalle_pedido d
                INNER JOIN productos pr ON d.producto_id = pr.id
                WHERE d.pedido_id = 1
                ORDER BY pr.nombre";
            using (var reader = cmd.ExecuteReader())
            {
                decimal total = 0;
                while (reader.Read())
                {
                    decimal sub = reader.GetDecimal(3);
                    total += sub;
                    Console.WriteLine($"  {reader.GetString(0)} x{reader.GetInt32(1)} @ ${reader.GetDecimal(2):F2} = ${sub:F2}");
                }
                Console.WriteLine($"  TOTAL pedido #1: ${total:F2}");
            }

            // U1: Subir 10% a la primera categoría
            cmd.CommandText = @"
                UPDATE productos SET precio = precio * 1.10
                WHERE categoria_id = (SELECT TOP 1 id FROM categorias ORDER BY id)";
            int filas = cmd.ExecuteNonQuery();
            Console.WriteLine($"[U1] Subí 10% precios de categoría #1 -> {filas} filas.");

            // D1: Borrar línea de detalle usando paginación analítica para asegurar portabilidad
            cmd.CommandText = @"
                DELETE FROM detalle_pedido
                WHERE pedido_id = 1
                  AND producto_id = (
                      SELECT id FROM (
                          SELECT id, ROW_NUMBER() OVER (ORDER BY id) AS rn FROM productos
                      ) t WHERE rn = 2
                  )";
            filas = cmd.ExecuteNonQuery();
            Console.WriteLine($"[D1] Borré línea (pedido 1, producto 2) -> {filas} filas.");

            tx.Commit();
            Console.WriteLine("Operaciones confirmadas (commit).");
        }
        catch
        {
            tx.Rollback();
            throw;
        }
    }

    // -------------------------------------------------------------------------
    // RF5 — Demostrar rollback
    // -------------------------------------------------------------------------
    public void DemostrarRollback()
    {
        Console.WriteLine("RF5 — Demostrar rollback");

        using var cnx = new SqlConnection(CnxPractico);
        cnx.Open();

        decimal precioAntes;
        using (var cmd = cnx.CreateCommand())
        {
            cmd.CommandText = "SELECT TOP 1 precio FROM productos ORDER BY id";
            precioAntes = Convert.ToDecimal(cmd.ExecuteScalar());
        }
        Console.WriteLine($"Precio del producto #1 ANTES: ${precioAntes:F2}");

        using var tx = cnx.BeginTransaction();
        try
        {
            using var cmd = cnx.CreateCommand();
            cmd.Transaction = tx;
            cmd.CommandText = "UPDATE productos SET precio = 1 WHERE id = (SELECT TOP 1 id FROM productos ORDER BY id)";
            cmd.ExecuteNonQuery();
            Console.WriteLine("UPDATE aplicado (precio -> 1) dentro de la transacción.");

            throw new Exception("algo salió mal.");
        }
        catch (Exception ex)
        {
            tx.Rollback();
            Console.WriteLine($"Excepción capturada -> ROLLBACK. (Error simulado: {ex.Message})");
        }

        decimal precioDespues;
        using (var cmd = cnx.CreateCommand())
        {
            cmd.CommandText = "SELECT TOP 1 precio FROM productos ORDER BY id";
            precioDespues = Convert.ToDecimal(cmd.ExecuteScalar());
        }
        Console.WriteLine($"Precio del producto #1 DESPUÉS: ${precioDespues:F2}");
        Console.WriteLine(precioAntes == precioDespues
            ? "OK: el rollback funcionó, el dato NO cambió."
            : "ERROR: el rollback falló, el dato cambió.");
    }
}
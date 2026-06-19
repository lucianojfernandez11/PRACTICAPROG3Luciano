using MySqlConnector;

namespace Practico.Datos;

/// <summary>
/// Estrategia concreta para MySQL.
/// Usa MySqlConnector como proveedor ADO.NET y el dialecto SQL de MySQL.
/// </summary>
public class AccesoMySql : IAccesoDatos
{
    private const string CnxAdmin =
        "Server=localhost;Port=3306;User Id=root;Password=Curso.NET2026;";

    private const string CnxPractico =
        "Server=localhost;Port=3306;Database=practico;User Id=root;Password=Curso.NET2026;";

    // -------------------------------------------------------------------------
    // RF2 — Crear estructura
    // -------------------------------------------------------------------------
    public void CrearEstructura()
    {
        Console.WriteLine("RF2 — Crear estructura");

        using (var cnx = new MySqlConnection(CnxAdmin))
        {
            cnx.Open();
            using var cmd = cnx.CreateCommand();
            cmd.CommandText = "CREATE DATABASE IF NOT EXISTS practico CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci";
            cmd.ExecuteNonQuery();
            Console.WriteLine("Base 'practico' lista.");
        }

        using (var cnx = new MySqlConnection(CnxPractico))
        {
            cnx.Open();
            using var cmd = cnx.CreateCommand();

            cmd.CommandText = "SET FOREIGN_KEY_CHECKS = 0";
            cmd.ExecuteNonQuery();

            var sentencias = new[]
            {
                "DROP TABLE IF EXISTS detalle_pedido",
                "DROP TABLE IF EXISTS pedidos",
                "DROP TABLE IF EXISTS productos",
                "DROP TABLE IF EXISTS clientes",
                "DROP TABLE IF EXISTS categorias",
                @"CREATE TABLE categorias (
                    id      INT AUTO_INCREMENT PRIMARY KEY,
                    nombre  VARCHAR(60) NOT NULL UNIQUE
                ) ENGINE=InnoDB",
                @"CREATE TABLE clientes (
                    id      INT AUTO_INCREMENT PRIMARY KEY,
                    nombre  VARCHAR(80)  NOT NULL,
                    email   VARCHAR(120) NOT NULL UNIQUE
                ) ENGINE=InnoDB",
                @"CREATE TABLE productos (
                    id           INT AUTO_INCREMENT PRIMARY KEY,
                    nombre       VARCHAR(100)  NOT NULL,
                    precio       DECIMAL(10,2) NOT NULL CHECK (precio >= 0),
                    stock        INT           NOT NULL DEFAULT 0,
                    categoria_id INT           NOT NULL,
                    CONSTRAINT fk_prod_cat FOREIGN KEY (categoria_id) REFERENCES categorias(id)
                        ON DELETE RESTRICT ON UPDATE CASCADE
                ) ENGINE=InnoDB",
                @"CREATE TABLE pedidos (
                    id         INT AUTO_INCREMENT PRIMARY KEY,
                    cliente_id INT      NOT NULL,
                    fecha      DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
                    estado     VARCHAR(20) NOT NULL DEFAULT 'pendiente',
                    CONSTRAINT fk_ped_cli FOREIGN KEY (cliente_id) REFERENCES clientes(id)
                        ON DELETE CASCADE ON UPDATE CASCADE
                ) ENGINE=InnoDB",
                @"CREATE TABLE detalle_pedido (
                    pedido_id       INT           NOT NULL,
                    producto_id     INT           NOT NULL,
                    cantidad        INT           NOT NULL CHECK (cantidad > 0),
                    precio_unitario DECIMAL(10,2) NOT NULL,
                    PRIMARY KEY (pedido_id, producto_id),
                    CONSTRAINT fk_det_ped FOREIGN KEY (pedido_id) REFERENCES pedidos(id)
                        ON DELETE CASCADE ON UPDATE CASCADE,
                    CONSTRAINT fk_det_prod FOREIGN KEY (producto_id) REFERENCES productos(id)
                        ON DELETE RESTRICT ON UPDATE CASCADE
                ) ENGINE=InnoDB"
            };

            foreach (var sql in sentencias)
            {
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();
            }

            cmd.CommandText = "SET FOREIGN_KEY_CHECKS = 1";
            cmd.ExecuteNonQuery();

            Console.WriteLine("Estructura (5 tablas) creada.");
        }
    }

    // -------------------------------------------------------------------------
    // RF3 — Insertar datos de prueba
    // -------------------------------------------------------------------------
    public void InsertarDatosPrueba()
    {
        Console.WriteLine("RF3 — Insertar datos de prueba");

        using var cnx = new MySqlConnection(CnxPractico);
        cnx.Open();
        using var tx = cnx.BeginTransaction();
        try
        {
            using var cmd = cnx.CreateCommand();
            cmd.Transaction = tx;

            // --- Categorías ---
            cmd.Parameters.Add(new MySqlParameter("@n", ""));

            cmd.Parameters["@n"].Value = "Electrónica";
            cmd.CommandText = "INSERT INTO categorias (nombre) VALUES (@n)";
            cmd.ExecuteNonQuery();
            cmd.CommandText = "SELECT LAST_INSERT_ID()";
            int catElectronica = Convert.ToInt32(cmd.ExecuteScalar());

            cmd.Parameters["@n"].Value = "Libros";
            cmd.CommandText = "INSERT INTO categorias (nombre) VALUES (@n)";
            cmd.ExecuteNonQuery();
            cmd.CommandText = "SELECT LAST_INSERT_ID()";
            int catLibros = Convert.ToInt32(cmd.ExecuteScalar());

            cmd.Parameters["@n"].Value = "Hogar";
            cmd.CommandText = "INSERT INTO categorias (nombre) VALUES (@n)";
            cmd.ExecuteNonQuery();
            cmd.CommandText = "SELECT LAST_INSERT_ID()";
            int catHogar = Convert.ToInt32(cmd.ExecuteScalar());

            // --- Productos ---
            cmd.Parameters.Clear();
            cmd.Parameters.Add(new MySqlParameter("@nombre", ""));
            cmd.Parameters.Add(new MySqlParameter("@precio", 0m));
            cmd.Parameters.Add(new MySqlParameter("@stock", 0));
            cmd.Parameters.Add(new MySqlParameter("@cat", 0));

            int InsertarProducto(string nombre, decimal precio, int stock, int cat)
            {
                cmd.Parameters["@nombre"].Value = nombre;
                cmd.Parameters["@precio"].Value = precio;
                cmd.Parameters["@stock"].Value = stock;
                cmd.Parameters["@cat"].Value = cat;
                cmd.CommandText = "INSERT INTO productos (nombre, precio, stock, categoria_id) VALUES (@nombre, @precio, @stock, @cat)";
                cmd.ExecuteNonQuery();
                cmd.CommandText = "SELECT LAST_INSERT_ID()";
                return Convert.ToInt32(cmd.ExecuteScalar());
            }

            int pNotebook = InsertarProducto("Notebook 14\"", 850000m, 10, catElectronica);
            int pMouse = InsertarProducto("Mouse inalámbrico", 12000m, 50, catElectronica);
            int pTeclado = InsertarProducto("Teclado mecánico", 35000m, 30, catElectronica);
            InsertarProducto("Clean Code", 28000m, 20, catLibros);
            InsertarProducto("Lámpara LED escritorio", 15000m, 25, catHogar);

            // --- Clientes ---
            cmd.Parameters.Clear();
            cmd.Parameters.Add(new MySqlParameter("@n", ""));
            cmd.Parameters.Add(new MySqlParameter("@e", ""));

            cmd.Parameters["@n"].Value = "Ana Gómez";
            cmd.Parameters["@e"].Value = "ana@example.com";
            cmd.CommandText = "INSERT INTO clientes (nombre, email) VALUES (@n, @e)";
            cmd.ExecuteNonQuery();
            cmd.CommandText = "SELECT LAST_INSERT_ID()";
            int cli1 = Convert.ToInt32(cmd.ExecuteScalar());

            cmd.Parameters["@n"].Value = "Bruno Díaz";
            cmd.Parameters["@e"].Value = "bruno@example.com";
            cmd.CommandText = "INSERT INTO clientes (nombre, email) VALUES (@n, @e)";
            cmd.ExecuteNonQuery();
            cmd.CommandText = "SELECT LAST_INSERT_ID()";
            int cli2 = Convert.ToInt32(cmd.ExecuteScalar());

            // --- Pedidos ---
            cmd.Parameters.Clear();
            cmd.Parameters.Add(new MySqlParameter("@c", 0));
            cmd.Parameters.Add(new MySqlParameter("@e", ""));

            cmd.Parameters["@c"].Value = cli1;
            cmd.Parameters["@e"].Value = "pagado";
            cmd.CommandText = "INSERT INTO pedidos (cliente_id, estado) VALUES (@c, @e)";
            cmd.ExecuteNonQuery();
            cmd.CommandText = "SELECT LAST_INSERT_ID()";
            int ped1 = Convert.ToInt32(cmd.ExecuteScalar());

            cmd.Parameters["@c"].Value = cli2;
            cmd.Parameters["@e"].Value = "pendiente";
            cmd.CommandText = "INSERT INTO pedidos (cliente_id, estado) VALUES (@c, @e)";
            cmd.ExecuteNonQuery();
            cmd.CommandText = "SELECT LAST_INSERT_ID()";
            int ped2 = Convert.ToInt32(cmd.ExecuteScalar());

            // --- Detalle ---
            cmd.Parameters.Clear();
            cmd.Parameters.Add(new MySqlParameter("@ped", 0));
            cmd.Parameters.Add(new MySqlParameter("@prod", 0));
            cmd.Parameters.Add(new MySqlParameter("@cant", 0));
            cmd.Parameters.Add(new MySqlParameter("@pu", 0m));
            cmd.CommandText = "INSERT INTO detalle_pedido (pedido_id, producto_id, cantidad, precio_unitario) VALUES (@ped, @prod, @cant, @pu)";

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

        using var cnx = new MySqlConnection(CnxPractico);
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
                WHERE categoria_id = (SELECT id FROM (SELECT MIN(id) AS id FROM categorias) t)";
            int filas = cmd.ExecuteNonQuery();
            Console.WriteLine($"[U1] Subí 10% precios de categoría #1 -> {filas} filas.");

            // D1: Borrar línea de detalle
            cmd.CommandText = @"
                DELETE FROM detalle_pedido
                WHERE pedido_id = 1
                  AND producto_id = (SELECT id FROM (SELECT id FROM productos ORDER BY id LIMIT 1 OFFSET 1) t)";
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

        using var cnx = new MySqlConnection(CnxPractico);
        cnx.Open();

        decimal precioAntes;
        using (var cmd = cnx.CreateCommand())
        {
            cmd.CommandText = "SELECT precio FROM productos ORDER BY id LIMIT 1";
            precioAntes = Convert.ToDecimal(cmd.ExecuteScalar());
        }
        Console.WriteLine($"Precio del producto #1 ANTES: ${precioAntes:F2}");

        using var tx = cnx.BeginTransaction();
        try
        {
            using var cmd = cnx.CreateCommand();
            cmd.Transaction = tx;
            cmd.CommandText = "UPDATE productos SET precio = 1 WHERE id = (SELECT id FROM (SELECT id FROM productos ORDER BY id LIMIT 1) t)";
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
            cmd.CommandText = "SELECT precio FROM productos ORDER BY id LIMIT 1";
            precioDespues = Convert.ToDecimal(cmd.ExecuteScalar());
        }
        Console.WriteLine($"Precio del producto #1 DESPUÉS: ${precioDespues:F2}");
        Console.WriteLine(precioAntes == precioDespues
            ? "OK: el rollback funcionó, el dato NO cambió."
            : "ERROR: el rollback falló, el dato cambió.");
    }
}
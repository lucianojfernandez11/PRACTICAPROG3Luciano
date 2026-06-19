using Npgsql;
using NpgsqlTypes;

namespace Practico.Datos;

public class AccesoPostgres : IAccesoDatos
{
    private const string CnxAdmin =
        "Host=localhost;Port=5432;Database=postgres;Username=postgres;Password=postgres";

    private const string CnxPractico =
        "Host=localhost;Port=5432;Database=practico;Username=postgres;Password=postgres";

    // -------------------------------------------------------------------------
    // RF2 — Crear estructura
    // -------------------------------------------------------------------------
    public void CrearEstructura()
    {
        Console.WriteLine("RF2 — Crear estructura");

        using (var cnx = new NpgsqlConnection(CnxAdmin))
        {
            cnx.Open();
            using var cmd = cnx.CreateCommand();
            cmd.CommandText = "SELECT COUNT(*) FROM pg_database WHERE datname = 'practico'";
            long existe = Convert.ToInt64(cmd.ExecuteScalar() ?? 0L);
            if (existe == 0)
            {
                cmd.CommandText = "CREATE DATABASE practico ENCODING 'UTF8'";
                cmd.ExecuteNonQuery();
                Console.WriteLine("Base 'practico' creada.");
            }
            else
            {
                Console.WriteLine("Base 'practico' ya existe.");
            }
        }

        using (var cnx = new NpgsqlConnection(CnxPractico))
        {
            cnx.Open();
            using var cmd = cnx.CreateCommand();
            cmd.CommandText = @"
                DROP TABLE IF EXISTS detalle_pedido;
                DROP TABLE IF EXISTS pedidos;
                DROP TABLE IF EXISTS productos;
                DROP TABLE IF EXISTS clientes;
                DROP TABLE IF EXISTS categorias;
                CREATE TABLE categorias (id SERIAL PRIMARY KEY, nombre VARCHAR(60) NOT NULL UNIQUE);
                CREATE TABLE clientes (id SERIAL PRIMARY KEY, nombre VARCHAR(80) NOT NULL, email VARCHAR(120) NOT NULL UNIQUE);
                CREATE TABLE productos (id SERIAL PRIMARY KEY, nombre VARCHAR(100) NOT NULL, precio NUMERIC(10,2) NOT NULL CHECK (precio >= 0), stock INTEGER NOT NULL DEFAULT 0, categoria_id INTEGER NOT NULL REFERENCES categorias(id) ON DELETE RESTRICT);
                CREATE TABLE pedidos (id SERIAL PRIMARY KEY, cliente_id INTEGER NOT NULL REFERENCES clientes(id) ON DELETE CASCADE, fecha TIMESTAMP NOT NULL DEFAULT NOW(), estado VARCHAR(20) NOT NULL DEFAULT 'pendiente');
                CREATE TABLE detalle_pedido (pedido_id INTEGER NOT NULL REFERENCES pedidos(id) ON DELETE CASCADE, producto_id INTEGER NOT NULL REFERENCES productos(id) ON DELETE RESTRICT, cantidad INTEGER NOT NULL CHECK (cantidad > 0), precio_unitario NUMERIC(10,2) NOT NULL, PRIMARY KEY (pedido_id, producto_id));
            ";
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

        using var cnx = new NpgsqlConnection(CnxPractico);
        cnx.Open();
        using var tx = cnx.BeginTransaction();
        try
        {
            using var cmd = cnx.CreateCommand();
            cmd.Transaction = tx;

            var pN = new NpgsqlParameter("@n", NpgsqlDbType.Varchar); cmd.Parameters.Add(pN);
            cmd.CommandText = "INSERT INTO categorias (nombre) VALUES (@n) RETURNING id";
            pN.Value = "Electrónica"; int catElectronica = Convert.ToInt32(cmd.ExecuteScalar());
            pN.Value = "Libros"; int catLibros = Convert.ToInt32(cmd.ExecuteScalar());
            pN.Value = "Hogar"; int catHogar = Convert.ToInt32(cmd.ExecuteScalar());

            cmd.Parameters.Clear();
            var pNombre = new NpgsqlParameter("@nombre", NpgsqlDbType.Varchar);
            var pPrecio = new NpgsqlParameter("@precio", NpgsqlDbType.Numeric);
            var pStock = new NpgsqlParameter("@stock", NpgsqlDbType.Integer);
            var pCat = new NpgsqlParameter("@cat", NpgsqlDbType.Integer);
            cmd.Parameters.Add(pNombre); cmd.Parameters.Add(pPrecio);
            cmd.Parameters.Add(pStock); cmd.Parameters.Add(pCat);
            cmd.CommandText = "INSERT INTO productos (nombre, precio, stock, categoria_id) VALUES (@nombre, @precio, @stock, @cat) RETURNING id";

            pNombre.Value = "Notebook 14\""; pPrecio.Value = 850000m; pStock.Value = 10; pCat.Value = catElectronica; cmd.ExecuteScalar();
            pNombre.Value = "Mouse inalámbrico"; pPrecio.Value = 12000m; pStock.Value = 50; pCat.Value = catElectronica; cmd.ExecuteScalar();
            pNombre.Value = "Teclado mecánico"; pPrecio.Value = 35000m; pStock.Value = 30; pCat.Value = catElectronica; cmd.ExecuteScalar();
            pNombre.Value = "Clean Code"; pPrecio.Value = 28000m; pStock.Value = 20; pCat.Value = catLibros; cmd.ExecuteScalar();
            pNombre.Value = "Lámpara LED escritorio"; pPrecio.Value = 15000m; pStock.Value = 25; pCat.Value = catHogar; cmd.ExecuteScalar();

            cmd.Parameters.Clear();
            var pBN = new NpgsqlParameter("@n", NpgsqlDbType.Varchar); cmd.Parameters.Add(pBN);
            cmd.CommandText = "SELECT id FROM productos WHERE nombre = @n";
            pBN.Value = "Notebook 14\""; int pNotebook = Convert.ToInt32(cmd.ExecuteScalar());
            pBN.Value = "Mouse inalámbrico"; int pMouse = Convert.ToInt32(cmd.ExecuteScalar());
            pBN.Value = "Teclado mecánico"; int pTeclado = Convert.ToInt32(cmd.ExecuteScalar());

            cmd.Parameters.Clear();
            var pCN = new NpgsqlParameter("@n", NpgsqlDbType.Varchar);
            var pCE = new NpgsqlParameter("@e", NpgsqlDbType.Varchar);
            cmd.Parameters.Add(pCN); cmd.Parameters.Add(pCE);
            cmd.CommandText = "INSERT INTO clientes (nombre, email) VALUES (@n, @e) RETURNING id";
            pCN.Value = "Ana Gómez"; pCE.Value = "ana@example.com"; int cli1 = Convert.ToInt32(cmd.ExecuteScalar());
            pCN.Value = "Bruno Díaz"; pCE.Value = "bruno@example.com"; int cli2 = Convert.ToInt32(cmd.ExecuteScalar());

            cmd.Parameters.Clear();
            var pCC = new NpgsqlParameter("@c", NpgsqlDbType.Integer);
            var pCEs = new NpgsqlParameter("@e", NpgsqlDbType.Varchar);
            cmd.Parameters.Add(pCC); cmd.Parameters.Add(pCEs);
            cmd.CommandText = "INSERT INTO pedidos (cliente_id, estado) VALUES (@c, @e) RETURNING id";
            pCC.Value = cli1; pCEs.Value = "pagado"; int ped1 = Convert.ToInt32(cmd.ExecuteScalar());
            pCC.Value = cli2; pCEs.Value = "pendiente"; int ped2 = Convert.ToInt32(cmd.ExecuteScalar());

            cmd.Parameters.Clear();
            var pDP = new NpgsqlParameter("@ped", NpgsqlDbType.Integer);
            var pDPr = new NpgsqlParameter("@prod", NpgsqlDbType.Integer);
            var pDC = new NpgsqlParameter("@cant", NpgsqlDbType.Integer);
            var pDPu = new NpgsqlParameter("@pu", NpgsqlDbType.Numeric);
            cmd.Parameters.Add(pDP); cmd.Parameters.Add(pDPr); cmd.Parameters.Add(pDC); cmd.Parameters.Add(pDPu);
            cmd.CommandText = "INSERT INTO detalle_pedido (pedido_id, producto_id, cantidad, precio_unitario) VALUES (@ped, @prod, @cant, @pu)";

            pDP.Value = ped1; pDPr.Value = pMouse; pDC.Value = 2; pDPu.Value = 12000m; cmd.ExecuteNonQuery();
            pDP.Value = ped1; pDPr.Value = pNotebook; pDC.Value = 1; pDPu.Value = 850000m; cmd.ExecuteNonQuery();
            pDP.Value = ped1; pDPr.Value = pTeclado; pDC.Value = 1; pDPu.Value = 35000m; cmd.ExecuteNonQuery();
            pDP.Value = ped2; pDPr.Value = pMouse; pDC.Value = 1; pDPu.Value = 12000m; cmd.ExecuteNonQuery();
            pDP.Value = ped2; pDPr.Value = pTeclado; pDC.Value = 2; pDPu.Value = 35000m; cmd.ExecuteNonQuery();

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
        using var cnx = new NpgsqlConnection(CnxPractico);
        cnx.Open();
        using var tx = cnx.BeginTransaction();
        try
        {
            using var cmd = cnx.CreateCommand();
            cmd.Transaction = tx;

            Console.WriteLine("[C1] Productos con su categoría:");
            cmd.CommandText = "SELECT p.id, p.nombre, p.precio, c.nombre FROM productos p INNER JOIN categorias c ON p.categoria_id = c.id ORDER BY p.id";
            using (var r = cmd.ExecuteReader())
            {
                while (r.Read())
                {
                    Console.WriteLine($"  #{r.GetInt32(0)} {r.GetString(1)} — ${r.GetDecimal(2):F2} [{r.GetString(3)}]");
                }
            }

            Console.WriteLine("[C2] Detalle y total del pedido #1:");
            cmd.CommandText = "SELECT pr.nombre, d.cantidad, d.precio_unitario, d.cantidad * d.precio_unitario FROM detalle_pedido d INNER JOIN productos pr ON d.producto_id = pr.id WHERE d.pedido_id = 1 ORDER BY pr.nombre";
            using (var r = cmd.ExecuteReader())
            {
                decimal total = 0;
                while (r.Read())
                {
                    decimal sub = r.GetDecimal(3);
                    total += sub;
                    Console.WriteLine($"  {r.GetString(0)} x{r.GetInt32(1)} @ ${r.GetDecimal(2):F2} = ${sub:F2}");
                }
                Console.WriteLine($"  TOTAL pedido #1: ${total:F2}");
            }

            cmd.CommandText = "UPDATE productos SET precio = precio * 1.10 WHERE categoria_id = (SELECT id FROM categorias ORDER BY id LIMIT 1)";
            Console.WriteLine($"[U1] Subí 10% precios de categoría #1 -> {cmd.ExecuteNonQuery()} filas.");

            cmd.CommandText = "DELETE FROM detalle_pedido WHERE pedido_id = 1 AND producto_id = (SELECT id FROM productos ORDER BY id LIMIT 1 OFFSET 1)";
            Console.WriteLine($"[D1] Borré línea (pedido 1, producto 2) -> {cmd.ExecuteNonQuery()} filas.");

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
        using var cnx = new NpgsqlConnection(CnxPractico);
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
            cmd.CommandText = "UPDATE productos SET precio = 1 WHERE id = (SELECT id FROM productos ORDER BY id LIMIT 1)";
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
        Console.WriteLine(precioAntes == precioDespues ? "OK: el rollback funcionó, el dato NO cambió." : "ERROR: el rollback falló.");
    }
}
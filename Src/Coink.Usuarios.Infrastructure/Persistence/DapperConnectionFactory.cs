using Microsoft.Extensions.Options;
using Npgsql;
using System.Data;

namespace Coink.Usuarios.Infrastructure.Persistence
{
    /// <summary>
    /// Reemplazo de DbContext sin Entity Framework.
    /// Provee conexiones a PostgreSQL de forma segura.
    /// </summary>
    public class DapperDbContext
    {
        private readonly string _connectionString;

        public DapperDbContext(IOptions<DatabaseOptions> options)
        {
            _connectionString = options.Value.UsuariosDB
                ?? throw new ArgumentNullException(nameof(options.Value.UsuariosDB));
        }

        /// <summary>
        /// Crea y retorna una conexión IDbConnection lista para usar.
        /// </summary>
        public IDbConnection CreateConnection()
        {
            return new NpgsqlConnection(_connectionString);
        }

        /// <summary>
        /// Ejecuta una operación dentro de una transacción.
        /// </summary>
        public async Task<T> ExecuteTransactionalAsync<T>(Func<IDbConnection, IDbTransaction, Task<T>> action)
        {
            using var connection = CreateConnection();
            connection.Open();

            using var transaction = connection.BeginTransaction();

            try
            {
                var result = await action(connection, transaction);
                transaction.Commit();

                return result;
            }
            catch
            {
                transaction.Rollback();
                throw;
            }
        }
    }
}

using Microsoft.Extensions.Options;
using Npgsql;
using System.Data;

namespace Coink.Usuarios.Infrastructure.Persistence
{
    public class DapperDbContext
    {
        private readonly string _connectionString;

        public DapperDbContext(IOptions<DatabaseOptions> options)
        {
            _connectionString = options.Value.UsuariosDB
                ?? throw new ArgumentNullException(nameof(options.Value.UsuariosDB));
        }


        public IDbConnection CreateConnection()
        {
            return new NpgsqlConnection(_connectionString);
        }


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

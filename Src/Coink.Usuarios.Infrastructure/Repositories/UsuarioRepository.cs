using Coink.Usuarios.Application.Interfaces;
using Coink.Usuarios.Domain.Entities;
using Coink.Usuarios.Infrastructure.Persistence;
using Dapper;
using System.Data;

namespace Coink.Usuarios.Infrastructure.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly DapperDbContext _db;

        public UsuarioRepository(DapperDbContext db)
        {
            _db = db;
        }

        public async Task<int> RegistrarAsync(Usuario usuario)
        {
            using var connection = _db.CreateConnection();

            var parameters = new DynamicParameters();
            parameters.Add("p_nombre", usuario.Nombre);
            parameters.Add("p_telefono", usuario.Telefono);
            parameters.Add("p_pais_id", usuario.PaisId);
            parameters.Add("p_departamento_id", usuario.DepartamentoId);
            parameters.Add("p_municipio_id", usuario.MunicipioId);
            parameters.Add("p_direccion", usuario.Direccion);
            parameters.Add("p_id_usuario", dbType: DbType.Int32, direction: ParameterDirection.Output);
            
            return await connection.ExecuteScalarAsync<int>(
                "usuarios.sp_registrar_usuario",
                parameters,
                commandType: CommandType.StoredProcedure);            
        }

        public async Task<Usuario> ConsultarAsync(int idUsuario)
        {
            using var connection = _db.CreateConnection();

            var sql = @"SELECT * FROM usuarios.usuario WHERE id_usuario = @IdUsuario";

            return await connection.QueryFirstOrDefaultAsync<Usuario>(
                sql, new { IdUsuario = idUsuario });
        }

        public async Task<List<Usuario>> ListarAsync()
        {
            using var connection = _db.CreateConnection();

            var sql = @"SELECT * FROM usuarios.usuario";

            var result = await connection.QueryAsync<Usuario>(sql);
            return result.AsList();
        }
    }
}

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

            var result = await connection.ExecuteScalarAsync<int>(
                "usuarios.sp_registrar_usuario",
                parameters,
                commandType: CommandType.StoredProcedure);

            return result;
        }
    }
}

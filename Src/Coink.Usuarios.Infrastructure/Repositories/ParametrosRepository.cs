using Coink.Usuarios.Application.Interfaces;
using Coink.Usuarios.Infrastructure.Persistence;
using Dapper;

public class ParametrosRepository : IParametrosRepository
{
    private readonly DapperDbContext _context;

    public ParametrosRepository(DapperDbContext context)
    {
        _context = context;
    }

    public async Task<bool> PaisExiste(int id)
    {
        using var conn = _context.CreateConnection();
        return await conn.ExecuteScalarAsync<bool>(
            "SELECT EXISTS (SELECT 1 FROM Parametros.Pais WHERE id_pais = @id)",
            new { id });
    }

    public async Task<bool> DepartamentoExiste(int id)
    {
        using var conn = _context.CreateConnection();
        return await conn.ExecuteScalarAsync<bool>(
            "SELECT EXISTS (SELECT 1 FROM Parametros.Departamento WHERE id_departamento = @id)",
            new { id });
    }

    public async Task<bool> MunicipioExiste(int id)
    {
        using var conn = _context.CreateConnection();
        return await conn.ExecuteScalarAsync<bool>(
            "SELECT EXISTS (SELECT 1 FROM Parametros.Municipio WHERE id_municipio = @id)",
            new { id });
    }

    public async Task<bool> MunicipioPerteneceAlDepartamento(int municipioId, int departamentoId)
    {
        using var conn = _context.CreateConnection();
        return await conn.ExecuteScalarAsync<bool>(
            @"SELECT EXISTS (
                SELECT 1 
                FROM Parametros.Municipio 
                WHERE id_municipio = @municipioId
                AND id_departamento = @departamentoId
            )",
            new { municipioId, departamentoId });
    }

    public async Task<bool> DepartamentoPerteneceAlPais(int departamentoId, int paisId)
    {
        using var conn = _context.CreateConnection();
        return await conn.ExecuteScalarAsync<bool>(
            @"SELECT EXISTS (
                SELECT 1
                FROM Parametros.Departamento
                WHERE id_departamento = @departamentoId
                AND id_pais = @paisId
            )",
            new { departamentoId, paisId });
    }
}


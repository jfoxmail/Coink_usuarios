using Coink.Usuarios.Domain.Entities;

namespace Coink.Usuarios.Application.Interfaces
{
    public interface IUsuarioRepository
    {
        Task<int> RegistrarAsync(Usuario usuario);
        Task<Usuario> ConsultarAsync(int idUsuario);
        Task<List<Usuario>> ListarAsync();
    }
}

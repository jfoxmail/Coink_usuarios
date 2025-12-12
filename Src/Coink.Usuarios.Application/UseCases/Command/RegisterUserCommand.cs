using MediatR;

namespace Coink.Usuarios.Application.UseCases.Command
{
    public record RegisterUserCommand(
    string Nombre,
    string Telefono,
    int PaisId,
    int DepartamentoId,
    int MunicipioId,
    string Direccion
) : IRequest<int>;
}

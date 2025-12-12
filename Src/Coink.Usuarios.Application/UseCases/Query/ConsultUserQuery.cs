using Coink.Usuarios.Domain.Entities;
using MediatR;

namespace Coink.Usuarios.Application.UseCases.Query
{
    public record ConsultUserQuery(
    int Id
) : IRequest<UsuarioDto>;
}

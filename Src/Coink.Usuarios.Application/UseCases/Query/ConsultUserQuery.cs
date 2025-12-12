using Coink.Usuarios.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coink.Usuarios.Application.UseCases.Query
{
    public record ConsultUserQuery(
    int Id
) : IRequest<UsuarioDto>;
}

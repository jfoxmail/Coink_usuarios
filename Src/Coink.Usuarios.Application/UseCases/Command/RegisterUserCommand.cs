using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

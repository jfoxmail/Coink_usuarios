using Coink.Usuarios.Application.Interfaces;
using Coink.Usuarios.Application.UseCases.Command;
using Coink.Usuarios.Domain.Entities;
using FluentValidation;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, int>
{
    private readonly IUsuarioRepository _usuarioRepository;
    private readonly RegisterUserCommandValidator _validator;

    public RegisterUserCommandHandler(
        IUsuarioRepository usuarioRepository,
        IParametrosRepository parametrosRepository)
    {
        _usuarioRepository = usuarioRepository;
        _validator = new RegisterUserCommandValidator(parametrosRepository);
    }

    public async Task<int> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        // Validación asíncrona
        var validationResult = await _validator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
        {
            // Lanza excepción si falla la validación
            throw new ValidationException(validationResult.Errors);
        }

        // Crear usuario
        var usuario = new Usuario
        {
            Nombre = request.Nombre,
            Telefono = request.Telefono,
            PaisId = request.PaisId,
            DepartamentoId = request.DepartamentoId,
            MunicipioId = request.MunicipioId,
            Direccion = request.Direccion,
            FechaCreacion = DateTime.UtcNow
        };

        // Guardar y devolver Id
        return await _usuarioRepository.RegistrarAsync(usuario);
    }
}

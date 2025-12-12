using Coink.Usuarios.Application.UseCases.Command;
using Coink.Usuarios.Application.Interfaces;
using FluentValidation;

public class RegisterUserCommandValidator : AbstractValidator<RegisterUserCommand>
{
    public RegisterUserCommandValidator(IParametrosRepository parametrosRepository)
    {
        RuleFor(x => x.Nombre)
            .NotEmpty().WithMessage("El nombre es obligatorio.");

        RuleFor(x => x.Telefono)
            .NotEmpty().WithMessage("El teléfono es obligatorio.");

        RuleFor(x => x.Direccion)
            .NotEmpty().WithMessage("La dirección es obligatoria.");

        // Validar existencia de País
        RuleFor(x => x.PaisId)
            .MustAsync(async (id, _) => await parametrosRepository.PaisExiste(id))
            .WithMessage("El país no existe.");

        // Validar existencia de Departamento
        RuleFor(x => x.DepartamentoId)
            .MustAsync(async (id, _) => await parametrosRepository.DepartamentoExiste(id))
            .WithMessage("El departamento no existe.");

        // Validar existencia de Municipio
        RuleFor(x => x.MunicipioId)
            .MustAsync(async (id, _) => await parametrosRepository.MunicipioExiste(id))
            .WithMessage("El municipio no existe.");

        // Validar jerarquía Municipio → Departamento
        RuleFor(x => x)
            .MustAsync(async (cmd, _) =>
                await parametrosRepository.MunicipioPerteneceAlDepartamento(
                    cmd.MunicipioId,
                    cmd.DepartamentoId
                ))
            .WithMessage("El municipio no pertenece al departamento seleccionado.");

        // Validar jerarquía Departamento → País
        RuleFor(x => x)
            .MustAsync(async (cmd, _) =>
                await parametrosRepository.DepartamentoPerteneceAlPais(
                    cmd.DepartamentoId,
                    cmd.PaisId
                ))
            .WithMessage("El departamento no pertenece al país seleccionado.");
    }
}

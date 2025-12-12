using Coink.Usuarios.Application.Interfaces;
using Coink.Usuarios.Application.UseCases.Command;
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


        RuleFor(x => x.PaisId)
            .MustAsync(async (id, _) => await parametrosRepository.PaisExiste(id))
            .WithMessage("El país no existe.");


        RuleFor(x => x.DepartamentoId)
            .MustAsync(async (id, _) => await parametrosRepository.DepartamentoExiste(id))
            .WithMessage("El departamento no existe.");


        RuleFor(x => x.MunicipioId)
            .MustAsync(async (id, _) => await parametrosRepository.MunicipioExiste(id))
            .WithMessage("El municipio no existe.");


        RuleFor(x => x)
            .MustAsync(async (cmd, _) =>
                await parametrosRepository.MunicipioPerteneceAlDepartamento(
                    cmd.MunicipioId,
                    cmd.DepartamentoId
                ))
            .WithMessage("El municipio no pertenece al departamento seleccionado.");


        RuleFor(x => x)
            .MustAsync(async (cmd, _) =>
                await parametrosRepository.DepartamentoPerteneceAlPais(
                    cmd.DepartamentoId,
                    cmd.PaisId
                ))
            .WithMessage("El departamento no pertenece al país seleccionado.");
    }
}

using Application.Features.Clientes.Commands.CreateClienteCommand;
using FluentValidation;

namespace Application.Features.Clientes.Validators
{
    public class CreateClienteCommandValidator : AbstractValidator<CreateClienteCommand>
    {
        public CreateClienteCommandValidator()
        {
            RuleLevelCascadeMode = CascadeMode.Stop;

            RuleFor(p => p.Nombre)
                .NotEmpty().WithMessage("{PropertyName} no puede ser vacío")
                .MaximumLength(50).WithMessage("{PropertyName} no debe exceder {MaxLength}");
            
            RuleFor(p => p.Apellido)
             .NotEmpty().WithMessage("{PropertyName} no puede ser vacío")
             .MaximumLength(50).WithMessage("{PropertyName} no debe exceder {MaxLength}");

            RuleFor(p => p.FechaNacimiento)
             .NotEmpty().WithMessage("{PropertyName} no puede ser vacío");

            RuleFor(p => p.Telefono)
               .NotEmpty().WithMessage("{PropertyName} no puede ser vacío")
               .Matches(@"^\d{4}-\d{4}$").WithMessage("{PropertyName} debe cumplir el formato 0000-0000")
               .MaximumLength(9).WithMessage("{PropertyName} no debe exceder {MaxLength}");

            RuleFor(p => p.Email)
             .NotEmpty().WithMessage("{PropertyName} no puede ser vacío")
             .EmailAddress().WithMessage("{PropertyName} debe ser una direccion de correo válida")
             .MaximumLength(100).WithMessage("{PropertyName} no debe exceder {MaxLength}");

            RuleFor(p => p.Direccion)
             .NotEmpty().WithMessage("{PropertyName} no puede ser vacío")
             .MaximumLength(120).WithMessage("{PropertyName} no debe exceder {MaxLength}");

        }
    }
}

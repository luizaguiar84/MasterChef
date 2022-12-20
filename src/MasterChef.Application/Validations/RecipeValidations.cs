using FluentValidation;
using MasterChef.Domain.Entities;
using MasterChef.Dto;
using static System.String;

namespace MasterChef.Application.Validations
{
    public class RecipeValidations : AbstractValidator<RecipeDto>
    {
        public RecipeValidations()
        {
            RuleFor(x => !IsNullOrEmpty(x.Title))
                .Must(titulo => titulo).WithMessage("O campo título é obrigatório");

            RuleFor(x => !IsNullOrEmpty(x.Description))
                .Must(descricao => descricao).WithMessage("O campo de descrição é obrigatório");

            RuleFor(x => x.Description.Length < 1000)
                .Must(descricao => descricao).WithMessage("A descrição da receita não deve conter mais que 1000 caracteres.");

            RuleFor(x => x.WayOfPrepare.Length < 5000)
                .Must(modo => modo).WithMessage("A Descrição não deve conter mais que 5000 caracteres.");
        }
    }
}

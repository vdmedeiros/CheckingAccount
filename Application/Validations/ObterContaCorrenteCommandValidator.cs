using CheckingAccount.API.Application.Commands;
using FluentValidation;

namespace CheckingAccount.API.Application.Commands
{
    public class ObterContaCorrenteCommandValidator
        : AbstractValidator<ObterContaCorrenteCommand>
    {
        public ObterContaCorrenteCommandValidator()
        {
            RuleFor(command => command.Id).NotEmpty().WithMessage("O id da conta corrente é obrigatória");
        }
    }
}

using CheckingAccount.API.Application.Commands;
using FluentValidation;

namespace CheckingAccount.API.Application.Commands
{
    public class CriarContaCorrenteCommandValidator :
        AbstractValidator<CriarContaCorrenteCommand>
    {
        public CriarContaCorrenteCommandValidator()
        {
            RuleFor(command => command.Id).NotEmpty().WithMessage("O id da conta corrente é obrigatória");
            RuleFor(command => command.CorrentistaId).NotEmpty().WithMessage("O id do correntista é obrigatório");
        }
    }
}

using CheckingAccount.API.Application.Commands;
using FluentValidation;

namespace CheckingAccount.API.Application.Commands
{
    public class EfetuarOperacaoCommandValidator 
        : AbstractValidator<EfetuarOperacaoCommand>
    {
        public EfetuarOperacaoCommandValidator()
        {
            RuleFor(command => command.ContaCorrenteOrigemId).NotEmpty().WithMessage("A conta corrente de origem é obrigatória");
            RuleFor(command => command.ContaCorrenteDestinoId).NotEmpty().WithMessage("A conta corrente de destino é obrigatória");
            RuleFor(command => command.ValorOperacao).GreaterThan(0).WithMessage("O valor da operação deve ser maior que zero");
        }
    }
}

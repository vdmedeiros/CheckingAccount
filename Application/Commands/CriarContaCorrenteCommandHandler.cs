using CheckingAccount.Domain.Aggregates.ContaCorrente;
using CheckingAccount.Domain.Aggregates.ContaCorrenteAggregate;
using CheckingAccount.Domain.SeedWork;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace CheckingAccount.API.Application.Commands
{
    public class CriarContaCorrenteCommandHandler
        : IRequestHandler<CriarContaCorrenteCommand, CommandResult>
    {
        private readonly ILogger _logger;
        private readonly IMediator _mediator;
        private readonly IContaCorrenteRepository _contaCorrenteRepository;
        private readonly IUnitOfWork _unitOfWork;
        private IEnumerable<Lancamento> GetLancamentos(decimal valor)
        {
            var tipoLancamento = TipoLancamento.Credito;
            var valorLancamento = valor;

            if (valor < 0)
            {
                tipoLancamento = TipoLancamento.Debito;
                valorLancamento = valorLancamento * -1;
            }

            return new List<Lancamento> {
                new Lancamento(
                tipoLancamento,
                DateTime.Now,
                valorLancamento)
            };
        }

        public CriarContaCorrenteCommandHandler(
            ILogger<CriarContaCorrenteCommandHandler> logger,
            IMediator mediator,
            IContaCorrenteRepository contaCorrenteRepository,
            IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _mediator = mediator;
            _contaCorrenteRepository = contaCorrenteRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<CommandResult> Handle(CriarContaCorrenteCommand request,
            CancellationToken cancellationToken)
        {
            var commandResult = new CommandResult(true);

            try
            {
                var lancamentos = GetLancamentos(request.Saldo);

                var novaContaCorrente = new ContaCorrente(
                    request.Id,
                    request.CorrentistaId,
                    lancamentos);

                _contaCorrenteRepository.Add(novaContaCorrente);
                var sucesso = await _unitOfWork.SaveEntitiesAsync();

                if (!sucesso)
                {
                    commandResult = new CommandResult(false, "Erro ao efetuar a escrita da conta corrente");
                }
            }
            catch (Exception ex)
            {
                commandResult = new CommandResult(false, ex.Message);
                _logger.LogError("Erro ao criar conta corrente", ex);
            }

            return commandResult;
        }
    }
}

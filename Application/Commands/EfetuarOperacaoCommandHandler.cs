using CheckingAccount.Domain.Aggregates.ContaCorrenteAggregate;
using CheckingAccount.Domain.SeedWork;
using CheckingAccount.Domain.Services;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CheckingAccount.API.Application.Commands
{
    public class EfetuarOperacaoCommandHandler
        : IRequestHandler<EfetuarOperacaoCommand, CommandResult>
    {
        private readonly IMediator _mediator;
        private readonly ILogger _logger;
        private readonly IContaCorrenteRepository _contaCorrenteRepository;
        private readonly IUnitOfWork _unitOfWork;

        public EfetuarOperacaoCommandHandler(
            IMediator mediator, 
            ILogger<EfetuarOperacaoCommandHandler> logger,
            IContaCorrenteRepository contaCorrenteRepository, 
            IUnitOfWork unitOfWork)
        {
            _mediator = mediator;
            _logger = logger;
            _contaCorrenteRepository = contaCorrenteRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<CommandResult> Handle(
            EfetuarOperacaoCommand request, 
            CancellationToken cancellationToken)
        {
            CommandResult commandResult = new CommandResult(true);

            try
            {
                var dataOperacao = DateTime.Now;
                var contaCorrenteOrigem = await _contaCorrenteRepository.FindByIdAsync(request.ContaCorrenteOrigemId);
                var contaCorrenteDestino = await _contaCorrenteRepository.FindByIdAsync(request.ContaCorrenteDestinoId);
                var operacaoDomainService = new OperacaoDomainService();

                operacaoDomainService.EfetuarTransacao(
                    contaCorrenteOrigem, 
                    contaCorrenteDestino, 
                    dataOperacao, 
                    request.ValorOperacao);

                var sucesso = await _unitOfWork.SaveEntitiesAsync();

                if (!sucesso)
                {
                    commandResult = new CommandResult(false, "Operação de escrita não foi efetuada");
                }
            }
            catch (Exception ex)
            {
                commandResult = new CommandResult(false, ex.Message);
            }

            return commandResult;
        }
    }
}

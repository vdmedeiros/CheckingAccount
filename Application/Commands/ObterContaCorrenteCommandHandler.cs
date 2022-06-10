using CheckingAccount.API.Application.Models;
using CheckingAccount.Domain.Aggregates.ContaCorrenteAggregate;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;

namespace CheckingAccount.API.Application.Commands
{
    public class ObterContaCorrenteCommandHandler
        : IRequestHandler<ObterContaCorrenteCommand, ContaCorrenteViewModel>
    {
        private readonly IMediator _mediator;
        private readonly ILogger _logger;
        private readonly IContaCorrenteRepository _contaCorrenteRepository;

        public ObterContaCorrenteCommandHandler(
            IMediator mediator, 
            ILogger<ObterContaCorrenteCommandHandler> logger, 
            IContaCorrenteRepository contaCorrenteRepository)
        {
            _mediator = mediator;
            _logger = logger;
            _contaCorrenteRepository = contaCorrenteRepository;
        }

        public async Task<ContaCorrenteViewModel> Handle(
            ObterContaCorrenteCommand request, 
            CancellationToken cancellationToken)
        {
            ContaCorrenteViewModel result = null;
            var contaCorrente = await _contaCorrenteRepository.FindByIdAsync(request.Id);

            if (contaCorrente != null)
            {
                result = new ContaCorrenteViewModel
                {
                    Id = contaCorrente.Id,
                    CorrentistaId = contaCorrente.CorrentistaId,
                    Saldo = contaCorrente.Saldo
                };
            }

            return result;
        }
    }
}

using CheckingAccount.API.Application.Models;
using CheckingAccount.Domain.Aggregates.ContaCorrenteAggregate;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CheckingAccount.API.Application.Commands
{
    public class ListarContaCorrenteCommandHandler 
        : IRequestHandler<ListarContaCorrenteCommand, ContaCorrenteViewModel[]>
    {
        private readonly IMediator _mediator;
        private readonly ILogger _logger;
        private readonly IContaCorrenteRepository _contaCorrenteRepository;

        public ListarContaCorrenteCommandHandler(
            IMediator mediator,
            ILogger<ListarContaCorrenteCommandHandler> logger,
            IContaCorrenteRepository contaCorrenteRepository)
        {
            _mediator = mediator;
            _logger = logger;
            _contaCorrenteRepository = contaCorrenteRepository;
        }

        public async Task<ContaCorrenteViewModel[]> Handle(
            ListarContaCorrenteCommand request, 
            CancellationToken cancellationToken)
        {
            ContaCorrenteViewModel[] result = null;
            var contasCorrentes = await _contaCorrenteRepository.GetAll();

            if (contasCorrentes != null)
            {
                result = contasCorrentes.Select(c => new ContaCorrenteViewModel
                {
                    Id = c.Id,
                    CorrentistaId = c.CorrentistaId,
                    Saldo = c.Saldo
                }).ToArray();
            }

            return result;
        }
    }
}

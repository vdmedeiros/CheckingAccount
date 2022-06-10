using CheckingAccount.API.Application.Commands;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace CheckingAccount.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class OperacaoController : ControllerBase
    {
        private readonly ILogger _logger;
        private readonly IMediator _mediator;
        
        public OperacaoController(
            ILogger<OperacaoController> logger,
            IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> EfetuarOperacao(
            [FromBody] EfetuarOperacaoCommand efetuarOperacaoCommand)
        {
            var commandResult = await _mediator.Send(efetuarOperacaoCommand);

            _logger.LogInformation($"Comando: {nameof(EfetuarOperacaoCommand)} enviado");

            if (!commandResult.Success)
            {
                return BadRequest(commandResult.Mensagem);
            }

            return Ok();
        } 
    }
}
using CheckingAccount.API.Application.Commands;
using MediatR;
using System;
using System.Runtime.Serialization;

namespace CheckingAccount.API.Application.Commands
{
    [DataContract]
    public class EfetuarOperacaoCommand : IRequest<CommandResult>
    {
        public EfetuarOperacaoCommand(Guid contaCorrenteOrigemId,
                                      Guid contaCorrenteDestinoId,
                                      decimal valorOperacao)
        {
            ContaCorrenteOrigemId = contaCorrenteOrigemId;
            ContaCorrenteDestinoId = contaCorrenteDestinoId;
            ValorOperacao = valorOperacao;
        }

        [DataMember]
        public Guid ContaCorrenteOrigemId { get; private set; }
        [DataMember]
        public Guid ContaCorrenteDestinoId { get; private set; }
        [DataMember]
        public decimal ValorOperacao { get; private set; }
    }
}

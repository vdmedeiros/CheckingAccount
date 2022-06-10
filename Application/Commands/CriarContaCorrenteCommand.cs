using CheckingAccount.API.Application.Commands;
using MediatR;
using System;
using System.Runtime.Serialization;

namespace CheckingAccount.API.Application.Commands
{
    [DataContract]
    public class CriarContaCorrenteCommand 
        : IRequest<CommandResult>
    {
        public CriarContaCorrenteCommand(
            Guid id, 
            Guid correntistaId, 
            decimal saldo)
        {           
            Id = id;
            CorrentistaId = correntistaId;
            Saldo = saldo;
        }

        [DataMember]
        public Guid Id { get; private set; }
        [DataMember]
        public Guid CorrentistaId { get; private set; }
        [DataMember]
        public decimal Saldo { get; private set; }
    }
}

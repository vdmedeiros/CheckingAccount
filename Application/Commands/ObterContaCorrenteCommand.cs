using CheckingAccount.API.Application.Models;
using MediatR;
using System;
using System.Runtime.Serialization;

namespace CheckingAccount.API.Application.Commands
{
    [DataContract]
    public class ObterContaCorrenteCommand 
        : IRequest<ContaCorrenteViewModel>
    {
        public ObterContaCorrenteCommand(Guid id)
        {
            Id = id;
        }

        [DataMember]
        public Guid Id { get; private set; }
    }
}

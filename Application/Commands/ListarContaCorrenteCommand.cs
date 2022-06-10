using CheckingAccount.API.Application.Models;
using MediatR;

namespace CheckingAccount.API.Application.Commands
{
    public class ListarContaCorrenteCommand 
        : IRequest<ContaCorrenteViewModel[]>
    {
    }
}

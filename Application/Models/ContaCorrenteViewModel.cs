using System;

namespace CheckingAccount.API.Application.Models
{
    public class ContaCorrenteViewModel
    {
        public Guid Id { get; set; }
        public Guid CorrentistaId { get; set; }
        public decimal Saldo { get; set; }
    }
}

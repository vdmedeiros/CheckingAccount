using CheckingAccount.Domain.Aggregates.ContaCorrente;
using CheckingAccount.Domain.Aggregates.ContaCorrenteAggregate;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Logging;
using System;

namespace CheckingAccount.API.Extensions
{
    public static class SeedWorkLoadRepositoryExtension
    {
        public static void LoadRepository(this IApplicationBuilder app)
        {
            var repository = (IContaCorrenteRepository)app.ApplicationServices.GetService(typeof(IContaCorrenteRepository));
            var logger = (ILogger<IContaCorrenteRepository>)app.ApplicationServices.GetService(typeof(ILogger<IContaCorrenteRepository>));
            var contaCorrenteOrigemId = Guid.Parse("2211EABD-E778-45DC-AFEA-C5ACA2E4607E");
            var saldoInicialContaOrigem = new Lancamento[] {
                new Lancamento(TipoLancamento.Credito, DateTime.Now, 9000)
            };
            var contaOrigem = new ContaCorrente(
                contaCorrenteOrigemId,
                Guid.NewGuid(),
                saldoInicialContaOrigem);

            var contaCorrenteDestinoId = Guid.Parse("A63210F1-B0A6-428D-AE2B-F076F86A4CAD");
            var saldoInicialContaDestino = new Lancamento[] {
                new Lancamento(TipoLancamento.Credito, DateTime.Now, 4500)
            };
            var contaDestino = new ContaCorrente(
                contaCorrenteDestinoId,
                Guid.NewGuid(),
                saldoInicialContaDestino);

            repository.Add(contaOrigem);            
            repository.Add(contaDestino);

            logger.LogInformation($"Conta Corrente Criada: {contaCorrenteOrigemId}, Saldo: {contaOrigem.Saldo}");
            logger.LogInformation($"Conta Corrente Criada: {contaCorrenteDestinoId}, Saldo: {contaDestino.Saldo}");
        }
    }
}

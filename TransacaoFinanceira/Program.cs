using System;
using System.Linq;
using TransacaoFinanceira.Main.Models;
using TransacaoFinanceira.Main.Repositories;
using TransacaoFinanceira.Main.Repositories.Interfaces;
using TransacaoFinanceira.Main.Services;

namespace TransacaoFinanceira.Main
{
    public class Program
    {
        static void Main(string[] args)
        {
            Transacao[] TRANSACOES = new[]
            {
                new Transacao {CorrelationId= 1, DateTime= DateTime.Parse("09/09/2023 14:15:00"), ContaOrigem= 938485762, ContaDestino= 2147483649, Valor= 150},
                new Transacao {CorrelationId= 2, DateTime= DateTime.Parse("09/09/2023 14:15:05"), ContaOrigem= 2147483649, ContaDestino= 210385733, Valor= 149},
                new Transacao {CorrelationId= 3, DateTime= DateTime.Parse("09/09/2023 14:15:29"), ContaOrigem= 347586970, ContaDestino= 238596054, Valor= 1100},
                new Transacao {CorrelationId= 4, DateTime= DateTime.Parse("09/09/2023 14:17:00"), ContaOrigem= 675869708, ContaDestino= 210385733, Valor= 5300},
                new Transacao {CorrelationId= 5, DateTime= DateTime.Parse("09/09/2023 14:18:00"), ContaOrigem= 238596054, ContaDestino= 674038564, Valor= 1489},
                new Transacao {CorrelationId= 6, DateTime= DateTime.Parse("09/09/2023 14:18:20"), ContaOrigem= 573659065, ContaDestino= 563856300, Valor= 49},
                new Transacao {CorrelationId= 7, DateTime= DateTime.Parse("09/09/2023 14:19:00"), ContaOrigem= 938485762, ContaDestino= 2147483649, Valor= 44},
                new Transacao {CorrelationId= 8, DateTime= DateTime.Parse("09/09/2023 14:19:01"), ContaOrigem= 573659065, ContaDestino= 675869708, Valor= 150}
            };

            var repository = new ContaRepository();
            var executor = new TransacaoService(repository);

            IOrderedEnumerable<Transacao> transacoesOrdenadasPorCronologia = TRANSACOES.OrderBy(TRANSACOES => TRANSACOES.DateTime);

            foreach (var transacao in transacoesOrdenadasPorCronologia)
            {
                executor.ExecutarTransferencia(
                    transacao.CorrelationId,
                    transacao.ContaOrigem,
                    transacao.ContaDestino,
                    transacao.Valor
                );
            }
        }
    }
}

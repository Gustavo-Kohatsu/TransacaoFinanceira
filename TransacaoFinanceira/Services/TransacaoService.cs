using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransacaoFinanceira.Main.Repositories;
using TransacaoFinanceira.Main.Repositories.Interfaces;

namespace TransacaoFinanceira.Main.Services
{
    public class TransacaoService
    {
        private readonly IContaRepository _repository;

        public TransacaoService(IContaRepository repository)
        {
            _repository = repository;
        }

        public bool ExecutarTransferencia(int correlationId, long contaOrigem, long contaDestino, decimal valor)
        {

            var origem = _repository.GetSaldo(contaOrigem);
            if (origem == null)
            {
                throw new ArgumentException("Conta de origem não encontrada.");
            }

            if (!origem.Debitar(valor))
            {
                Console.WriteLine("Transacao número {0} foi cancelada por falta de saldo", correlationId);
                return false;
            }

            var destino = _repository.GetSaldo(contaDestino);
            if (destino == null)
            {
                throw new ArgumentException("Conta de destino não encontrada.");
            }

            destino.Creditar(valor);
            Console.WriteLine("Transacao número {0} foi efetivada com sucesso! Novos saldos: Conta Origem: {1} | Conta Destino: {2}",
                              correlationId, origem.Saldo, destino.Saldo);
            return true;
        }
    }
}

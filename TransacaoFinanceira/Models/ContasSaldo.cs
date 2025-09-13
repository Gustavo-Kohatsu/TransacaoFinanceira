using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransacaoFinanceira.Main.Models
{
    public class ContasSaldo
    {

        public ContasSaldo(long conta, decimal saldoInicial)
        {
            Conta = conta;
            Saldo = saldoInicial;
        }

        public bool Debitar(decimal valor)
        {
            if (VerificarSaldoInsuficiente(valor)) return false;
            Saldo -= valor;
            return true;
        }

        public bool VerificarSaldoInsuficiente(decimal valor)
        {
            return Saldo < valor;
        }

        public void Creditar(decimal valor)
        {
            Saldo += valor;
        }

        public long Conta { get; set; }
        public decimal Saldo { get; set; }
    }
}

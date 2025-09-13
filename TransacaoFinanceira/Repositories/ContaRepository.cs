using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransacaoFinanceira.Main.Models;
using TransacaoFinanceira.Main.Repositories.Interfaces;

namespace TransacaoFinanceira.Main.Repositories
{
    public class ContaRepository : IContaRepository
    {

        private readonly List<ContasSaldo> _tabelaSaldos;

        public ContaRepository()
        {
            _tabelaSaldos = new() {
                new ContasSaldo(938485762, 180),
                new ContasSaldo(347586970, 1200),
                new ContasSaldo(2147483649, 0),
                new ContasSaldo(675869708, 4900),
                new ContasSaldo(238596054, 478),
                new ContasSaldo(573659065, 787),
                new ContasSaldo(210385733, 10),
                new ContasSaldo(674038564, 400),
                new ContasSaldo(563856300, 1200)
            };
        }

        public ContasSaldo GetSaldo(long id)
        {
            return _tabelaSaldos.Find(contaSaldo => contaSaldo.Conta == id);
        }

        public bool Atualizar(ContasSaldo atualizado)
        {

            try
            {
                ContasSaldo item = atualizado as ContasSaldo;
                _tabelaSaldos.RemoveAll(contaSaldo => contaSaldo.Conta == item.Conta);
                _tabelaSaldos.Add(atualizado as ContasSaldo);
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }

        }
    }
}

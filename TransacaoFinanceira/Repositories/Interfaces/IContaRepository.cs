using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransacaoFinanceira.Main.Models;

namespace TransacaoFinanceira.Main.Repositories.Interfaces
{
    public interface IContaRepository
    {
        ContasSaldo GetSaldo(long contaId);
        bool Atualizar(ContasSaldo conta);
    }
}

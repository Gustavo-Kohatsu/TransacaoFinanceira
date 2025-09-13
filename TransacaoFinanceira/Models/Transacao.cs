using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransacaoFinanceira.Main.Models
{
    public class Transacao
    {
        public int CorrelationId { get; set; }
        public DateTime DateTime { get; set; }
        public long ContaOrigem { get; set; }
        public long ContaDestino { get; set; }
        public decimal Valor { get; set; }
    }
}

using Modelo.Clientes;
using System.Collections.Generic;
using System.Linq;

namespace Modelo.Pedidos
{
    public class Pedido
    {
        public long PedidoID { get; set; }

        public string Descricao { get; set; }

        public decimal ValorTotal { get { return ValorTotal; } set { ValorTotal = CalcularValorTotal(); } }

        public virtual ICollection<PedidoItem> Itens { get; set; }

        public long ClienteID { get; set; }
        public virtual Cliente Cliente { get; set; }

        private decimal CalcularValorTotal()
        {
            var subtotais = Itens.Select(x => x.Subtotal);
            decimal valorTotal = 0;
            foreach (var subtotal in subtotais)
            {
                valorTotal += subtotal;
            }
            return valorTotal;
        }
    }
}
using Modelo.Cadastros;
using System.ComponentModel.DataAnnotations;

namespace Modelo.Pedidos
{
    public class PedidoItem
    {
        [Key]
        public long PedidoItemID { get; set; }

        public int Quantidade { get; set; }
        public decimal Subtotal { get { return Quantidade * (decimal)Produto.ValorUnitario; } }

        public long ProdutoID { get; set; }
        public virtual Produto Produto { get; set; }

        public long PedidoID { get; set; }
        public virtual Pedido Pedido { get; set; }
    }
}
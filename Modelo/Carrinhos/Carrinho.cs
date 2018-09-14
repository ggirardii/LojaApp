using Modelo.Clientes;
using System.Collections.Generic;

namespace Modelo.Carrinhos
{
    public class Carrinho
    {
        public long? CarrinhoID { get; set; }
        public virtual ICollection<ItemCarrinho> ItemCarrinho { get; set; }
    }
}
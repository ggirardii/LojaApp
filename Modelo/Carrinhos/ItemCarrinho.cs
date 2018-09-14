using Modelo.Cadastros;

namespace Modelo.Carrinhos
{
    public class ItemCarrinho
    {
        public long? ItemCarrinhoID { get; set; }
        public Produto Produto { get; set; }
        public int Quantidade { get; set; }
        public decimal ValorUnitario { get; set; }
        public decimal SubTotal { get { return Quantidade * ValorUnitario; } }
    }
}
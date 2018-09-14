using Modelo.Cadastros;
using System.Collections;
using System.Linq;

namespace Modelo.Contratos
{
    public interface IProdutoRepositorio
    {
        IQueryable<Produto> ObterProdutosClassificadosPorNome();

        IQueryable<Produto> ObterProdutosFabricantesCategorias();

        IList ObterProdutosPorNome(string param);
    }
}
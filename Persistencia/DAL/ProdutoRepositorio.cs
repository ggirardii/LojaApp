using Modelo.Cadastros;
using Modelo.Contratos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using System.Data.Entity;

namespace Persistencia.DAL
{
    public class ProdutoRepositorio : RepositorioBase<Produto>, IProdutoRepositorio
    {
        public IQueryable<Produto> ObterProdutosClassificadosPorNome()
        {
            return context.Produtos.OrderBy(p => p.Nome);
        }

        public IQueryable<Produto> ObterProdutosFabricantesCategorias()
        {
            return context.Produtos.Include(c => c.Categoria).Include(f => f.Fabricante).OrderBy(n => n.Nome);
        }

        public IList ObterProdutosPorNome(string param)
        {
            var r = from produto in context.Produtos
                    where produto.Nome.ToUpper().StartsWith(param.ToUpper())
                    orderby (produto.Nome)
                    select new
                    {
                        id = produto.ProdutoID,
                        label = produto.Nome,
                        value = produto.Nome
                    };
            return r.ToList();
        }
    }
}
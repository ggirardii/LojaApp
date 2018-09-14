using Modelo.Tabelas;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistencia.DAL
{
    public class CategoriaRepositorio : RepositorioBase<Categoria>
    {
        public IQueryable<Categoria> ObterCategoriasClassificadasPorNome() => context.Categorias.OrderBy(c => c.Nome);

        public Categoria ObterCategoriaComProdutosFabricantes(long id) => context.Categorias.Where(x => x.CategoriaID == id).Include("Produtos.Fabricante").FirstOrDefault();
    }
}
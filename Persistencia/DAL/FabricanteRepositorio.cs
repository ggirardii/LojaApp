using Modelo.Cadastros;
using Modelo.Contratos;
using System.Data.Entity;
using System.Linq;

namespace Persistencia.DAL
{
    public class FabricanteRepositorio : RepositorioBase<Fabricante>, IFabricanteRepositorio
    {
        public IQueryable<Fabricante> ObterFabricantesClassificadosPorNome() => context.Fabricantes.OrderBy(f => f.Nome);

        public Fabricante ObterFabricanteComProdutosCategorias(long id) => context.Fabricantes.Where(x => x.FabricanteID == id).Include("Produtos.Categoria").FirstOrDefault();
    }
}
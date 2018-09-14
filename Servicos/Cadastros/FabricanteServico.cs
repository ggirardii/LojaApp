using System.Linq;
using Modelo.Cadastros;
using Persistencia.DAL;

namespace Servicos.Cadastros
{
    public class FabricanteServico : IServico<Fabricante>
    {
        private FabricanteRepositorio _fabricanteRepositorio = new FabricanteRepositorio();

        public IQueryable<Fabricante> ObterFabricantePeloNome() => _fabricanteRepositorio.ObterFabricantesClassificadosPorNome();

        public Fabricante ObterFabricanteComProdutosCategorias(long id) => _fabricanteRepositorio.ObterFabricanteComProdutosCategorias(id);

        public void GravarFabricante(Fabricante fabricante)
        {
            if (fabricante.FabricanteID == null)
            {
                Inserir(fabricante);
            }
            else
            {
                Atualizar(fabricante);
            }
        }

        public void Inserir(Fabricante entidade)
        {
            _fabricanteRepositorio.Inserir(entidade);
            Salvar();
        }

        public void Atualizar(Fabricante entidade)
        {
            _fabricanteRepositorio.Atualizar(entidade);
            Salvar();
        }

        public void Deletar(Fabricante entidade)
        {
            _fabricanteRepositorio.Deletar(entidade);
            Salvar();
        }

        public Fabricante BuscarPorId(long id) => _fabricanteRepositorio.BuscarPorId(id);

        public IQueryable<Fabricante> BuscarTodos() => _fabricanteRepositorio.BuscarTodos();

        public void Salvar() => _fabricanteRepositorio.Salvar();
    }
}
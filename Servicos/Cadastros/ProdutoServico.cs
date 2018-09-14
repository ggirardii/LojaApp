using System.Linq;
using Modelo.Cadastros;
using System.Collections;
using Persistencia.DAL;

namespace Servicos.Cadastros
{
    public class ProdutoServico : IServico<Produto>
    {
        private ProdutoRepositorio _produtoRepositorio = new ProdutoRepositorio();

        public IQueryable<Produto> ObterProdutosFabricantesCategorias() => _produtoRepositorio.ObterProdutosFabricantesCategorias();

        public void GravarProduto(Produto produto)
        {
            if (produto.ProdutoID == null)
            {
                Inserir(produto);
            }
            else
            {
                Atualizar(produto);
            }
        }

        public void Inserir(Produto entidade)
        {
            _produtoRepositorio.Inserir(entidade);
            Salvar();
        }

        public void Atualizar(Produto entidade)
        {
            _produtoRepositorio.Atualizar(entidade);
            Salvar();
        }

        public void Deletar(Produto entidade)
        {
            _produtoRepositorio.Deletar(entidade);
            Salvar();
        }

        public Produto BuscarPorId(long id) => _produtoRepositorio.BuscarPorId(id);

        public IQueryable<Produto> BuscarTodos() => _produtoRepositorio.BuscarTodos();

        public void Salvar() => _produtoRepositorio.Salvar();

        //--------------------OBTER PRODUTOS POR NOME PARA COMBOBOX ---------------------
        public IList ObterProdutosPorNome(string param) => _produtoRepositorio.ObterProdutosPorNome(param);
    }
}
using System;
using System.Linq;
using Modelo.Tabelas;
using Persistencia.DAL;

namespace Servicos.Tabelas
{
    public class CategoriaServico : IServico<Categoria>
    {
        private CategoriaRepositorio _categoriaRepositorio = new CategoriaRepositorio();

        public IQueryable<Categoria> ObterCategoriasClassificadasPorNome() => _categoriaRepositorio.ObterCategoriasClassificadasPorNome();

        public Categoria ObterCategoriaComProdutosFabricantes(long id) => _categoriaRepositorio.ObterCategoriaComProdutosFabricantes(id);

        public Categoria BuscarPorId(long id)
        {
            throw new NotImplementedException();
        }

        public IQueryable<Categoria> BuscarTodos()
        {
            throw new NotImplementedException();
        }

        public void Inserir(Categoria entidade)
        {
            _categoriaRepositorio.Inserir(entidade);
            Salvar();
        }

        public void Atualizar(Categoria entidade)
        {
            _categoriaRepositorio.Atualizar(entidade);
            Salvar();
        }

        public void Deletar(Categoria entidade)
        {
            _categoriaRepositorio.Deletar(entidade);
            Salvar();
        }

        public void Salvar() => _categoriaRepositorio.Salvar();
    }
}
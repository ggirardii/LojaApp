using Modelo;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace Persistencia.DAL
{
    public interface IRepositorioBase<TEntidade> where TEntidade : class
    {
        void Inserir(TEntidade entidade);

        void Atualizar(TEntidade entidade);

        void Deletar(TEntidade entidade);

        TEntidade BuscarPorId(long id);

        IQueryable<TEntidade> BuscarTodos();

        IQueryable<TEntidade> Filtrar(Expression<Func<TEntidade, bool>> predicate);

        void Salvar();
    }
}
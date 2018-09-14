using Persistencia.Contexts;
using System;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace Persistencia.DAL
{
    public class RepositorioBase<TEntidade> : IRepositorioBase<TEntidade> where TEntidade : class
    {
        public EFContext context = new EFContext();

        public void Inserir(TEntidade entidade)
        {
            context.Set<TEntidade>().Add(entidade);
        }

        public void Atualizar(TEntidade entidade)
        {
            context.Entry(entidade).State = EntityState.Modified;
        }

        public void Deletar(TEntidade entidade)
        {
            context.Entry(entidade).State = EntityState.Deleted;
        }

        public TEntidade BuscarPorId(long id)
        {
            return context.Set<TEntidade>().Find(id);
        }

        public IQueryable<TEntidade> BuscarTodos()
        {
            return context.Set<TEntidade>().AsNoTracking().AsQueryable();
        }

        public IQueryable<TEntidade> Filtrar(Expression<Func<TEntidade, bool>> predicate)
        {
            return context.Set<TEntidade>().Where(predicate);
        }

        public void Salvar()
        {
            context.SaveChanges();
        }
    }
}
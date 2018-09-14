using System.Linq;

namespace Servicos
{
    public interface IServico<TEntidade> where TEntidade : class
    {
        void Inserir(TEntidade entidade);

        void Atualizar(TEntidade entidade);

        void Deletar(TEntidade entidade);

        TEntidade BuscarPorId(long id);

        IQueryable<TEntidade> BuscarTodos();

        void Salvar();
    }
}
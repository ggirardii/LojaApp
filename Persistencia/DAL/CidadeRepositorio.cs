using Modelo.Tabelas;
using System.Linq;
using System.Data.Entity;

namespace Persistencia.DAL
{
    public class CidadeRepositorio : RepositorioBase<Cidade>
    {
        public IQueryable<Cidade> ObterCidadePorEstado(long? estadoId) => context.Cidades.Where(c => c.EstadoID == estadoId).OrderBy(n => n.Nome);
    }
}
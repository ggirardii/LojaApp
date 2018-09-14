using Modelo.Tabelas;
using System.Linq;

namespace Persistencia.DAL
{
    public class EstadoRepositorio : RepositorioBase<Estado>
    {
        public IQueryable<Estado> ObterEstadosClassificadosPorNome() => context.Estados.OrderBy(n => n.Nome);
    }
}
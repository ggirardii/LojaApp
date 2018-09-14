using Modelo.Tabelas;
using Persistencia.DAL;
using System.Linq;

namespace Servicos.Tabelas
{
    public class CidadeServico
    {
        private CidadeRepositorio _cidadeRepositorio = new CidadeRepositorio();

        public IQueryable<Cidade> ObterCidadePorEstado(long? estadoID)
        {
            return _cidadeRepositorio.ObterCidadePorEstado(estadoID);
        }
    }
}
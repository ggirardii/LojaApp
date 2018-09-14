using Modelo.Tabelas;
using Persistencia.DAL;
using System;
using System.Linq;

namespace Servicos.Tabelas
{
    public class EstadoServico
    {
        private EstadoRepositorio _estadoRepositorio = new EstadoRepositorio();

        public IQueryable<Estado> ObterEstadosClassificadosPorNome()
        {
            return _estadoRepositorio.ObterEstadosClassificadosPorNome();
        }
    }
}
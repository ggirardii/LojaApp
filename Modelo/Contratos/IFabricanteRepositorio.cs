using Modelo.Cadastros;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo.Contratos
{
    public interface IFabricanteRepositorio
    {
        IQueryable<Fabricante> ObterFabricantesClassificadosPorNome();
    }
}
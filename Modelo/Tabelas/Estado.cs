using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo.Tabelas
{
    public class Estado
    {
        public long? EstadoID { get; set; }
        public string UF { get; set; }
        public string Nome { get; set; }
        public virtual ICollection<Cidade> Cidades { get; set; }
    }
}
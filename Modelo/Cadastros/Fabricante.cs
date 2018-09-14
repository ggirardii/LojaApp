using Modelo.Tabelas;
using System.Collections.Generic;

namespace Modelo.Cadastros
{
    public class Fabricante
    {
        public long? FabricanteID { get; set; }
        public string Nome { get; set; }
        public string TipoPessoa { get; set; }
        public bool EstaAtivo { get; set; }
        public long? EstadoID { get; set; }
        public long? CidadeID { get; set; }

        public virtual ICollection<Produto> Produtos { get; set; }
        public virtual Estado Estado { get; set; }
        public virtual Cidade Cidade { get; set; }
    }
}
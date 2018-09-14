using Modelo.Tabelas;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;
using System.Web;

namespace Modelo.Cadastros
{
    public class Produto
    {
        [DisplayName("ID")]
        public long? ProdutoID { get; set; }

        [StringLength(100, ErrorMessage = "O nome do prduto precisa ter no minimo 5 caracteres", MinimumLength = 5)]
        [Required(ErrorMessage = "Informe o nome do Produto")]
        public string Nome { get; set; }

        [DisplayName("Data de Cadastro")]
        [Required(ErrorMessage = "Informe a data de cadastro do produto")]
        public DateTime? DataCadastro { get; set; }

        public decimal ValorUnitario { get; set; }

        [DisplayName("Categoria")]
        public long? CategoriaID { get; set; }

        [DisplayName("Fabricante")]
        public long? FabricanteID { get; set; }

        public virtual Categoria Categoria { get; set; }

        public virtual Fabricante Fabricante { get; set; }

        //------------------------ IMAGEM PRODUTO -----------------------------
        public string LogotipoMimeType { get; set; }

        public byte[] Logotipo { get; set; }
        public string NomeArquivo { get; set; }
        public long TamanhoArquivo { get; set; }

        [NotMapped]
        public List<HttpPostedFileBase> Files { get; set; }

        //    public Produto()
        //    {
        //        Files = new List<HttpPostedFileBase>();
        //    }
    }
}
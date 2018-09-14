using Modelo.Autenticacao;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Projeto01.Areas.Seguranca.Models
{
    public class SegurancaModelViews
    {
        public class PapelEditModel
        {
            public Papel Papel { get; set; }
            public IEnumerable<Usuario> Membros { get; set; }
            public IEnumerable<Usuario> NaoMembros { get; set; }
        }

        public class PapelModificationModel
        {
            [Required]
            public string NomePapel { get; set; }

            public string[] IdsParaAdicionar { get; set; }
            public string[] IdsParaRemover { get; set; }
        }
    }
}
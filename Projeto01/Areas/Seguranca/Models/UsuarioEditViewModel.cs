using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Projeto01.Areas.Seguranca.Models
{
    public class UsuarioEditViewModel
    {
        public string Id { get; set; }

        [Required]
        public string Nome { get; set; }

        [Required]
        public string Email { get; set; }

        public string Senha { get; set; }
        public string CompararSenha { get; set; }
    }
}
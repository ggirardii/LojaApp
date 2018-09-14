using System.ComponentModel.DataAnnotations;

namespace Projeto01.Areas.Seguranca.Models
{
    public class UsuarioCadastroViewModel
    {
        public string Id { get; set; }

        [Required]
        public string Nome { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Senha { get; set; }

        [Required, Compare("Senha", ErrorMessage = "A confirmação de senha precisa ser igual.")]
        public string CompararSenha { get; set; }
    }
}
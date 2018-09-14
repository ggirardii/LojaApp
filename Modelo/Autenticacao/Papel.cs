using Microsoft.AspNet.Identity.EntityFramework;

namespace Modelo.Autenticacao
{
    public class Papel : IdentityRole
    {
        public Papel() : base()
        {
        }

        public Papel(string name) : base(name)
        {
        }
    }
}
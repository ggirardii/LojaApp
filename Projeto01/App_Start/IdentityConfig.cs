using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security.Cookies;
using Owin;
using Persistencia.DAL.Usuarios;
using Persistencia.DAL;

namespace Projeto01
{
    public class IdentityConfig
    {
        public void Configuration(IAppBuilder app)
        {
            app.CreatePerOwinContext(IdentityDbContextAplicacao.Create);
            app.CreatePerOwinContext<GerenciadorUsuario>(GerenciadorUsuario.Create);
            app.UseCookieAuthentication(new CookieAuthenticationOptions { AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie, LoginPath = new Microsoft.Owin.PathString("/Seguranca/Account/Login") });
            app.CreatePerOwinContext<GerenciadorPapel>(GerenciadorPapel.Create);
        }
    }
}
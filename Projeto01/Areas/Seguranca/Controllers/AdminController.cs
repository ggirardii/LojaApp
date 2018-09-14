using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Projeto01.Areas.Seguranca.Models;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Modelo.Autenticacao;
using Persistencia.DAL.Usuarios;

namespace Projeto01.Areas.Seguranca.Controllers
{
    public class AdminController : Controller
    {
        [Authorize(Roles = "Administradores")]
        public ActionResult Index()
        {
            return View(_gerenciadorUsuario.Users);
        }

        //----------------------GET E POST : CREATE -------------------

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(UsuarioCadastroViewModel model)
        {
            if (ModelState.IsValid)
            {
                Usuario user = new Usuario { UserName = model.Nome, Email = model.Email };
                IdentityResult result = _gerenciadorUsuario.Create(user, model.Senha);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    AddErrorsFromResult(result);
                }
            }
            return View(model);
        }

        private void AddErrorsFromResult(IdentityResult result)
        {
            foreach (string error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        //---------------------------GET E POST : EDIT -----------------------------

        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Usuario usuario = _gerenciadorUsuario.FindById(id);

            if (usuario == null)
            {
                return HttpNotFound();
            }
            var uevm = new UsuarioEditViewModel();
            uevm.Id = usuario.Id;
            uevm.Nome = usuario.UserName;
            uevm.Email = usuario.Email;
            return View(uevm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(UsuarioEditViewModel uevm)
        {
            if (ModelState.IsValid)
            {
                Usuario usuario = _gerenciadorUsuario.FindById(uevm.Id);
                usuario.UserName = uevm.Nome;
                usuario.Email = uevm.Email;
                if ((uevm.Senha == uevm.CompararSenha) && ((uevm.Senha != null) && (uevm.CompararSenha != null)))
                {
                    usuario.PasswordHash = _gerenciadorUsuario.PasswordHasher.HashPassword(uevm.Senha);
                }
                else
                {
                    usuario.PasswordHash = usuario.PasswordHash;
                }

                IdentityResult result = _gerenciadorUsuario.Update(usuario);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    AddErrorsFromResult(result);
                }
            }
            return View(uevm);
        }

        //-------------------------GET E POST : DELETE -----------------------------

        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Usuario usuario = _gerenciadorUsuario.FindById(id);
            if (usuario == null)
            {
                return HttpNotFound();
            }
            return View(usuario);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(Usuario usuario)
        {
            Usuario user = _gerenciadorUsuario.FindById(usuario.Id);
            if (user != null)
            {
                IdentityResult result = _gerenciadorUsuario.Delete(user);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
            }
            else
            {
                return HttpNotFound();
            }
        }

        //------------------------- GET : DETAILS ---------------------------

        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Usuario usuario = _gerenciadorUsuario.FindById(id);
            if (usuario == null)
            {
                return HttpNotFound();
            }
            return View(usuario);
        }

        //----------------------------------AUXILIAR----------------------
        private GerenciadorUsuario _gerenciadorUsuario
        {
            get
            {
                return HttpContext.GetOwinContext().GetUserManager<GerenciadorUsuario>();
            }
        }

        public UsuarioCadastroViewModel GerarVisaoPorId(Usuario usuario)
        {
            var uvm = new UsuarioCadastroViewModel();
            uvm.Id = usuario.Id;
            uvm.Nome = usuario.UserName;
            uvm.Email = usuario.Email;
            return (uvm);
        }

        public IdentityResult SalvarAlteracoes(UsuarioCadastroViewModel uvm)
        {
            Usuario usuario = _gerenciadorUsuario.FindById(uvm.Id);
            usuario.UserName = uvm.Nome;
            usuario.Email = uvm.Email;
            usuario.PasswordHash = _gerenciadorUsuario.PasswordHasher.HashPassword(uvm.Senha);
            IdentityResult result = _gerenciadorUsuario.Update(usuario);
            return (result);
        }
    }
}
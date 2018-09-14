using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static Projeto01.Areas.Seguranca.Models.SegurancaModelViews;
using System.Net;
using Modelo.Autenticacao;
using Persistencia.DAL.Usuarios;

namespace Projeto01.Areas.Seguranca.Controllers
{
    [Authorize(Roles = "Administradores")]
    public class PapelAdminController : Controller
    {
        // GET: Seguranca/PapelAdmin
        public ActionResult Index()
        {
            return View(RoleManager.Roles);
        }

        //-----------------------GET E POST: CREATE --------------------------

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Required]string nome)
        {
            if (ModelState.IsValid)
            {
                IdentityResult result = RoleManager.Create(new Papel(nome));
                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    AddErrorsFromResult(result);
                }
            }
            return View(nome);
        }

        private void AddErrorsFromResult(IdentityResult result)
        {
            foreach (string error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        private GerenciadorUsuario UserManager
        {
            get
            {
                return HttpContext.GetOwinContext().GetUserManager<GerenciadorUsuario>();
            }
        }

        //--------------------------- GET E POST: EDIT ---------------------------------

        public ActionResult Edit(string id)
        {
            var papel = RoleManager.FindById(id);
            var memberIDs = papel.Users.Select(x => x.UserId).ToArray();
            var membros = UserManager.Users.Where(x => memberIDs.Any(y => y == x.Id));
            var naoMembros = UserManager.Users.Except(membros);
            return View(new PapelEditModel
            {
                Papel = papel,
                Membros = membros,
                NaoMembros = naoMembros
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(PapelModificationModel model)
        {
            IdentityResult result;
            if (ModelState.IsValid)
            {
                foreach (string userId in model.IdsParaAdicionar ?? new string[] { })
                {
                    result = UserManager.AddToRole(userId, model.NomePapel);
                    if (!result.Succeeded)
                    {
                        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                    }
                }
                foreach (string useriD in model.IdsParaRemover ?? new string[] { })
                {
                    result = UserManager.RemoveFromRole(useriD, model.NomePapel);
                    if (!result.Succeeded)
                    {
                        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                    }
                }
                return RedirectToAction("Index");
            }
            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        }

        //-----------------------------------GET E POST: DELETE -----------------------------

        public ActionResult Delete(string Id)
        {
            if (Id == null || Id == "")
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var papel = RoleManager.FindById(Id);
            if (papel == null)
            {
                return new HttpNotFoundResult();
            }

            var memberIDs = papel.Users.Select(x => x.UserId).ToArray();
            var membros = UserManager.Users.Where(x => memberIDs.Any(y => y == x.Id));
            var naoMembros = UserManager.Users.Except(membros);

            var papelModel = new PapelEditModel()
            {
                Papel = papel,
                Membros = membros,
                NaoMembros = naoMembros
            };

            return View(papelModel);
        }

        //----------------------------- AUXILIAR ------------------------------------------
        private GerenciadorPapel RoleManager
        {
            get
            {
                return HttpContext.GetOwinContext().GetUserManager<GerenciadorPapel>();
            }
        }
    }
}
using Modelo.Cadastros;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using Persistencia.Contexts;
using Servicos.Tabelas;
using Servicos.Cadastros;

namespace Projeto01.Areas.Cadastros.Controllers
{
    public class FabricantesController : Controller
    {
        private FabricanteServico _fabricanteServico = new FabricanteServico();
        private EstadoServico _estadoServico = new EstadoServico();
        private CidadeServico _cidadeServico = new CidadeServico();

        //--------------------INDEX------------------------------------
        public ActionResult Index()
        {
            return View(_fabricanteServico.ObterFabricantePeloNome());
        }

        //-------------------------- GET E POST: CREATE --------------------------
        public ActionResult Create()
        {
            ViewBag.EstadoID = new SelectList(_estadoServico.ObterEstadosClassificadosPorNome(), "EstadoID", "Nome");
            ViewBag.CidadeID = new SelectList(_cidadeServico.ObterCidadePorEstado(null), "CidadeID", "Nome");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Fabricante fabricante)
        {
            if (ModelState.IsValid)
            {
                _fabricanteServico.GravarFabricante(fabricante);
                return RedirectToAction("Index");
            }
            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        }

        //------------------------------GET E POST: EDIT -----------------------------------
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Fabricante fabricante = _fabricanteServico.BuscarPorId((long)id);

            if (fabricante == null)
            {
                return new HttpNotFoundResult();
            }
            ViewBag.EstadoID = new SelectList(_estadoServico.ObterEstadosClassificadosPorNome(), "EstadoID", "Nome", fabricante.EstadoID);
            ViewBag.CidadeID = new SelectList(_cidadeServico.ObterCidadePorEstado(fabricante.EstadoID), "CidadeID", "Nome", fabricante.CidadeID);

            return View(fabricante);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Fabricante fabricante)
        {
            if (ModelState.IsValid)
            {
                _fabricanteServico.GravarFabricante(fabricante);
                return RedirectToAction("Index");
            }
            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        }

        //----------------------------------- GET: DETAILS -----------------------------
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Fabricante fabricante = _fabricanteServico.ObterFabricanteComProdutosCategorias((long)id);

            if (fabricante == null)
            {
                return new HttpNotFoundResult();
            }

            return View(fabricante);
        }

        //----------------------------------- GET E POST : DELETE --------------------------------
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Fabricante fabricante = _fabricanteServico.ObterFabricanteComProdutosCategorias((long)id);

            if (fabricante == null)
            {
                return new HttpNotFoundResult();
            }

            return View(fabricante);
        }

        //POST: fabricante/delete/(id)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(long id)
        {
            Fabricante fabricante = _fabricanteServico.ObterFabricanteComProdutosCategorias(id);
            if (fabricante.Produtos.Where(x => x.FabricanteID == id).Count() > 0)
            {
                TempData["Message"] = "Fabricante " + fabricante.Nome.ToUpper() + " não pode ser removido pois possui produtos cadastrados!";
                return RedirectToAction("Delete", new { @id = id });
            }
            else
            {
                _fabricanteServico.Deletar(fabricante);
                TempData["Message"] = "Fabricante " + fabricante.Nome.ToUpper() + " foi removido";
                return RedirectToAction("index");
            }
        }
    }
}
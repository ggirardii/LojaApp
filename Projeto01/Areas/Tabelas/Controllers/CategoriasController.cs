using Modelo.Tabelas;
using System.Web.Mvc;
using System.Linq;
using System.Net;
using Servicos.Tabelas;

namespace Projeto01.Areas.Tabelas.Controllers
{
    public class CategoriasController : Controller
    {
        private CategoriaServico _categoriaServico = new CategoriaServico();

        // GET: Categorias
        public ActionResult Index()
        {
            return View(_categoriaServico.ObterCategoriasClassificadasPorNome().ToList());
        }

        //-----------------------------GET E POST: CREATE ------------------------------
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Categoria categoria)
        {
            if (ModelState.IsValid)
            {
                _categoriaServico.Inserir(categoria);
            }
            else
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            return RedirectToAction("index");
        }

        //---------------------------------------------- GET E POST: EDIT -------------------------
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Categoria categoria = _categoriaServico.ObterCategoriaComProdutosFabricantes((long)id);

            if (categoria == null)
            {
                return new HttpNotFoundResult();
            }

            return View(categoria);
        }

        [HttpPost]
        public ActionResult Edit(Categoria categoria)
        {
            if (ModelState.IsValid)
            {
                _categoriaServico.Atualizar(categoria);
                return RedirectToAction("index");
            }
            else
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
        }

        //-------------------------------------GET DETAILS -----------------------------------
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Categoria categoria = _categoriaServico.ObterCategoriaComProdutosFabricantes((long)id);

            if (categoria == null)
            {
                return new HttpNotFoundResult();
            }

            return View(categoria);
        }

        //-----------------------------------GET E POST DELETE ------------------------------
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Categoria categoria = _categoriaServico.ObterCategoriaComProdutosFabricantes((long)id);

            if (categoria == null)
            {
                return new HttpNotFoundResult();
            }

            return View(categoria);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(long id)
        {
            Categoria categoria = _categoriaServico.ObterCategoriaComProdutosFabricantes(id);
            if (categoria.Produtos.Where(x => x.CategoriaID == id).Count() > 0)
            {
                TempData["Message"] = "Categoria " + categoria.Nome.ToUpper() + " não pode ser removida pois possui produtos cadastrados!";
                return RedirectToAction("Delete", new { @id = id });
            }
            else
            {
                _categoriaServico.Deletar(categoria);
                TempData["Message"] = "Categoria " + categoria.Nome.ToUpper() + " foi removida";
            }

            return RedirectToAction("index");
        }
    }
}
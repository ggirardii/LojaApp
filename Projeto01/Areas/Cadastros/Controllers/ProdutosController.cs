using System.Web.Mvc;
using Modelo.Cadastros;
using System.Net;
using Servicos.Cadastros;
using Servicos.Tabelas;
using System.Linq;
using System.Web;
using System.IO;
using System;
using System.Collections.Generic;

namespace Projeto01.Areas.Cadastros.Controllers
{
    public class ProdutosController : Controller
    {
        private ProdutoServico produtoServico = new ProdutoServico();
        private CategoriaServico categoriaServico = new CategoriaServico();
        private FabricanteServico fabricanteServico = new FabricanteServico();

        //---------------------- GET: INDEX (produtos) ---------------------
        public ActionResult Index()
        {
            return View(produtoServico.ObterProdutosFabricantesCategorias());
        }

        //---------------------GET: /DETAILS ------------------------
        public ActionResult Details(long? id)
        {
            return ObterVisaoProdutoPorID(id);
        }

        //----------------GET E POST: CREATE  --------------------
        public ActionResult Create()
        {
            PopularViewBag();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Produto produto, HttpPostedFileBase logotipo = null, string chkRemoverImagem = null)
        {
            if (produto.Files.Count > 0)
            {
                logotipo = produto.Files[0];
            }
            return GravarProduto(produto, logotipo, chkRemoverImagem);
        }

        //--------------------------GET E POST: EDIT---------------------------
        public ActionResult Edit(long? id)
        {
            if (id != null)
            {
                PopularViewBag(produtoServico.BuscarPorId((long)id));
            }

            return ObterVisaoProdutoPorID(id);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Produto produto, HttpPostedFileBase logotipo, string chkRemoverImagem)
        {
            Produto prodCheck = produtoServico.BuscarPorId((long)produto.ProdutoID);
            prodCheck.Nome = produto.Nome;
            prodCheck.CategoriaID = produto.CategoriaID;
            prodCheck.FabricanteID = produto.FabricanteID;
            prodCheck.ValorUnitario = produto.ValorUnitario;
            prodCheck.DataCadastro = produto.DataCadastro;
            if (produto.Files != null && produto.Files.Count > 0 && produto.Files[0] != null)
            {
                logotipo = produto.Files[0];
                return GravarProduto(prodCheck, logotipo, chkRemoverImagem);
            }
            produtoServico.GravarProduto(prodCheck);
            return RedirectToAction("Index");
        }

        //--------------------------GET E POST: DELETE ------------------------
        public ActionResult Delete(long? id)
        {
            return ObterVisaoProdutoPorID(id);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(long id)
        {
            Produto produto = produtoServico.BuscarPorId(id);
            produtoServico.Deletar(produto);
            TempData["Message"] = "Produto " + produto.Nome.ToUpper() + " foi deletado";
            return RedirectToAction("Index");
        }

        //----------------------GET JSON COMBOBOX ----------------------------
        public JsonResult ObterProdutosPorNome(string param)
        {
            var r = produtoServico.ObterProdutosPorNome(param);
            return Json(r, JsonRequestBehavior.AllowGet);
        }

        //-------------------------------METODOS--------------------------
        //----------------------------------------------------------------

        //----------------------- OBTER UMA VIEW POR ID DO PRODUTO---------------------
        private ActionResult ObterVisaoProdutoPorID(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Produto produto = produtoServico.BuscarPorId((long)id);

            if (produto == null)
            {
                return HttpNotFound();
            }
            return View(produto);
        }

        //--------------- OBTER UMA VIEW PASSANDO TAMBEM DADOS DE FABRICANTES E CATEGORIAS -------------
        private void PopularViewBag(Produto produto = null)
        {
            if (produto == null)
            {
                ViewBag.CategoriaID = new SelectList(categoriaServico.ObterCategoriasClassificadasPorNome(), "CategoriaID", "Nome");
                ViewBag.FabricanteID = new SelectList(fabricanteServico.ObterFabricantePeloNome(), "FabricanteID", "Nome");
            }
            else
            {
                ViewBag.CategoriaID = new SelectList(categoriaServico.ObterCategoriasClassificadasPorNome(), "CategoriaID", "Nome", produto.CategoriaID);
                ViewBag.FabricanteID = new SelectList(fabricanteServico.ObterFabricantePeloNome().ToList(), "FabricanteID", "Nome", produto.FabricanteID);
            }
        }

        //-------------------METODO POST PARA GRAVAR UM PRODUTO-----------------------------
        private ActionResult GravarProduto(Produto produto, HttpPostedFileBase logotipo, string chkRemoverImagem)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (chkRemoverImagem != null)
                    {
                        produto.Logotipo = null;
                    }
                    if (logotipo != null)
                    {
                        produto.LogotipoMimeType = logotipo.ContentType;
                        produto.Logotipo = SetLogotipo(logotipo);
                        produto.NomeArquivo = logotipo.FileName;
                        produto.TamanhoArquivo = logotipo.ContentLength;
                    }
                    produtoServico.GravarProduto(produto);
                    return RedirectToAction("Index");
                }

                PopularViewBag(produto);
                return View(produto);
            }
            catch (Exception e)
            {
                var execption = e;
                return View(produto);
            }
        }

        //--------------------------CHECAR SE TEM UMA IMAGEM ANTES DE GRAVAR --------------------
        private Produto ChecarImagem(Produto produto)
        {
            Produto prodCheck = produtoServico.BuscarPorId((long)produto.ProdutoID);
            if (prodCheck.Logotipo != null)
            {
                produto.Logotipo = prodCheck.Logotipo;
                produto.LogotipoMimeType = prodCheck.LogotipoMimeType;
                produto.NomeArquivo = prodCheck.NomeArquivo;
                produto.TamanhoArquivo = prodCheck.TamanhoArquivo;
            }
            return produto;
        }

        //--------------------------GRAVAR UMA IMAGEM----------------------

        private byte[] SetLogotipo(HttpPostedFileBase logotipo)
        {
            var bytesLogotipo = new byte[logotipo.ContentLength];
            logotipo.InputStream.Read(bytesLogotipo, 0, logotipo.ContentLength);
            return bytesLogotipo;
        }

        public FileContentResult GetLogotipo(long id)
        {
            Produto produto = produtoServico.BuscarPorId(id);
            if (produto != null)
            {
                return File(produto.Logotipo, produto.LogotipoMimeType);
            }
            return null;
        }

        //---------------------------DOWNLOAD IMAGEM ----------------------
        public ActionResult DownloadArquivo(long id)
        {
            Produto produto = produtoServico.BuscarPorId(id);
            FileStream fileStream = new FileStream(Server.MapPath("~/TempData/" + produto.NomeArquivo), FileMode.Create, FileAccess.Write);
            fileStream.Write(produto.Logotipo, 0, Convert.ToInt32(produto.TamanhoArquivo));
            fileStream.Close();
            return File(fileStream.Name, produto.LogotipoMimeType, produto.NomeArquivo);
        }
    }
}
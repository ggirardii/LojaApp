using Modelo.Cadastros;
using Modelo.Carrinhos;
using Projeto01.Areas.Seguranca.Models;
using Servicos.Cadastros;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Projeto01.Areas.Carrinho.Controllers
{
    public class CarrinhosController : Controller
    {
        // GET: Carrinho/Carrinhos
        public ActionResult Create()
        {
            IEnumerable<ItemCarrinho> carrinho = HttpContext.Session["carrinho"] as IEnumerable<ItemCarrinho>;
            if (carrinho == null)
            {
                carrinho = new List<ItemCarrinho>();
                HttpContext.Session["carrinho"] = carrinho;
            }
            return View(carrinho);
        }

        //------------------auxiliar-------------------
        private ProdutoServico produtoServico = new ProdutoServico();

        //-----------------------------------------

        //=--------------------- ADICIONAR PRODUTOS NO CARRINHO ------------------
        [HttpPost, ValidateAntiForgeryToken]
        public PartialViewResult AddProduto(FormCollection collection)
        {
            List<ItemCarrinho> carrinho = HttpContext.Session["carrinho"] as List<ItemCarrinho>;
            var produto = produtoServico.BuscarPorId(Convert.ToInt32(collection.Get("idproduto")));

            ItemCarrinho itemCarrinho = ChecarCarrinho(carrinho, produto);

            //carrinho.Add(itemCarrinho);
            HttpContext.Session["carrinho"] = carrinho;
            return PartialView("_ItensCarrinho", carrinho);
        }

        // faz checagem se o produto ja esxiste no carrinho
        public ItemCarrinho ChecarCarrinho(List<ItemCarrinho> carrinho, Produto produto)
        {
            foreach (var item in carrinho)
            {
                if (produto.Nome == item.Produto.Nome)
                {
                    item.Quantidade = item.Quantidade + 1;
                    return item;
                }
            }
            var itemCarrinho = new ItemCarrinho()
            {
                Produto = produto,
                Quantidade = 1,
                ValorUnitario = produto.ValorUnitario
            };
            carrinho.Add(itemCarrinho);
            return itemCarrinho;
        }
    }
}
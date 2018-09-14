using System.Web.Mvc;
using Modelo.Clientes;
using Microsoft.AspNet.Identity;
using Persistencia.Contexts;
using Servicos.Pedidos;
using Servicos.Cadastros;

namespace Projeto01.Areas.Pedidos.Controllers
{
    public class PedidosController : Controller
    {
        private EFContext context = new EFContext();
        private PedidoServico pedidoServico = new PedidoServico();
        private ClienteServico clienteServico = new ClienteServico();

        [Authorize]
        public ActionResult InfEndereco()
        {
            var usuarioID = User.Identity.GetUserId();
            if (usuarioID != null)
            {
                Cliente cliente = clienteServico.ObterClientePorUsuarioId(usuarioID);
                return View(cliente);
            }
            return View();
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult InfEndereco(Cliente cliente)
        {
            if (ModelState.IsValid)
            {
                clienteServico.GravarCliente(cliente, User.Identity.GetUserId());
            }

            return RedirectToAction("Index");
        }
    }
}
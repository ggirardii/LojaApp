using System;
using System.Linq;
using Modelo.Pedidos;
using Persistencia.DAL;

namespace Servicos.Pedidos
{
    public class PedidoServico : IServico<Pedido>
    {
        private PedidoRepositorio _pedidoRepositorio = new PedidoRepositorio();

        public void Inserir(Pedido entidade)
        {
            _pedidoRepositorio.Inserir(entidade);
            Salvar();
        }

        public void Atualizar(Pedido entidade)
        {
            _pedidoRepositorio.Atualizar(entidade);
            Salvar();
        }

        public void Deletar(Pedido entidade)
        {
            _pedidoRepositorio.Deletar(entidade);
            Salvar();
        }

        public Pedido BuscarPorId(long id) => _pedidoRepositorio.BuscarPorId(id);

        public IQueryable<Pedido> BuscarTodos() => _pedidoRepositorio.BuscarTodos();

        public void Salvar() => _pedidoRepositorio.Salvar();
    }
}
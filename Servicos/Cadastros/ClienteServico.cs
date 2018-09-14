using Modelo.Clientes;
using Persistencia.DAL;
using System.Linq;

namespace Servicos.Cadastros
{
    public class ClienteServico : IServico<Cliente>
    {
        private ClienteRepositorio _clienteRepositorio = new ClienteRepositorio();

        public Cliente ObterClientePorUsuarioId(string usuarioID)
        {
            return _clienteRepositorio.ObterClientePorUsuarioId(usuarioID);
        }

        public void GravarCliente(Cliente cliente, string usuarioID)
        {
            Cliente clienteBanco = ObterClientePorUsuarioId(usuarioID);
            if (clienteBanco != null)
            {
                clienteBanco.Nome = cliente.Nome;
                clienteBanco.Rua = cliente.Rua;
                clienteBanco.Numero = cliente.Numero;
                clienteBanco.Cidade = cliente.Cidade;
                clienteBanco.Estado = cliente.Estado;
                clienteBanco.Bairro = cliente.Bairro;
                Atualizar(clienteBanco);
            }
            else
            {
                cliente.UsuarioID = usuarioID;
                Inserir(cliente);
            }
        }

        public void Inserir(Cliente entidade)
        {
            _clienteRepositorio.Inserir(entidade);
            Salvar();
        }

        public void Atualizar(Cliente entidade)
        {
            _clienteRepositorio.Atualizar(entidade);
            Salvar();
        }

        public void Deletar(Cliente entidade)
        {
            _clienteRepositorio.Deletar(entidade);
            Salvar();
        }

        public Cliente BuscarPorId(long id) => _clienteRepositorio.BuscarPorId(id);

        public IQueryable<Cliente> BuscarTodos() => _clienteRepositorio.BuscarTodos();

        public void Salvar() => _clienteRepositorio.Salvar();
    }
}
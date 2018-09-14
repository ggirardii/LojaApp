using Modelo.Clientes;
using Modelo.Contratos;
using System.Linq;

namespace Persistencia.DAL
{
    public class ClienteRepositorio : RepositorioBase<Cliente>, IClienteRepositorio
    {
        public Cliente ObterClientePorUsuarioId(string usuarioID)
        {
            return Filtrar(x => x.UsuarioID == usuarioID).FirstOrDefault();
        }
    }
}
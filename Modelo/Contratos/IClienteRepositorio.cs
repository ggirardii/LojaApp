using Modelo.Clientes;

namespace Modelo.Contratos
{
    public interface IClienteRepositorio
    {
        Cliente ObterClientePorUsuarioId(string usuarioID);
    }
}
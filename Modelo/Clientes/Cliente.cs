namespace Modelo.Clientes
{
    public class Cliente
    {
        public long ClienteID { get; set; }
        public string Nome { get; set; }
        public string Rua { get; set; }
        public string Numero { get; set; }
        public string Bairro { get; set; }
        public string Estado { get; set; }
        public string Cidade { get; set; }

        public string UsuarioID { get; set; }
    }
}
namespace Compras.API.Migrations
{
    public interface IProvedorDeServico
    {
        string SufixoBancosDeDados { get; }
    }

    public class ProvedorDeServico : IProvedorDeServico
    {
        public string SufixoBancosDeDados { get; set; }
    }
}

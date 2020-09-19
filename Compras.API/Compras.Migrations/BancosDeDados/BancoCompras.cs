namespace Compras.API.Migrations.BancosDeDados
{
    public class BancoCompras : BancoDeDados
    {
        public BancoCompras()
        {
            nomeDoBanco = nameof(Compras);
            Ordem = 5;
        }
    }
}

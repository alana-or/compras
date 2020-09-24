using FluentMigrator;

namespace Compras.Migrations.Compras
{
    [Migration(202009241810)]
    public class CriarPedidoDetalhe : Migration
    {
        private const string chavePrimaria = "Pk_PedidoDetalhe";
        private const string fkProduto = "Fk_PedidoDetalhe_Produto";
        private const string fkPedido = "Fk_PedidoDetalhe_Pedido";
        private const string schema = "dbo";
        private const string nomeTabela = "PedidoDetalhe";
        private const string nomeTabelaProduto = "Produto";
        private const string nomeTabelaPedido = "Pedido";

        public override void Up()
        {
            Create
                .Table(nomeTabela)
                .InSchema(schema)
                .WithColumn("id").AsInt32().NotNullable().PrimaryKey(chavePrimaria).Identity()
                .WithColumn("idProduto").AsInt32().NotNullable()
                .WithColumn("idPedido").AsInt32().NotNullable()
                .WithColumn("quantidade").AsInt32().NotNullable();

            Create.ForeignKey(fkPedido)
                .FromTable(nomeTabela)
                .InSchema(schema)
                .ForeignColumns("idPedido")
                .ToTable(nomeTabelaPedido)
                .InSchema(schema)
                .PrimaryColumns("id");

            Create.ForeignKey(fkProduto)
                .FromTable(nomeTabela)
                .InSchema(schema)
                .ForeignColumns("idProduto")
                .ToTable(nomeTabelaProduto)
                .InSchema(schema)
                .PrimaryColumns("id");
        }

        public override void Down()
        {
            Delete.PrimaryKey(chavePrimaria).FromTable(nomeTabela).InSchema(schema);
            Delete.ForeignKey(fkPedido).OnTable(nomeTabelaPedido).InSchema(schema);
            Delete.ForeignKey(fkProduto).OnTable(nomeTabelaProduto).InSchema(schema);
            Delete.Table(nomeTabela).InSchema(schema);
        }
    }
}

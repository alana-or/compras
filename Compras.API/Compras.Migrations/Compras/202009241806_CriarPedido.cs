using FluentMigrator;

namespace Compras.Migrations.Compras
{
    [Migration(202009241806)]
    public class CriarPedido : Migration
    {
        private const string chavePrimaria = "Pk_Pedido";
        private const string schema = "dbo";
        private const string nomeTabela = "Pedido";
        private const string fkCliente = "Fk_Pedido_Cliente";
        private const string nomeTabelaCliente = "Cliente";

        public override void Up()
        {
            Create
                .Table(nomeTabela)
                .InSchema(schema)
                .WithColumn("id").AsInt32().NotNullable().PrimaryKey(chavePrimaria).Identity()
                .WithColumn("criadoEm").AsDateTime().Nullable()
                .WithColumn("status").AsInt32().NotNullable()
                .WithColumn("idCliente").AsInt32().NotNullable();

            Create.ForeignKey(fkCliente)
                .FromTable(nomeTabela)
                .InSchema(schema)
                .ForeignColumns("idCliente")
                .ToTable(nomeTabelaCliente)
                .InSchema(schema)
                .PrimaryColumns("id");
        }

        public override void Down()
        {
            Delete.PrimaryKey(chavePrimaria).FromTable(nomeTabela).InSchema(schema);
            Delete.ForeignKey(fkCliente).OnTable(nomeTabelaCliente).InSchema(schema);
            Delete.Table(nomeTabela).InSchema(schema);
        }
    }
}

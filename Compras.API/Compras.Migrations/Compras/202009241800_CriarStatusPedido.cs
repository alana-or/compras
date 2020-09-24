using FluentMigrator;

namespace Compras.Migrations.Compras
{
    [Migration(202009241800)]
    public class CriarStatusPedido : Migration
    {
        private const string chavePrimaria = "Pk_CriarStatusPedido";
        private const string schema = "dbo";
        private const string nomeTabela = "CriarStatusPedido";

        public override void Up()
        {
            Create
                .Table(nomeTabela)
                .InSchema(schema)
                .WithColumn("id").AsInt32().NotNullable().PrimaryKey(chavePrimaria).Identity()
                .WithColumn("status").AsInt32().NotNullable()
                .WithColumn("descricao").AsString().NotNullable();
        }

        public override void Down()
        {
            Delete.PrimaryKey(chavePrimaria).FromTable(nomeTabela).InSchema(schema);
            Delete.Table(nomeTabela).InSchema(schema);
        }
    }
}

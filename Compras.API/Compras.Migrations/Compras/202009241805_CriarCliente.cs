using FluentMigrator;

namespace Compras.Migrations.Compras
{
    [Migration(202009241805)]
    public class CriarCliente : Migration
    {
        private const string chavePrimaria = "Pk_Cliente";
        private const string schema = "dbo";
        private const string nomeTabela = "Cliente";

        public override void Up()
        {
            Create
                .Table(nomeTabela)
                .InSchema(schema)
                .WithColumn("id").AsInt32().NotNullable().PrimaryKey(chavePrimaria).Identity()
                .WithColumn("nome").AsInt32().NotNullable()
                .WithColumn("criadoEm").AsDateTime().Nullable();
        }

        public override void Down()
        {
            Delete.PrimaryKey(chavePrimaria).FromTable(nomeTabela).InSchema(schema);
            Delete.Table(nomeTabela).InSchema(schema);
        }
    }
}

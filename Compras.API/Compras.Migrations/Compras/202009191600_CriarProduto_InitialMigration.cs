using FluentMigrator;

namespace Compras.Migrations.Compras
{
    [Migration(202009191600)]
    public class CriarProduto : Migration
    {
        private const string chavePrimaria = "Pk_Produto";
        private const string schema = "dbo";
        private const string nomeTabela = "Produto";

        public override void Up()
        {
            Create
                .Table(nomeTabela)
                .InSchema(schema)
                .WithColumn("id").AsInt32().NotNullable().PrimaryKey(chavePrimaria).Identity()
                .WithColumn("descricao").AsString().NotNullable()
                .WithColumn("preco").AsInt32().NotNullable()
                .WithColumn("nome").AsInt32().NotNullable()
                .WithColumn("ativo").AsBoolean().NotNullable().WithDefaultValue(true)
                .WithColumn("criadoEm").AsDateTime().NotNullable().WithDefault(SystemMethods.CurrentDateTime);
        }

        public override void Down()
        {
            Delete.PrimaryKey(chavePrimaria).FromTable(nomeTabela).InSchema(schema);
            Delete.Table(nomeTabela).InSchema(schema);
        }
    }
}

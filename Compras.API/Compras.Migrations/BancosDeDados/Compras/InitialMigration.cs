using FluentMigrator;

namespace Compras.Migrations.BancosDeDados.Compras
{
    [Migration(202009191600)]
    public class CriarOSPerguntasQuestionario : Migration
    {
        private const string chavePrimaria = "Pk_Chave";
        private const string schema = "dbo";
        private const string nomeTabela = "table";

        public override void Up()
        {
            Create
                .Table(nomeTabela)
                .InSchema(schema)
                .WithColumn("id").AsInt32().NotNullable().PrimaryKey(chavePrimaria).Identity()
                .WithColumn("texto").AsString().NotNullable()
                .WithColumn("ordem").AsInt32().NotNullable()
                .WithColumn("tipoResposta").AsInt32().NotNullable()
                .WithColumn("ativo").AsBoolean().NotNullable().WithDefaultValue(true)
                .WithColumn("dataCriacao").AsDateTime().NotNullable().WithDefault(SystemMethods.CurrentDateTime);
        }

        public override void Down()
        {
            Delete.PrimaryKey(chavePrimaria).FromTable(nomeTabela).InSchema(schema);
            Delete.Table(nomeTabela).InSchema(schema);
        }
    }
}

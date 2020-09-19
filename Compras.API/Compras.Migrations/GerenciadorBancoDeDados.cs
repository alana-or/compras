using System;
using System.Data.SqlClient;
using System.IO;

namespace Compras.API.Migrations
{
    public class GerenciadorBancoDeDados
    {
        private readonly string stringDeConexao;
        private readonly string diretorioBancosDeDados;

        public GerenciadorBancoDeDados(string stringDeConexao, string diretorioBancosDeDados)
        {
            this.stringDeConexao = stringDeConexao;
            this.diretorioBancosDeDados = diretorioBancosDeDados;
        }

        private void ExecutarScript(string script)
        {
            using (var conexao = new SqlConnection(stringDeConexao))
            {
                using (var comando = new SqlCommand(script, conexao))
                {
                    conexao.Open();
                    comando.ExecuteNonQuery();
                }
            }
        }

        public void CriarCasoNaoExista(string nomeBancoDeDados)
        {
            var diretorioUsuario = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile,
                Environment.SpecialFolderOption.DoNotVerify);

            var diretorio = Path.Combine(diretorioUsuario, diretorioBancosDeDados);
            Directory.CreateDirectory(diretorio);
            var caminhoBancoDeDados = Path.Combine(diretorio, nomeBancoDeDados);

            var script = $@"IF (NOT EXISTS (SELECT name
                    FROM master.dbo.sysdatabases
                    WHERE ('[' + name + ']' = '{nomeBancoDeDados}'
                    OR name = '{nomeBancoDeDados}')))
                        BEGIN
                            CREATE DATABASE [{nomeBancoDeDados}] on (name='{nomeBancoDeDados}', filename='{caminhoBancoDeDados}.mdf')
                        END";
            ExecutarScript(script);
        }

        public void ExcluirBancoDeDados(string nomeBancoDeDados)
        {
            using (var conexao = new SqlConnection(stringDeConexao))
            {
                string dbFileName, dbLogFileName;
                conexao.Open();

                using (var cmdFileName = conexao.CreateCommand())
                {
                    cmdFileName.CommandText =
                        $"SELECT physical_name FROM sys.master_files WHERE name = '{nomeBancoDeDados}'";
                    dbFileName = (string)cmdFileName.ExecuteScalar();

                    cmdFileName.CommandText =
                        $"SELECT physical_name FROM sys.master_files WHERE name = '{nomeBancoDeDados}_log'";
                    dbLogFileName = (string)cmdFileName.ExecuteScalar();
                }

                if (!string.IsNullOrWhiteSpace(dbFileName))
                {
                    using (var cmd = conexao.CreateCommand())
                    {
                        const string command =
                            "ALTER DATABASE [{0}] SET SINGLE_USER WITH ROLLBACK IMMEDIATE; DROP database [{0}];";
                        cmd.CommandText = string.Format(command, nomeBancoDeDados);
                        cmd.ExecuteNonQuery();
                    }

                    Console.WriteLine("Deleting file '{0}'.", dbFileName);
                    File.Delete(dbFileName);

                    Console.WriteLine("Deleting file '{0}'.", dbLogFileName);
                    File.Delete(dbLogFileName);
                }
            }
        }
    }
}

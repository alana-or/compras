using CommandLine;
using System.Collections.Generic;

namespace Compras.API.Migrations
{
    public class Argumentos
    {
        [Option('n', "nomesDosBancos", HelpText = "Nomes dos bancos onde as migrations serao executadas.")]
        public IEnumerable<string> NomesDosBancos { get; set; }
        [Option('r', "rollback", HelpText = "Rollback.")]
        public bool Rollback { get; set; }
        [Option('s', "steps", HelpText = "Quantidades de versoes a serem removidas no rollback.")]
        public int Steps { get; set; }
        [Option('o', "output", HelpText = "Nome do arquivo que será gerado o Script SQL em vez de aplicar as migrations no banco de dados.")]
        public string ArquivoDeSaida { get; private set; }
    }
}

using System;

namespace Compras.API.Domain
{
    public class Produto
    {
        public int CodigoProduto { get; set; }
        public string Nome { get; set; }
        public int Preco { get; set; }
        public string Descricao { get; set; }
        public bool Ativo { get; set; }
        public DateTime DataCriacao { get; set; }
    }
}

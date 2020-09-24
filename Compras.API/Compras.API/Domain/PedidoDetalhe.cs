using System.Collections.Generic;
using System.Linq;

namespace Compras.API.Domain
{
    public class PedidoDetalhe
    {
        public int CodigoPedidoDetalhe { get; set; }
        
        public int CodigoPedido { get; set; }
        public Pedido Pedido { get; set; }
        
        private List<Produto> produtos;
        public virtual IReadOnlyCollection<Produto> Produtos
        {
            get => produtos?.ToList();
            protected set => produtos = (List<Produto>)value;
        }
    }
}

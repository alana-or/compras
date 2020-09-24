using System;

namespace Compras.API.Domain
{
    public class Pedido
    {
        public int CodigoPedido { get; set; }
        public int CodigoCliente{ get; set; }
        public Cliente Cliente { get; set; }
        public int CodigoPedidoDetalhe { get; set; }
        public PedidoDetalhe PedidoDetalhe { get; set; }
        public Status Status { get; set; }
        public DateTime DataCriacao { get; set; }
    }

    public enum Status
    {
        Ativo = 1,
        Inativo = 2,
        Finalizado = 3
    }
}

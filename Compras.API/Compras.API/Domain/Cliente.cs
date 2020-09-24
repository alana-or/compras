using System;
using System.Collections.Generic;
using System.Linq;

namespace Compras.API.Domain
{
    public class Cliente
    {
        public int CodigoCliente { get; set; }

        private List<Pedido> pedidos;
        public virtual IReadOnlyCollection<Pedido> Pedidos
        {
            get => pedidos?.ToList();
            protected set => pedidos = (List<Pedido>)value;
        }

        public string Nome { get; set; }
        public DateTime DataCriacao { get; set; }
    }
}

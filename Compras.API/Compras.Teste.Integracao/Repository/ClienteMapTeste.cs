using AutoBogus;
using Compras.API.Domain;
using Compras.API.Teste.Integracao;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Compras.Teste.Integracao.Repository
{
    public class ClienteMapTeste : TesteIntegracaoBase
    {
        [Test]
        public async Task DeveMapearClientes()
        {
            Cliente cliente;
            await using (var context = ObterCorporateContext())
            {
                cliente = new AutoFaker<Cliente>()
                     .RuleFor(x => x.CodigoCliente, 0)
                     .RuleFor(x => x.DataCriacao, DateTime.Now)
                     .RuleFor(x => x.Nome, f => f.Name.FullName())
                     .RuleFor(x => x.Pedidos, new List<Pedido>())
                     .Generate();

                context.Clientes.Add(cliente);
                await context.SaveChangesAsync();
            }

            await using (var context = ObterCorporateContext())
            {
                var clienteRetornado = await context
                    .Clientes.SingleAsync();

                clienteRetornado.Should().BeEquivalentTo(cliente,
                    opt => opt.IgnoringCyclicReferences());
            }
        }
    }
}

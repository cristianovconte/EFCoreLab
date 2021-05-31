using ConsoleApp.Data;
using ConsoleApp.Domain;
using System;
using System.Threading.Tasks;
using System.Linq;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace ConsoleApp
{
    class Program
    {
        async static Task Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            //await InserirDados();
            //await InserirDadosEmMassa();
            //ConsultarDados();
            //await CadastrarPedido();
            // await ConsultarPedidoCarregamentoAdiantado();
            await AtualizarDados();
        }

        private async static Task InserirDados()
        {
            var produto = new Produto()
            {
                Descricao = "Produto teste",
                CodigoBarras = "1234564654",
                Valor = 10m,
                TipoProduto = TipoProduto.MercadoriaParaRevenda,
                Ativo = true
            };

            using var db = new Context();
            db.Set<Produto>().Add(produto);

            var registros = await db.SaveChangesAsync();
        }

        private async static Task InserirDadosEmMassa()
        {
            var produto = new Produto()
            {
                Descricao = "Produto teste",
                CodigoBarras = "1234564654",
                Valor = 10m,
                TipoProduto = TipoProduto.MercadoriaParaRevenda,
                Ativo = true
            };

            var cliente = new Cliente()
            {
                Nome = "Teste Cliente",
                CEP = "99999999",
                Cidade = "Cotia",
                Estado = "SP",
                Telefone = "91185181818"
            };

            using var db = new Context();
            db.AddRange(produto, cliente);

            var registros = await db.SaveChangesAsync();

            Console.WriteLine(registros);
        }

        private async static Task ConsultarDados()
        {
            using var db = new Context();

            var consultarPorSintaxe = (from c in db.Clientes where c.Id > 0 select c).ToList();

            var consultarPorMetodo = db.Clientes
                .Where(p => p.Id > 0).ToList();

            foreach (var item in consultarPorMetodo)
            {
                Console.WriteLine($"Consultado Cliente: {item.Id}");

                /* Por default o Find é o único método que não faz consulta no banco de dados, se Tracking desabilitado irá fazer consulta */
                db.Clientes.Find(item.Id);
            }
        }

        private async static Task CadastrarPedido()
        {
            using var db = new Context();

            var cliente = db.Clientes.FirstOrDefault();
            var produto = db.Produtos.FirstOrDefault();

            var pedido = new Pedido()
            {
                ClienteId = cliente.Id,
                FinalizadoEm = DateTime.Now,
                Observacao = "Pedido teste",
                StatusPedido = StatusPedido.Analise,
                TipoFrete = TipoFrete.SemFrete,
                Itens = new List<PedidoItem>()
                {
                    new PedidoItem()
                    {
                        ProdutoId = produto.Id,
                        Desconto = 0,
                        Quantitade = 1,
                        Valor = 10
                    }
                }
            };

            db.Pedidos.Add(pedido);

            await db.SaveChangesAsync();
        }

        private async static Task ConsultarPedidoCarregamentoAdiantado()
        {
            using var db = new Context();
            var pedidos = db.Pedidos
                .Include(_ => _.Itens)
                    .ThenInclude(_ => _.Produto)
                .ToList();


            Console.WriteLine(pedidos.Count);
        }

        private async static Task AtualizarDados()
        {
            using var db = new Context();
            var cliente = db.Clientes.Find(1);

            cliente.Nome = "Nome alterado";

            await db.SaveChangesAsync();
        }

        private async static Task AtualizarDadosDesconectado()
        {
            using var db = new Context();

            var cliente = db.Clientes.Find(1);

            var clienteDesconectado = new Cliente()
            {
                Nome = "Cliente desconectado",
                Telefone = "0696956565"
            };

            db.Entry(cliente).CurrentValues.SetValues(clienteDesconectado);

            await db.SaveChangesAsync();
        }

        private async static Task AtualizarDadosTotalmenteDesconectado()
        {
            using var db = new Context();

            var clienteTotalmenteDesconectado = new Cliente()
            {
                Id = 1
            };

            var clienteDesconectado = new Cliente()
            {
                Nome = "Cliente desconectado",
                Telefone = "0696956565"
            };

            db.Attach(clienteTotalmenteDesconectado);
            db.Entry(clienteTotalmenteDesconectado).CurrentValues.SetValues(clienteDesconectado);

            await db.SaveChangesAsync();
        }

        private async static Task RemoverRegistros()
        {
            using var db = new Context();

            var cliente = db.Clientes.Find(2);

            db.Clientes.Remove(cliente);

            await db.SaveChangesAsync();
        }

        private async static Task RemoverRegistrosDesconectado()
        {
            using var db = new Context();

            var cliente = new Cliente() { Id = 1 };

            db.Entry(cliente).State = EntityState.Deleted;

            await db.SaveChangesAsync();
        }
    }
}

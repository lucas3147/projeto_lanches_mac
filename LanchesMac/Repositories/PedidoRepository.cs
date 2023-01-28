using LanchesMac.Context;
using LanchesMac.Models;
using LanchesMac.Repositories.Interfaces;

namespace LanchesMac.Repositories
{
    public class PedidoRepository : IPedidoRepository
    {
        private readonly CarrinhoCompra _carrinhoCompra;
        private readonly AppDbContext _context;

        public PedidoRepository(AppDbContext context, CarrinhoCompra carrinhoCompra)
        {
            _context = context;
            _carrinhoCompra = carrinhoCompra;
        }

        public void CriarPedido(Pedido pedido)
        {
            pedido.PedidoEnviado = DateTime.Now;
            _context.Pedidos.Add(pedido);
            _context.SaveChanges();

            foreach (var carrinhoCompraItem in _carrinhoCompra.CarrinhoCompraItens)
            {
                var PedidoDetalhe = new PedidoDetalhe()
                {
                    LancheId = carrinhoCompraItem.Lanche.LancheId,
                    PedidoId = pedido.PedidoId,
                    Preco = carrinhoCompraItem.Lanche.Preco,
                    Quantidade = carrinhoCompraItem.Quantidade
                };

                _context.PedidoDetalhes.Add(PedidoDetalhe);
            }

            _context.SaveChanges();
        }
    }
}

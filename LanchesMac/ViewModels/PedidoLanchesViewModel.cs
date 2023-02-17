using LanchesMac.Models;

namespace LanchesMac.ViewModels
{
    public class PedidoLanchesViewModel
    {
        public Pedido Pedido { get; set; }
        public IEnumerable<PedidoDetalhe> PedidoDetalhes { get; set; }
    }
}

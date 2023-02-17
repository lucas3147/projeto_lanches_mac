using LanchesMac.Context;
using LanchesMac.Models;
using Microsoft.EntityFrameworkCore;

namespace LanchesMac.Areas.Admin.Servicos
{
    public class RelatorioVendasService
    {
        private readonly AppDbContext context;

        public RelatorioVendasService(AppDbContext context)
        {
            this.context = context;
        }

        public async Task<List<Pedido>> FindByDateAsync(DateTime? minDate, DateTime? maxDate)
        {
            var resultado = from obj in context.Pedidos select obj;

            if (minDate.HasValue)
            {
                resultado = resultado.Where(r => r.PedidoEnviado >= minDate);
            }
            if (maxDate.HasValue)
            {
                resultado = resultado.Where(r => r.PedidoEnviado <= maxDate);
            }

            return await resultado
                            .Include(l => l.PedidoItens)
                            .ThenInclude(l => l.Lanche)
                            .OrderByDescending(x => x.PedidoEnviado)
                            .ToListAsync();
        }
    }
}

using LanchesMac.Models;
using LanchesMac.Repositories.Interfaces;
using LanchesMac.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata;

namespace LanchesMac.Controllers
{
    public class LancheController : Controller
    {
        private readonly ILancheRepository _lancheRepository;

        public LancheController(ILancheRepository lancheRepository)
        {
            _lancheRepository = lancheRepository;
        }

        public IActionResult List(string categoria)
        {
            IEnumerable<Lanche> lanches;

            string categoriaAtual = string.Empty;

            if (string.IsNullOrEmpty(categoria))
            {
                lanches = _lancheRepository.Lanches.OrderBy(l => l.LancheId);
                categoriaAtual = "Todos os lanches";
            }
            else
            {
                lanches = _lancheRepository.Lanches.Where(x => x.Categoria.CategoriaNome == categoria)
                    .OrderBy(c => c.Nome);
            }

            var lanchesListViewModel = new LancheListViewModels
            {
                Lanches = lanches,
                CategoriaAtual = categoriaAtual
            }; 

            return View(lanchesListViewModel);
        }

        public IActionResult Details(int LancheId)
        {
            var lanche = _lancheRepository.Lanches.FirstOrDefault(l => l.LancheId == LancheId);

            return View(lanche);
        }

        public IActionResult Search(string strSearch)
        {
            IEnumerable<Lanche> Lanches;
            string categoriaAtual = string.Empty;

            if(string.IsNullOrEmpty(strSearch))
            {
                Lanches = _lancheRepository.Lanches.OrderBy(l => l.LancheId); 
                categoriaAtual = "Todos os lanches";
            }else
            {
                Lanches = _lancheRepository.Lanches.Where(l => l.Nome == strSearch);

                if(Lanches.Any())
                    categoriaAtual = "Lanches";
                else
                    categoriaAtual = "Nenhum Lanche foi encontrado";
            }

            return View("~/Views/Lanche/List.cshtml", new LancheListViewModels()
            {
                Lanches = Lanches,
                CategoriaAtual = categoriaAtual
            });
        }
    }
}

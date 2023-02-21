using FastReport.Data;
using FastReport.Web;
using LanchesMac.Areas.Admin.FastReportUtil;
using LanchesMac.Areas.Admin.Servicos;
using Microsoft.AspNetCore.Mvc;

namespace LanchesMac.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AdminLanchesReportController : Controller
    {
        private readonly IWebHostEnvironment _webHostEnv;
        private readonly RelatorioLanchesService _relatorioLanchesService;

        public AdminLanchesReportController(IWebHostEnvironment webHostEnv, RelatorioLanchesService relatorioLanchesService)
        {
            _webHostEnv = webHostEnv;
            _relatorioLanchesService = relatorioLanchesService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [Route("LanchesCategoriaReport")]
        public async Task<ActionResult> LanchesCategoriaReport()
        {
            var webReport = new WebReport();

            var mssqlDataConnetion = new MsSqlDataConnection();

            webReport.Report.Dictionary.AddChild(mssqlDataConnetion);

            webReport.Report.Load(Path.Combine(_webHostEnv.ContentRootPath, "wwwroot/reports", "LanchesCategoria.frx"));

            var lanches = HelperFastReport.GetTable(await _relatorioLanchesService.GetLanchesReport(), "LanchesReport");

            var categorias = HelperFastReport.GetTable(await _relatorioLanchesService.GetCategoriasReport(), "CategoriasReport");

            webReport.Report.RegisterData(lanches, "LanchesReport");
            webReport.Report.RegisterData(categorias, "CategoriasReport");

            return View(webReport);
        }
    }
}

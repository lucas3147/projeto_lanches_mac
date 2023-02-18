using LanchesMac.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace LanchesMac.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class AdminImagensController : Controller
    {
        private readonly ConfigurationImagens _myConfig;

        private readonly IWebHostEnvironment hostingEnvironment;

        public AdminImagensController(IOptions<ConfigurationImagens> myConfiguration, IWebHostEnvironment hostingEnvironment)
        {
            _myConfig = myConfiguration.Value;
            this.hostingEnvironment = hostingEnvironment;
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}

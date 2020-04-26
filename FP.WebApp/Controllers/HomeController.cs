using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace FP.WebApp.Controllers
{
    public class HomeController : Controller
    {
        public async Task<IActionResult> Index()
        {
            return View();
        }       
    }
}

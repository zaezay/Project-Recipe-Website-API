using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RecipeSharingWebsiteAPI.Data;
using RecipeSharingWebsiteAPI.Models;
using System.Diagnostics;

namespace RecipeSharingWebsiteAPI.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly RecipeSharingWebsiteContext _context;

        public HomeController(ILogger<HomeController> logger, RecipeSharingWebsiteContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var infos = await _context.Info.ToListAsync();
            return View(infos);
        }

        [HttpPost]
        public async Task<IActionResult> Details(int recipe_id)
        {
            var recipe = await _context.Info.FirstOrDefaultAsync(r => r.recipe_id == recipe_id);
            if (recipe == null)
            {
                return NotFound();
            }
            return View(recipe);
        }

        [HttpGet]
        public async Task<IActionResult> GetRecipeDetails(int recipe_id)
        {
            var recipe = await _context.Recipe
                .Include(r => r.info)
                .FirstOrDefaultAsync(r => r.recipe_id == recipe_id);

            if (recipe == null)
            {
                return NotFound();
            }

            return Json(recipe);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult AboutUs()
        {
            return View();
        }

        public IActionResult FAQ()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
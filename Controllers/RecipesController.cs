using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RecipeSharingWebsiteAPI.Data;
using RecipeSharingWebsiteAPI.Models;

namespace RecipeSharingWebsiteAPI.Controllers
{
    [Route("[controller]")]
    public class RecipesController : Controller
    {
        private readonly RecipeSharingWebsiteContext _context;

        public RecipesController(RecipeSharingWebsiteContext context)
        {
            _context = context;
        }

        // GET: Recipes
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var recipes = await _context.Recipe.ToListAsync();
            return View(recipes);
        }

        // GET: Recipes/Details/5
        [HttpGet("Details/{id}")]
        public async Task<IActionResult> Details(int id)
        {
            var recipe = await _context.Recipe.FindAsync(id);
            if (recipe == null)
            {
                return NotFound();
            }
            return View(recipe);
        }

        // GET: Recipes/Create
        [HttpGet("Create")]
        public IActionResult Create()
        {
            ViewBag.InfoSelectList = new SelectList(_context.Info, "recipe_id", "recipe_title");
            return View();
        }

        // POST: Recipes/Create
        [HttpPost("Create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(recipe recipe)
        {
            if (ModelState.IsValid)
            {
                _context.Add(recipe);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewBag.InfoSelectList = new SelectList(_context.Info, "recipe_id", "recipe_title", recipe.recipe_id);
            return View(recipe);
        }

        // GET: Recipes/Edit/5
        [HttpGet("Edit/{id}")]
        public async Task<IActionResult> Edit(int id)
        {
            var recipe = await _context.Recipe.FindAsync(id);
            if (recipe == null)
            {
                return NotFound();
            }
            ViewBag.InfoSelectList = new SelectList(_context.Info, "recipe_id", "recipe_title", recipe.recipe_id);
            return View(recipe);
        }

        // POST: Recipes/Edit/5
        [HttpPost("Edit/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, recipe recipe)
        {
            if (id != recipe.detail_id)
            {
                return BadRequest();
            }

            // Check if the foreign key exists in the info table
            var infoExists = await _context.Info.AnyAsync(e => e.recipe_id == recipe.recipe_id);
            if (!infoExists)
            {
                ModelState.AddModelError("recipe_id", "The recipe_id does not exist in the info table.");
                ViewBag.InfoSelectList = new SelectList(_context.Info, "recipe_id", "recipe_title", recipe.recipe_id);
                return View(recipe);
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(recipe);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RecipeExists(id))
                    {
                        return NotFound();
                    }
                    throw;
                }
                catch (Exception ex)
                {
                    // Log the exception
                    Console.WriteLine($"An error occurred while updating the recipe: {ex.Message}");
                    ModelState.AddModelError(string.Empty, "An error occurred while saving the changes. Please try again.");
                }
            }
            else
            {
                // Log model state errors
                foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
                {
                    Console.WriteLine($"Model error: {error.ErrorMessage}");
                }
            }

            ViewBag.InfoSelectList = new SelectList(_context.Info, "recipe_id", "recipe_title", recipe.recipe_id);
            return View(recipe);
        }

        private bool RecipeExists(int id)
        {
            return _context.Recipe.Any(e => e.detail_id == id);
        }

        // GET: Recipes/Delete/5
        [HttpGet("Delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var recipe = await _context.Recipe.FindAsync(id);
            if (recipe == null)
            {
                return NotFound();
            }
            return View(recipe);
        }

        // POST: Recipes/Delete/5
        [HttpPost("Delete/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var recipe = await _context.Recipe.FindAsync(id);
            if (recipe != null)
            {
                _context.Recipe.Remove(recipe);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
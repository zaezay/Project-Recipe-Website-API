using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RecipeSharingWebsiteAPI.Data;
using RecipeSharingWebsiteAPI.Models;

namespace RecipeSharingWebsiteAPI.Controllers
{
    [Route("[controller]")]
    public class InfosController : Controller
    {
        private readonly RecipeSharingWebsiteContext _context;

        public InfosController(RecipeSharingWebsiteContext context)
        {
            _context = context;
        }

        // GET: Infos
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var infos = await _context.Info.ToListAsync();
            return View(infos);
        }

        // GET: Infos/Details/5
        [HttpGet("Details/{id}")]
        public async Task<IActionResult> Details(int id)
        {
            var info = await _context.Info.FindAsync(id);
            if (info == null)
            {
                return NotFound();
            }
            return View(info);
        }

        // GET: Infos/Create
        [HttpGet("Create")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Infos/Create
        [HttpPost("Create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("recipe_id,recipe_title,create_by")] info info, IFormFile? recipe_image)
        {
            if (ModelState.IsValid)
            {
                if (recipe_image != null && recipe_image.Length > 0)
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        await recipe_image.CopyToAsync(memoryStream);
                        info.recipe_image = memoryStream.ToArray();
                    }
                }

                _context.Add(info);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(info);
        }

        // GET: Infos/Edit/5
        [HttpGet("Edit/{id}")]
        public async Task<IActionResult> Edit(int id)
        {
            var info = await _context.Info.FindAsync(id);
            if (info == null)
            {
                return NotFound();
            }
            return View(info);
        }

        // POST: Infos/Edit/5
        [HttpPost("Edit/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("recipe_id,recipe_title,create_by")] info info, IFormFile? recipe_image)
        {
            if (id != info.recipe_id)
            {
                return BadRequest();
            }
            if (ModelState.IsValid)
            {
                var existingInfo = await _context.Info.FindAsync(id);
                if (existingInfo == null)
                {
                    return NotFound();
                }

                existingInfo.recipe_title = info.recipe_title;
                existingInfo.create_by = info.create_by;

                if (recipe_image != null && recipe_image.Length > 0)
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        await recipe_image.CopyToAsync(memoryStream);
                        existingInfo.recipe_image = memoryStream.ToArray();
                    }
                }

                try
                {
                    _context.Update(existingInfo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!InfoExists(id))
                    {
                        return NotFound();
                    }
                    throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(info);
        }

        // GET: Infos/Delete/5
        [HttpGet("Delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var info = await _context.Info.FindAsync(id);
            if (info == null)
            {
                return NotFound();
            }
            return View(info);
        }

        // POST: Infos/Delete/5
        [HttpPost("Delete/{id}"), ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var info = await _context.Info.FindAsync(id);
            if (info != null)
            {
                _context.Info.Remove(info);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        private bool InfoExists(int id)
        {
            return _context.Info.Any(e => e.recipe_id == id);
        }
    }
}
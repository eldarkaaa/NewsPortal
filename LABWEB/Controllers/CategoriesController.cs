using LABWEB.Data;
using LABWEB.Models.Entities;
using LABWEB.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static System.Net.Mime.MediaTypeNames;

namespace LABWEB.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly ApplicationDBContext dbContext;
        public CategoriesController(ApplicationDBContext dbContext)
        {
            this.dbContext = dbContext;
        }
        [HttpGet]
        public async Task<IActionResult> List()
        {
            var categories = await dbContext.Categories.ToListAsync();
            return View(categories);
        }
        [HttpGet]
        public async Task<IActionResult> NewsList(int id)
        {
            return View(dbContext.News.Where(p => p.CategoriesId.Equals(id)).ToList());
        }
        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Add(AddCategoriesViewModel viewModel)
        {
            var categories = new Categories
            {
                Category = viewModel.Category
            };
            await dbContext.Categories.AddAsync(categories);
            await dbContext.SaveChangesAsync();
            return RedirectToAction("List", "Categories");
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var categories = await dbContext.Categories.FindAsync(id);
            return View(categories);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(Categories viewModel)
        {
            var categories = await dbContext.Categories.FindAsync(viewModel.Id);
            if (categories is not null)
            {
                categories.Category = viewModel.Category;
                await dbContext.SaveChangesAsync();
            }
            return RedirectToAction("List", "Categories");
        }
        [HttpPost]
        public async Task<IActionResult> Delete(Categories viewModel)
        {
            var categories = await dbContext.Categories.AsNoTracking().FirstOrDefaultAsync(x => x.Id == viewModel.Id);
            if (categories is not null)
            {
                dbContext.Categories.Remove(viewModel);
                await dbContext.SaveChangesAsync();
            }
            return RedirectToAction("List", "Categories");
        }
    }
}

using LABWEB.Data;
using LABWEB.Models;
using LABWEB.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LABWEB.Controllers
{
    public class NewsController : Controller
    {
        private readonly ApplicationDBContext dbContext;
        public NewsController(ApplicationDBContext dbContext)
        {
            this.dbContext = dbContext;
        }
        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Add(AddNewsViewModel viewModel)
        {
            var news = new News
            {
                HeadLine = viewModel.HeadLine,
                Text = viewModel.Text,
                Date = viewModel.Date,
                Author = viewModel.Author,
                CategoriesId = viewModel.CategoriesId
            };
            await dbContext.News.AddAsync(news);
            await dbContext.SaveChangesAsync();
            return RedirectToAction("List", "News");
        }
        [HttpGet]
        public async Task<IActionResult> List()
        {
            var news = await dbContext.News.ToListAsync();
            return View(news);
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var news = await dbContext.News.FindAsync(id);
            return View(news);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(News viewModel)
        {
            var news = await dbContext.News.FindAsync(viewModel.Id);
            if (news is not null)
            {
                news.HeadLine = viewModel.HeadLine;
                news.Text = viewModel.Text;
                news.Date = viewModel.Date;
                news.Author = viewModel.Author;
                news.CategoriesId = viewModel.CategoriesId;
                await dbContext.SaveChangesAsync();
            }
            return RedirectToAction("List", "News");
        }
        [HttpPost]
        public async Task<IActionResult> Delete(News viewModel)
        {
            var news = await dbContext.News.AsNoTracking().FirstOrDefaultAsync(x => x.Id == viewModel.Id);
            if (news is not null)
            {
                dbContext.News.Remove(viewModel);
                await dbContext.SaveChangesAsync();
            }
            return RedirectToAction("List", "News");
        }
    }
}

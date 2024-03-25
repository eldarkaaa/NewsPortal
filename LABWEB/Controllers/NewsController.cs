using LABWEB.Data;
using LABWEB.Models;
using LABWEB.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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
        public async Task<IActionResult> Add()
        {
            var categories = await this.dbContext.Categories.ToListAsync();
            ViewBag.Categories = new SelectList(categories, "Id", "Category");
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
        public async Task<IActionResult> List(int pg=1)
        {
            var news = await dbContext.News.Include(n => n.Categories).ToListAsync();
            const int pageSize = 2;
            if (pg < 1)
                pg = 1;
            int recsCount = news.Count();
            var pager = new Pager(recsCount, pg, pageSize);
            int recSkip = (pg - 1) * pageSize;
            var data = news.Skip(recSkip).Take(pager.PageSize).ToList();
            this.ViewBag.Pager = pager;
            //return View(news);
            return View(data);
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

using LABWEB.Models.Entities;
using LABWEB.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LABWEB.Data;
namespace LABWEB.Controllers
{
    public class CommentsController : Controller
    {
        private readonly ApplicationDBContext dbContext;
        public CommentsController(ApplicationDBContext dbContext)
        {
            this.dbContext = dbContext;
        }
        [HttpGet]
        public async Task<IActionResult> List(int id)
        {
            var comments = dbContext.Comments.Where(p => p.News!.Id == id);
            var comments2 = await comments.ToListAsync();
            if (comments2 != null)
            {
                return View(comments2);
            }
            else
                return RedirectToAction("List", "News");

        }
        [HttpGet]
        public async Task<IActionResult> Add(int id)
        {
            var comments = await dbContext.Comments.FindAsync(id);

            return View(comments);
        }
        [HttpPost]
        public async Task<IActionResult> Add(AddCommentsViewModel viewModel, int id)
        {
            var comments = new Comments
            {
                Text = viewModel.Text,
                Author = viewModel.Author,
                NewsId = id
            };
            await dbContext.Comments.AddAsync(comments);
            await dbContext.SaveChangesAsync();
            return RedirectToAction("List", "News");
        }
    }
}

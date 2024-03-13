using Microsoft.AspNetCore.Mvc;
using LABWEB.Models.Entities;
using LABWEB.Data;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;
using LABWEB.Models;
using static System.Net.Mime.MediaTypeNames;

namespace LABWEB.Controllers
{
    public class SearchController : Controller
    {
        private readonly ApplicationDBContext dbContext;
        public SearchController(ApplicationDBContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<IActionResult> Index(string text)
        {
            return View(dbContext.News.Where(p => p.HeadLine.Contains(text) || p.Text.Contains(text) || p.Author.Contains(text)).ToList());
        }

    }
}

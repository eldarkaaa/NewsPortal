using Microsoft.EntityFrameworkCore;
using LABWEB.Models;
using LABWEB.Models.Entities;

namespace LABWEB.Data
{
    public class ApplicationDBContext : DbContext
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options)
        {

        }
        public DbSet<News> News { get; set; }
        public DbSet<Comments> Comments { get; set; }
    }
}

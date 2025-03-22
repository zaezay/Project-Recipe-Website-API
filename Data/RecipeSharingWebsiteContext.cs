using Microsoft.EntityFrameworkCore;
using RecipeSharingWebsiteAPI.Models;

namespace RecipeSharingWebsiteAPI.Data
{
    public class RecipeSharingWebsiteContext : DbContext
    {
        public RecipeSharingWebsiteContext(DbContextOptions<RecipeSharingWebsiteContext> options) : base(options) { }

        public DbSet<info> Info { get; set; }
        public DbSet<recipe> Recipe { get; set; }
    }
}

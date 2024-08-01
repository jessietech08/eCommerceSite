using eCommerceSite.Models;
using Microsoft.EntityFrameworkCore;
namespace eCommerceSite.Data
{
    public class VideoGameContext : DbContext
    {
        public VideoGameContext(DbContextOptions<VideoGameContext> option) : base(option) 
        {
            
        }

        public DbSet<Game> Games { get; set; }
    }
}

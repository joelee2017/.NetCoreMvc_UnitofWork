using Microsoft.EntityFrameworkCore;
using Model.Models;

namespace Model.Data
{
    public class MvcMovieContext : DbContext
    {
        public MvcMovieContext(DbContextOptions<MvcMovieContext> options)
            : base(options)
        {
        }

        public DbSet<Movie> Movie { get; set; }

        public DbSet<Sales> Sales { get; set; }
    }
}

using Microsoft.EntityFrameworkCore;
using MoviesCRUD.Models;

namespace MoviesAPI.Context
{
    public class ApplicationDbContext :DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options):base(options)
        {
        }
        public DbSet<Genre> genres { get; set; }
        public DbSet<Movies> Movies { get; set; }
    }
}

using Edukate_simulation_AzMPA201.Models;
using Microsoft.EntityFrameworkCore;

namespace Edukate_simulation_AzMPA201.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions options) : base(options)
    {
    }


    public DbSet<Author> Authors{ get; set; }
    public DbSet<Course>  Courses{ get; set; }

}

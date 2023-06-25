using Microsoft.EntityFrameworkCore;

namespace Kwint_It_Assignment
{
    public class beersContext:DbContext
    {
        public beersContext(DbContextOptions options):base(options)
        {

        }
        public DbSet<beer> beers { get; set; }
    }
}

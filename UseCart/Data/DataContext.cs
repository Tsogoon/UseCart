global using Microsoft.EntityFrameworkCore;

namespace UseCart.Data
{
    public class DataContext:DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        public DbSet<Cart> Carts { get; set; }
    }
}

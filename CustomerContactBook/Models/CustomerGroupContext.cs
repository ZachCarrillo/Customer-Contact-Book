using Microsoft.EntityFrameworkCore;

namespace CustomerContactBook.Models
{
    public class CustomerGroupContext : DbContext
    {
        public CustomerGroupContext(DbContextOptions<CustomerGroupContext> options)
            : base(options)
        {

        }

        public DbSet<CustomerGroup> Groups { get; set; }
    }
}
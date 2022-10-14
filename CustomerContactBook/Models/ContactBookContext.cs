using Microsoft.EntityFrameworkCore;

namespace CustomerContactBook.Models
{
    public class ContactBookContext : DbContext
    {
        public ContactBookContext(DbContextOptions<ContactBookContext> options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<GroupMember>()
                .HasKey(x => new { x.CustomerId, x.GroupId });
        }


        public DbSet<Customer> Customers { get; set; }
        public DbSet<CustomerGroup> Groups { get; set; }
        public DbSet<GroupMember> GroupMembers { get; set; }
    }
}

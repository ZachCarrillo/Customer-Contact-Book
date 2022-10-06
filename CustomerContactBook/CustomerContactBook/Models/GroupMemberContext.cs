using Microsoft.EntityFrameworkCore;

namespace CustomerContactBook.Models
{
    public class GroupMemberContext : DbContext
    {
        public GroupMemberContext(DbContextOptions<GroupMemberContext> options)
            : base(options)
        {

        }

        public DbSet<GroupMember> GroupMembers { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<GroupMember>().HasKey(x => x.CustomerId);
            modelBuilder.Entity<GroupMember>().HasKey(y => y.GroupId);
            base.OnModelCreating(modelBuilder);
        }
    }
}
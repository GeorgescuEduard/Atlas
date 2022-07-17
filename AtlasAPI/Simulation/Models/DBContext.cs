using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Simulation.Models
{
    public class DBContext : IdentityDbContext
    {
        public DBContext(DbContextOptions<DBContext> options) : base(options) { }

        public DbSet<UserAccount> UserAccount { get; set; }
        public DbSet<Issues> Issues { get; set; }
        public DbSet<Activity> Activity { get; set; }
        public DbSet<Comments> Comments { get; set; }
        public DbSet<Versions> Versions { get; set; }
        public DbSet<Projects> Projects { get; set; }
        public DbSet<Attachments> Attachments { get; set; }

    }
}

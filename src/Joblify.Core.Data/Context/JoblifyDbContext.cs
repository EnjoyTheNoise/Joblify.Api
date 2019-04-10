using Joblify.Core.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Joblify.Core.Data.Context
{
    public class JoblifyDbContext : DbContext
    {
        public JoblifyDbContext()
        {
        }

        public JoblifyDbContext(DbContextOptions<JoblifyDbContext> options) : base(options) { }

        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<ExternalProvider> ExternalProviders { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<File> Files { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=Joblify;Trusted_Connection=True;");
            }
        }
    }
}

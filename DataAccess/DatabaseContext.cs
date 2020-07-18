using DomainModels;
using System.Data.Entity;

namespace DataAccess
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext()
            : base("name=DatabaseContext")
        {
        }

        public virtual DbSet<Blog> Blogs { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>()
                .HasMany(e => e.Blogs)
                .WithRequired(e => e.Category)
                .WillCascadeOnDelete(false);
        }
    }
}

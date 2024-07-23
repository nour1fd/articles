using ArticleProject.Core;
using Microsoft.EntityFrameworkCore;

namespace ArticleProject.Data.SqlserverEF
{
    public class DBContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=localhost\SQLEXPRESS;Database=ArticlesDB;Trusted_Connection=True;TrustServerCertificate=Yes");

        }
        public DbSet<Category> Category { get; set; }
        public DbSet<Authores> Authores { get; set; }
        public DbSet<autherPost> autherPost { get; set; }


    }
}

using DataLayer.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataLayer.DbContext
{
    public class AppDbContext : Microsoft.EntityFrameworkCore.DbContext
    {
        public DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //TODO: replace to 'appsettings.json' and retrieve
            optionsBuilder.UseSqlServer(@"Server=.;Database=TrackYourLife;Trusted_Connection=True;");
        }
    }
}

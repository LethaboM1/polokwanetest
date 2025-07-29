using Microsoft.EntityFrameworkCore;
using Polokwane_surf.Server.Models;

namespace Polokwane_surf.Server.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

       public DbSet<ClientSurvey> ClientSurveys { get; set; } = null!;
       public DbSet<ContactForm> ContactForm { get; set; } = null!;


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.UseCollation("utf8mb4_unicode_ci");
            modelBuilder.HasCharSet("utf8mb4");

            modelBuilder.Entity<ClientSurvey>()
                 .ToTable("ClientSurveys"); // Use exact table name here, matching DB
            
            modelBuilder.Entity<ContactForm>()
                   .ToTable("ContactForm");
            base.OnModelCreating(modelBuilder);
        }
    }
}

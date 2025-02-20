using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using osprey_web_app.Areas.Identity.Data;
using osprey_web_app.Models;

namespace osprey_web_app.Data
{
    public class osprey_web_appContext : IdentityDbContext<osprey_web_appUser>
    {
        public osprey_web_appContext(DbContextOptions<osprey_web_appContext> options)
            : base(options)
        {
        }

        public DbSet<Mentor> Mentors { get; set; }
        public DbSet<Workshop> Workshops { get; set; }
        public DbSet<UserWorkshop> UserWorkshops { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder); // ✅ Always call the base method first

            // ✅ Ensure there is no explicit key configuration for IdentityUser
            builder.Entity<osprey_web_appUser>().ToTable("AspNetUsers"); // Ensure it's mapped correctly
        }
    }
}

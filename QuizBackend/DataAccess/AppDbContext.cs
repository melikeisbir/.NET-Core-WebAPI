using Entity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;


namespace DataAccess
{
    public class AppDbContext : IdentityDbContext<IdentityUser,Ide>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public DbSet<Ders> Dersler { get; set; } // db deki tablolar
        public DbSet<Konu> Konular { get; set; }
        public DbSet<Soru> Sorular { get; set; }



    }
}
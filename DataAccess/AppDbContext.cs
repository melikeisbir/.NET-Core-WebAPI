using Entity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DataAccess
{
    public class AppDbContext : IdentityDbContext<Kullanici,Role, string>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public DbSet<Ders> Dersler { get; set; } // db deki tablolar
        public DbSet<Konu> Konular { get; set; }
        public DbSet<Soru> Sorular { get; set; }
        public DbSet<Kullanici> Kullanicilar { get; set;}
        public DbSet<Role> Roller { get; set; }
        public DbSet<KullaniciRole> KullaniciRoles { get; set; }
        


    }
}
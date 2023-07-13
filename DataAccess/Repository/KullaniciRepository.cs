using DataAccess.Interfaces;
using Entity;
using Entity.ViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class KullaniciRepository : IKullaniciRepository
    {
        private readonly AppDbContext _appDbContext;
        private readonly UserManager<Kullanici> _userManager;

        public KullaniciRepository(AppDbContext appDbContext, UserManager<Kullanici> userManager)
        {
            _appDbContext = appDbContext;
            _userManager = userManager;
        }

        public async Task<IEnumerable<Kullanici>> getAll()
        {
            return await _appDbContext.Kullanicilar.ToListAsync();
        }

        public async Task<Kullanici> getById(string kullaniciId)
        {
            return await _appDbContext.Kullanicilar
                .FirstOrDefaultAsync(e => e.Id == kullaniciId);
        }

        public async Task<IdentityResult> Add(LoginVM kullanici)
        {
            var user = new Kullanici
            {
                UserName = kullanici.KullaniciAdi
            };

            var result = await _userManager.CreateAsync(user, kullanici.Sifre);
            return result;
        }

        public async Task<IdentityResult> Update(Kullanici kullanici)
        {
         

            var result = await _userManager.UpdateAsync(kullanici);
            return result;
        }

        public async Task<Kullanici> Delete(string kullaniciId)
        {
            var result = await _appDbContext.Kullanicilar.FindAsync(kullaniciId);
            if (result == null)
            {
                return result;
            }

            _appDbContext.Kullanicilar.Remove(result);
            await _appDbContext.SaveChangesAsync();

            return result;
        }


    }
}

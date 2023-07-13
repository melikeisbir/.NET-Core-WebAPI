using DataAccess.Interfaces;
using Entity;
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

        public KullaniciRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<IEnumerable<Kullanici>> getAll()
        {
            return await _appDbContext.Kullanicilar.ToListAsync();
        }

        public async Task<Kullanici> getById(int kullaniciId)
        {
            return await _appDbContext.Kullanicilar
                .FirstOrDefaultAsync(e => e.Id == kullaniciId);
        }

        public async Task<Kullanici> Add(Kullanici kullanici)
        {
            var result = await _appDbContext.Kullanicilar.AddAsync(kullanici);
            await _appDbContext.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<Kullanici> Update(Kullanici kullanici)
        {
            var result = await _appDbContext.Kullanicilar
                .FirstOrDefaultAsync(e => e.Id == kullanici.Id);

            if (result != null)
            {
                result.KullaniciAdi = kullanici.KullaniciAdi;
                result.Eposta = kullanici.Eposta;
                result.Sifre = kullanici.Sifre;
          

                await _appDbContext.SaveChangesAsync();

                return result;
            }

            return null;
        }

        public async Task<Kullanici> Delete(int kullaniciId)
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

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
    public class KonuRepository: IKonuRepository
    {
        private readonly AppDbContext _appDbContext;

        public KonuRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<IEnumerable<Konu>> getAll()
        {
            return await _appDbContext.Konular.ToListAsync();
        }

        public async Task<Konu> getById(int konuId)
        {
            return await _appDbContext.Konular
                .FirstOrDefaultAsync(e => e.Id == konuId);
        }

        public async Task<Konu> Add(Konu konu)
        {
            var result = await _appDbContext.Konular.AddAsync(konu);
            await _appDbContext.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<Konu> Update(Konu konu)
        {
            var result = await _appDbContext.Konular
                .FirstOrDefaultAsync(e => e.Id == konu.Id);

            if (result != null)
            {
                result.DersId = konu.DersId;
                result.KonuAdi = konu.KonuAdi;

                await _appDbContext.SaveChangesAsync();

                return result;
            }

            return null;
        }

        public async Task<Konu> Delete(int konuId)
        {
            var result = await _appDbContext.Konular.FindAsync(konuId);
            if (result == null)
            {
                return result;
            }

            _appDbContext.Konular.Remove(result);
            await _appDbContext.SaveChangesAsync();

            return result;
        }

        public async Task<Konu> getByDersId(int dersId)
        {
            return await _appDbContext.Konular
                .FirstOrDefaultAsync(e => e.DersId == dersId);
        }
    }
}

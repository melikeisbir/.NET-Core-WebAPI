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
    public class SoruRepository : ISoruRepository
    {
        private readonly AppDbContext _appDbContext;

        public SoruRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<IEnumerable<Soru>> getAll()
        {
            return await _appDbContext.Sorular.ToListAsync();
        }

        public async Task<Soru> getById(int soruId)
        {
            return await _appDbContext.Sorular
                .FirstOrDefaultAsync(e => e.Id == soruId);
        }

        public async Task<Soru> Add(Soru soru)
        {
            var result = await _appDbContext.Sorular.AddAsync(soru);
            await _appDbContext.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<Soru> Update(Soru soru)
        {
            var result = await _appDbContext.Sorular
                .FirstOrDefaultAsync(e => e.Id == soru.Id);

            if (result != null)
            {
                result.KonuId = soru.KonuId;
                result.SoruText = soru.SoruText;
                result.CevapA = soru.CevapA;
                result.CevapB = soru.CevapB;
                result.CevapC = soru.CevapC;
                result .CevapD = soru.CevapD;
                result.CevapE = soru.CevapE;
                result.DogruCevap = soru.DogruCevap;



                await _appDbContext.SaveChangesAsync();

                return result;
            }

            return null;
        }

        public async Task<Soru> Delete(int soruId)
        {
            var result = await _appDbContext.Sorular.FindAsync(soruId);
            if (result == null)
            {
                return result;
            }

            _appDbContext.Sorular.Remove(result);
            await _appDbContext.SaveChangesAsync();

            return result;
        }

        public async Task<Soru> getByKonuId(int konuId)
        {
            return await _appDbContext.Sorular
                .FirstOrDefaultAsync(e => e.KonuId == konuId);
        }
    }
}

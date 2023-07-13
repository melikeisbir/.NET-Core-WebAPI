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
    public class DersRepository : IDersRepository
    {
        private readonly AppDbContext _appDbContext;

        public DersRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<IEnumerable<Ders>> getAll()
        {
            return await _appDbContext.Dersler.ToListAsync();
        }

        public async Task<Ders> getById(int dersId)
        {
            return await _appDbContext.Dersler
                .FirstOrDefaultAsync(e => e.Id == dersId);
        }

        public async Task<Ders> Add(Ders ders)
        {
            var result = await _appDbContext.Dersler.AddAsync(ders);
            await _appDbContext.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<Ders> Update(Ders ders)
        {
            var result = await _appDbContext.Dersler
                .FirstOrDefaultAsync(e => e.Id == ders.Id);

            if (result != null)
            {
                result.DersAdi = ders.DersAdi;

                await _appDbContext.SaveChangesAsync();

                return result;
            }

            return null;
        }

        public async Task<Ders> Delete(int dersId)
        {
            var result = await _appDbContext.Dersler.FindAsync(dersId);
            if (result == null)
            {
                return result;
            }

            _appDbContext.Dersler.Remove(result);
            await _appDbContext.SaveChangesAsync();

            return result;
        }

    }
}

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
    public class RoleRepository : IRoleRepository
    {
        private readonly AppDbContext _appDbContext;

        public RoleRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<IEnumerable<Role>> getAll()
        {
            return await _appDbContext.Roller.ToListAsync();
        }

        public async Task<Role> getById(string RoleId)
        {
            return await _appDbContext.Roller
                .FirstOrDefaultAsync(e => e.Id == RoleId);
        }

        public async Task<Role> Add(Role Role)
        {
            Role.Id = Guid.NewGuid().ToString();

            var result = await _appDbContext.Roller.AddAsync(Role);
            await _appDbContext.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<Role> Update(Role Role)
        {
            var result = await _appDbContext.Roller
                .FirstOrDefaultAsync(e => e.Id == Role.Id);

            if (result != null)
            {
                result.Name = Role.Name;

                await _appDbContext.SaveChangesAsync();

                return result;
            }

            return null;
        }

        public async Task<Role> Delete(string RoleId)
        {
            var result = await _appDbContext.Roller.FindAsync(RoleId);
            if (result == null)
            {
                return result;
            }

            _appDbContext.Roller.Remove(result);
            await _appDbContext.SaveChangesAsync();

            return result;
        }

  
    }
}

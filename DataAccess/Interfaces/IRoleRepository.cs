using Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Interfaces
{
    public interface IRoleRepository
    {
        Task<IEnumerable<Role>> getAll();
        Task<Role> getById(string RoleId);
        Task<Role> Add(Role Role);
        Task<Role> Update(Role Role);
        Task<Role> Delete(string RoleId);
    }
}


using Entity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Interfaces
{
    public interface IUserRoleRepository
    {
     

        Task<EntityEntry<IdentityUserRole<string>>> Add(string userID, string roleID);
        EntityEntry<IdentityUserRole<string>> Update(string userID, string roleID);
        EntityEntry<IdentityUserRole<string>> Delete(string userID, string roleID);
    }
}


using DataAccess.Interfaces;
using Entity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class UserRoleRepository : IUserRoleRepository
    {
        private readonly AppDbContext _appDbContext;



        public UserRoleRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

      

        //public async Task<IList<string>?> getById(string userID, string roleID)
        //{
        //    var identityuserrole = new KullaniciRole { RoleId = roleID, UserId = userID };
        //    var result = await _appDbContext.UserRoles.GetAsyncEnumerator(identityuserrole);

        //    return result;

        //    //return await _appDbContext.KullaniciRoller
        //    //    .FirstOrDefaultAsync(e => e.UserId == userID && e.RoleId == roleID);
        //}

        public async Task<EntityEntry<IdentityUserRole<string>>> Add(string userID, string roleID)

        {
            var identityuserrole = new KullaniciRole { RoleId = roleID, UserId = userID };
            var result = await _appDbContext.UserRoles.AddAsync(identityuserrole);

            return result;
        }

        public  EntityEntry<IdentityUserRole<string>> Update(string userID, string roleID)
        {
            var identityuserrole = new KullaniciRole { RoleId = roleID, UserId = userID };
            var result =  _appDbContext.UserRoles.Update(identityuserrole);

            return result;
        }

        public  EntityEntry<IdentityUserRole<string>> Delete(string userID, string roleID)
        {
            var identityuserrole = new KullaniciRole { RoleId = roleID, UserId = userID };
            var result =  _appDbContext.UserRoles.Remove(identityuserrole);

            return result;
        }


    }
}
//public async Task<UserRole> CreateUser(string username, string password)
//{
//    var user = new UserRole { UserName = username };

//    // Parolayı hashlemek için Identity Framework'ün PasswordHasher sınıfını kullanıyoruz
//    var passwordHasher = new PasswordHasher<UserRole>();
//    user.PasswordHash = passwordHasher.HashPassword(user, password);

//    // Kullanıcıyı kaydediyoruz
//    var result = await _userManager.CreateAsync(user);

//    if (result.Succeeded)
//    {
//        return user;
//    }
//    else
//    {
//        // Kullanıcı oluşturma başarısız olduysa hata işleme yapılabilir
//        throw new Exception("Kullanıcı oluşturma işlemi başarısız.");
//    }
//}
//}








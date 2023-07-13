using Entity;
using Entity.ViewModel;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Interfaces
{
    public interface IKullaniciRepository
    {
        Task<IEnumerable<Kullanici>> getAll();
        Task<Kullanici> getById(string kullaniciId);
        Task<IdentityResult> Add(LoginVM kullanici);

        Task<IdentityResult> Update(Kullanici kullanici);
        Task<Kullanici> Delete(string kullaniciId);
    }
}


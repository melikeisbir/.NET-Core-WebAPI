using Entity;
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
        Task<Kullanici> getById(int kullaniciId);
        Task<Kullanici> Add(Kullanici kullanici);
        Task<Kullanici> Update(Kullanici kullanici);
        Task<Kullanici> Delete(int kullaniciId);
    }
}


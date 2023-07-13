using Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Interfaces
{
    public interface ISoruRepository   //bir soru (Question) veri erişim katmanı veya servis katmanı arabirimini temsil ediyor.
                                       //Bu kod, bir veritabanından veya başka bir veri kaynağından soru nesnelerini almak,
                                       //belirli bir soru veya konu ID'ye göre soru getirmek, yeni bir soru eklemek, mevcut bir
                                       //soruyu güncellemek ve bir soruyu silmek için kullanılabilir metodları içerir.
    {
        Task<IEnumerable<Soru>> getAll(); //metodu, tüm soru nesnelerini almak için kullanılır ve bir IEnumerable koleksiyonunda
                                          //(örneğin, bir liste) dönen bir asenkron işlemi temsil eder.
                                          //Asenkron (asynchronous), bir programın işlemlerini ardışık olarak sırasıyla işleme koymak yerine,
                                          //işlemleri eşzamanlı olarak gerçekleştirebilen bir programlama yaklaşımıdır. 
        Task<Soru> getById(int soruId); // belirli bir soru ID'sine göre soru nesnesini almak için kullanılır ve Soru türünde bir asenkron
                                        // işlemi temsil eder.
        Task<Soru> getByKonuId(int konuId); // belirli bir konu ID'sine göre soru nesnesini almak için kullanılır ve Soru türünde bir asenkron
                                            // işlemi temsil eder.
        Task<Soru> Add(Soru soru); //yeni soru nesnesi ekleme
        Task<Soru> Update(Soru soru); // güncelleme
        Task<Soru> Delete(int soruId); //silme
    }
}

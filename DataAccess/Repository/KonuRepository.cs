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
    public class KonuRepository: IKonuRepository   //"KonuRepository" sınıfının, konu verilerini işlemek için kullanılan bir veritabanı veya veri kaynağı üzerindeki işlemleri yürüten bir sınıf olduğunu söyleyebiliriz.
                                                   // KonuRepository sınıfı "IKonuRepository" arayüzünü uygulayan bir sınıftır.Arayüzler, sınıflar arasında bir sözleşme sağlamak için kullanılan bir mekanizmadır. Bir sınıf,
                                                   // bir arayüzü uyguladığında, arayüzde tanımlanan tüm yöntemleri içermek zorundadır.
    {
        private readonly AppDbContext _appDbContext;  //. "private readonly" ifadesi, bu alanın sadece "KonuRepository" sınıfı içinde erişilebilir olacağını ve değerinin başlangıçta atanıp daha sonra değiştirilemeyeceğini
                                                      //belirtir.  AppDbContext  "KonuRepository" sınıfının bir örneği oluşturulurken veya başlatılırken kullanılacak bir nesneyi temsil eder.


        public KonuRepository(AppDbContext appDbContext) // "KonuRepository" sınıfının bir yapıcı metodu (constructor) olarak tanımlanmış. Yapıcı metotlar, bir sınıfın bir örneği (instance) oluşturulduğunda otomatik olarak
                                                         // çalışan özel metotlardır. "_appDbContext" alanı, yapıcıya geçirilen "appDbContext" nesnesine atama yapar ve daha sonra bu nesneyi "KonuRepository" sınıfının diğer
                                                         // yöntemlerinde kullanabilir.  "AppDbContext" nesnesi, "_appDbContext" alanı üzerinden "KonuRepository" sınıfının diğer yöntemlerine erişim sağlamak ve veritabanı
                                                         // işlemlerini gerçekleştirmek için kullanılabilir.
        {
            _appDbContext = appDbContext;
        }



        public async Task<IEnumerable<Konu>> getAll()  // "IEnumerable<Konu>" ise döndürülen değerin "Konu" türündeki öğelerden oluşan bir koleksiyon veya liste olduğunu belirtir. "getAll" metodu, "Konu" türündeki verilerin
                                                       // tamamını asenkron olarak "Konular" tablosundan getirir ve bu verileri bir liste olarak döndürür. Bu yöntem, "KonuRepository" sınıfının kullanıldığı yerde tüm konu
                                                       // verilerine erişmek için çağrılabilir.
        {
            return await _appDbContext.Konular.ToListAsync();
        }



        public async Task<Konu> getById(int konuId)  //FirstOrDefaultAsync() yöntemi, belirli bir koşula uyan ilk öğeyi asenkron olarak getirir. Konular tablosunda  "Id" özelliği "konuId" parametresine eşit olan ilk "Konu"
                                                     //öğesini getirir. , "getById" metodu, belirli bir "konuId" değeriyle eşleşen bir "Konu" öğesini asenkron olarak "Konular" tablosundan getirir ve bu öğeyi döndürür. Bu yöntem,
                                                     //"KonuRepository" sınıfının kullanıldığı yerde belirli bir konu öğesine erişmek için çağrılabilir.
        {
            return await _appDbContext.Konular
                .FirstOrDefaultAsync(e => e.Id == konuId);
        }




        public async Task<Konu> Add(Konu konu)   // Metot,  "Add" adını taşır ve bir "Konu" öğesini veritabanına ekler.  "return result.Entity" ifadesi kullanılır. Bu ifade, eklenen "Konu" öğesinin geri döndürülmesini sağlar.
                                                 // "result.Entity", "AddAsync" yöntemi tarafından eklenen öğenin referansını temsil eder.  "Add" metodu, "Konu" öğesini asenkron olarak veritabanına ekler, değişiklikleri kaydeder
                                                 // ve eklenen öğeyi döndürür. Bu yöntem, "KonuRepository" sınıfının kullanıldığı yerde yeni bir konu öğesi eklemek için çağrılabilir.
        {
            var result = await _appDbContext.Konular.AddAsync(konu);
            await _appDbContext.SaveChangesAsync();
            return result.Entity;
        }




        public async Task<Konu> Update(Konu konu) //Eklediğimiz öğelerin güncellendiği kısım
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





        public async Task<Konu> Delete(int konuId)  //silindiği kısım
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






        public async Task<Konu> getByDersId(int dersId) // "getByDersId" metodu, belirli bir "dersId" değeriyle eşleşen bir "Konu" öğesini asenkron olarak "Konular" tablosundan getirir ve bu öğeyi döndürür.
                                                        // Bu yöntem, "KonuRepository" sınıfının kullanıldığı yerde belirli bir dersin konu öğesine erişmek için çağrılabilir.
        {
            return await _appDbContext.Konular    // Bu ifade, "_appDbContext" alanı üzerinden "Konular" tablosunda, "dersId" değerine sahip ilk öğeyi asenkron olarak arar ve bulur. 
                .FirstOrDefaultAsync(e => e.DersId == dersId);
        }
    }
}

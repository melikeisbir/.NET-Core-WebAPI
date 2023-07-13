using Entity;


namespace DataAccess.Interfaces
{
    public interface IKonuRepository
    {
        Task<IEnumerable<Konu>> getAll();
        Task<Konu> getById(int konuId);
        Task<Konu> getByDersId(int dersId); //belirli bir ders kimliğiyle ilişkili bir Konu nesnesini almak için
                                            //kullanılan bir metodun tanımı
        Task<Konu> Add(Konu konu);
        Task<Konu> Update(Konu konu);
        Task<Konu> Delete(int konuId);
    }
}

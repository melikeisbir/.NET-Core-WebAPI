using Entity;

namespace DataAccess.Interfaces
{
    public interface IDersRepository
    {
        Task<IEnumerable<Ders>> getAll();
        Task<Ders> getById(int dersId);
        Task<Ders> Add(Ders ders);
        Task<Ders> Update(Ders ders);
        Task<Ders> Delete(int dersId);
    }
}

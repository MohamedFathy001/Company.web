using Company.Data.Entities;

namespace Company.Repository.Interfacies
{
    public interface IGenericRepo<T> where T : BaseEntity
    {
        T GetbyId(int id);
        IEnumerable<T> GetAll();
        void Add(T entity);
        void Updates(T entity);
        void Delete(int id);
    }
}

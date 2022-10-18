using System.Collections.Generic;
using System.Threading.Tasks;

namespace Pharmacy.Data.Abstract
{
    public interface IRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAll();
        Task<T> GetById(int id);
        Task Insert(T obj);
        Task Update(T obj);
        Task Delete(int id);
    }
}

using System.Collections.Generic;
using System.Threading.Tasks;

namespace Pharmacy.Data.Abstract
{
    public interface IRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAll();
        Task<T> GetById(int id);
        Task<string> Insert(T item);
        Task<string> Update(T item);
        Task<string> Delete(int id);
    }
}

using System.Collections.Generic;
using System.Threading.Tasks;

namespace Pharmacy.Data.Abstract
{
    public interface IRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAll();
        Task<T> GetById(int id);
        Task<bool> Insert(T obj);
        Task<bool> Update(T obj);
        Task<bool> Delete(int id);
        //IEnumerable<T> Search(string query);
    }
}

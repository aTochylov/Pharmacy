using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Pharmacy.Services
{
    public interface IRepository<T>
    {
        int Add(T item);
        int Update(T item);
        int Delete(T item);
        T Get(int id);
        IEnumerable<T> GetItems();
        IEnumerable<T> GetSearchResults(string query);
    }
}

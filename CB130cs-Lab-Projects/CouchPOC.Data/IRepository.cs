using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CouchPOC.Data
{
    public interface IRepository<T>
    {
        Task<T> GetAsync(string id);
        T Create(string id, T item);
        T Update(string id, T item);
        void Delete(string id);
        Task<IEnumerable<T>> GetAllAsync();
    }
}

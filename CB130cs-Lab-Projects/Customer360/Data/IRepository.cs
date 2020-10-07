using System.Collections.Generic;

namespace Customer360.Data
{
    public interface IRepository<T>
    {
        T Get(string id);
        T Create(string id, T item);
        T Update(string id, T item);
        void Delete(string id);
        IEnumerable<T> GetAll();
        IEnumerable<T> GetByCity(string city);
    }
}
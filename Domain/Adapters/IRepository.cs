using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Adapters
{
    public interface IRepository<T>
    {
        Task<IEnumerable<T>> GetAll();
        Task<T> Get(int id);

        Task<T> Get(string key);

        Task<bool> Insert(T item);

        Task<bool> Update(T item);

        Task<bool> Delete(int id);
    }
}

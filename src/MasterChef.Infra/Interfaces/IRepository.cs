using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasterChef.Infra.Interfaces
{
    public interface IRepository<T> where T : class
    {
        Task<T> Save(T entity);
        Task<T> Update(T entity);
        Task<T> GetById(int id);
        Task<List<T>> GetAll();
        Task<bool> Delete(int id);
        List<T> GetAll(Func<T, bool> func);

    }
}

using System.Collections.Generic;
using System.Threading.Tasks;

namespace TechChallenge.Domain.Repository
{
    public interface IRepository<T> where T : class
    {
        Task<List<T>> GetNewAll();
        Task<T> Get(long id);
        Task<T> Add(T entity);
        Task<T> Update(T entity);
        Task<T> Delete(int id);
    }
}

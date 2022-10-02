using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace CarRental.API.Repository.IRepository
{
    public interface IRepositoryAsync<T> where T : class
    {
        Task<T> GetAsync(int id);

        Task<IEnumerable<T>> GetAllAsync(
            Expression<Func<T, bool>> filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderby = null,
            string includeProperties = null
            );

        Task<T> GetFirstOrDefaultAsync(
             Expression<Func<T, bool>> filter = null,
             string includeProperties = null
             );

        Task AddAsync(T entity);

        Task RemoveAsync(int id);

        Task RemoveAsync(T entity);

        Task RemoveRangeAsync(IEnumerable<T> entity);

        Task Save();

        Task<bool> DoesRecordExist(int id);
    }
}

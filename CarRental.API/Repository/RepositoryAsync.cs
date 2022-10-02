using CarRental.API.Data;
using CarRental.API.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace CarRental.API.Repository
{
    public class RepositoryAsync<T> : IRepositoryAsync<T> where T : class
    {
        private readonly CarRentalDbContext _db;
        internal DbSet<T> dbset;

        public RepositoryAsync(CarRentalDbContext db)
        {
            _db = db;
            this.dbset = _db.Set<T>();
        }

        public async Task AddAsync(T entity)
        {
            await dbset.AddAsync(entity);
        }

        public async Task<T> GetAsync(int id)
        {
            return await dbset.FindAsync(id);
        }

        public async Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, string includeProperties = null)
        {
            IQueryable<T> query = dbset;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (includeProperties != null)
            {
                foreach (var includeProp in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProp);
                }
            }

            if (orderBy != null)
            {
                return await orderBy(query).ToListAsync();
            }
            return await query.ToListAsync();
        }

        public async Task<T> GetFirstOrDefaultAsync(Expression<Func<T, bool>> filter = null, string includeProperties = null)
        {
            IQueryable<T> query = dbset;
            if (filter != null)
            {
                query = query.Where(filter);
            }
            if (includeProperties != null)
            {
                foreach (var includeProp in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProp);
                }
            }
            return await query.FirstOrDefaultAsync();

        }

        public async Task<bool> DoesRecordExist(int id)
        {
            T entity = await dbset.FindAsync(id);
            if (entity == null)
            {
                return false;
            }
            return true;
        }

        public async Task RemoveAsync(int id)
        {
            T entity = await dbset.FindAsync(id);
            await RemoveAsync(entity);
        }

        public async Task RemoveAsync(T entity)
        {
            dbset.Remove(entity);
        }

        public async Task RemoveRangeAsync(IEnumerable<T> entity)
        {
            dbset.RemoveRange(entity);
        }

        public async Task Save()
        {
            await _db.SaveChangesAsync();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WeatherForecast.Core.Entities;

namespace WeatherForecast.Core.Abstractions.DAL.Repositories.Base
{
    public interface IBaseRepository<TEntity> where TEntity : BaseEntity
    {
        IQueryable<TEntity> GetAll();
        IQueryable<TEntity> Find(Expression<Func<TEntity, bool>> predicate);
        Task<TEntity> GetByIdAsync(object id, CancellationToken cancellationToken = default);
        Task<IEnumerable<TEntity>> GetAsync(Expression<Func<TEntity, bool>> filter = null, CancellationToken cancellationToken = default, params Expression<Func<TEntity, object>>[] includes);
        Task<IEnumerable<TEntity>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<TEntity> GetSingleAsync(Expression<Func<TEntity, bool>> filter = null, CancellationToken cancellationToken = default, params Expression<Func<TEntity, object>>[] includes);
        Task<bool> AnyAsync(Expression<Func<TEntity, bool>> filter = null, CancellationToken cancellationToken = default);
        Task InsertAsync(TEntity entity, CancellationToken cancellationToken = default);
        Task InsertRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default);
        void Insert(TEntity entity);
        void InsertRange(IEnumerable<TEntity> entities);
        void Update(TEntity entity);
        void UpdateRange(IEnumerable<TEntity> entities);
        void Delete(TEntity entity);
        void DeleteRange(IEnumerable<TEntity> entities);

        void SaveChanges();
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);




    }
}

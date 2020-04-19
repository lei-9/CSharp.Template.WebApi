using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using CSharp.Framework.Pager;
using CSharp.Template.Repositories.Specification;
using CSharp.Template.Repositories.Specification.Interfaces;
using Microsoft.EntityFrameworkCore;
using MySql.Data.MySqlClient;

namespace CSharp.Template.Repositories.Data
{
    public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class
    {
        private readonly EfContext _efContext;

        public BaseRepository(EfContext dbContext)
        {
            _efContext = dbContext;
        }

        private DbSet<TEntity> Db => _efContext.Set<TEntity>();

        public MySqlConnection Connection => ConnectionManager.Connection;
        public Lazy<IQueryable<TEntity>> Entities => new Lazy<IQueryable<TEntity>>(_efContext.Set<TEntity>().AsQueryable());

        #region add

        public virtual async Task Add(TEntity entity)
        {
            await _efContext.Set<TEntity>().AddAsync(entity);
            await _efContext.SaveChangesAsync();
        }

        public virtual async Task AddRange(IEnumerable<TEntity> list)
        {
            await _efContext.Set<TEntity>().AddRangeAsync(list);
        }

        #endregion

        #region delete

        public virtual async Task Delete(TEntity entity)
        {
            Db.Remove(entity);
            await _efContext.SaveChangesAsync();
        }

        public virtual async Task Delete(IEnumerable<TEntity> list)
        {
            Db.RemoveRange(list);
            await _efContext.SaveChangesAsync();
        }

        #endregion

        #region update

        public virtual async Task Update(TEntity entity, List<string> updateFields = null)
        {
            _efContext.Attach(entity).State = EntityState.Modified;
            await _efContext.SaveChangesAsync();
        }

        public virtual async Task Update(IEnumerable<TEntity> list, List<string> updateFields = null)
        {
            foreach (var entity in list)
            {
                _efContext.Attach(entity).State = EntityState.Modified;
            }

            await _efContext.SaveChangesAsync();
        }

        #endregion

        #region query

        public virtual async Task<TEntity> GetById(object id)
        {
            return await Db.FindAsync(id);
        }

        // public virtual async Task<IEnumerable<TEntity>> ListAsync(PagingInfo pagingInfo)
        // {
        //     
        // }
        
        public virtual async Task<IEnumerable<TEntity>> ListAsync(PagingInfo pagingInfo)
        {
            throw new NotImplementedException();
        }

        public virtual async Task<IEnumerable<TResult>> ListAsync<TResult>(PagingInfo pagingInfo, Expression<Func<TEntity, TResult>> selector)
        {
            throw new NotImplementedException();
            
            await Db.Select(selector).ToListAsync();
        }

        public async Task<IReadOnlyList<TEntity>> ListAsync(ISpecification<TEntity> spec)
        {
            return await ApplySpecification(spec).ToListAsync();
        }
        
        public async Task<IReadOnlyList<TResult>> ListAsync<TResult>(ISpecification<TEntity> spec, Expression<Func<TEntity, TResult>> selector)
        {
            return await ApplySpecification(spec).Select(selector).ToListAsync();
        }


        public async Task<int> CountAsync(ISpecification<TEntity> spec)
        {
            return await ApplySpecification(spec).CountAsync();
        }

        private IQueryable<TEntity> ApplySpecification(ISpecification<TEntity> spec)
        {
            return SpecificationEvaluator<TEntity>.GetQuery(Db, spec);
        }

        #endregion
    }
}
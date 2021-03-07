using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using VehicleMaintenance.Core.Entities;
using Microsoft.EntityFrameworkCore;
using VehicleMaintenance.Core.DataAccess;

namespace VehicleMaintenance.Core.DataAccess.EntityFramework
{
    public class efRepositoryBase<TEntity> : IEntityRepository<TEntity>
        where TEntity : class, IEntity, new()
    {
        private readonly DbContext _context;
        public DbSet<TEntity> dbSet { get; }

        public efRepositoryBase(DbContext context)
        {
            _context = context;
            dbSet = context.Set<TEntity>();
        }
        public void Add(TEntity entity)
        {
            var activeEntity = dbSet.Add(entity);
            activeEntity.State = EntityState.Added;
        }

        public void Delete(TEntity entity)
        {
            var activeEntity = _context.Entry(entity);
            activeEntity.State = EntityState.Deleted;
        }

        public TEntity Get(Expression<Func<TEntity, bool>> condition)
        {
            return _context.Set<TEntity>().SingleOrDefault(condition);
        }

        public TEntity GetDetached(Expression<Func<TEntity, bool>> condition)
        {
            var result = _context.Set<TEntity>().SingleOrDefault(condition);
            var activeEntity = _context.Entry(result);
            activeEntity.State = EntityState.Detached;
            return result;
        }

        public List<TEntity> GetList(Expression<Func<TEntity, bool>> condition = null)
        {
            var list = condition == null ?
                _context.Set<TEntity>()
                .ToList() :
                _context.Set<TEntity>().Where(condition)
                .ToList();

            return list;
        }

        public void Update(TEntity entity)
        {
            var activeEntity = _context.Entry(entity);
            activeEntity.State = EntityState.Modified;
        }
        public void Save()
        {
            _context.SaveChanges();
        }

        public bool SaveChanges()
        {
            try
            {
                Save();
                return true;
            }
            catch (Exception exp)
            {
                var message = exp.Message;
                return false;
            }
        }
    }
}
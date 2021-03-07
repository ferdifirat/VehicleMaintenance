using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using VehicleMaintenance.Core.Entities;

namespace VehicleMaintenance.Core.DataAccess
{
    public interface IEntityRepository<T> where T : class, IEntity, new()
    {
        DbSet<T> dbSet { get; }
        List<T> GetList(Expression<Func<T, bool>> condition = null);
        T Get(Expression<Func<T, bool>> condition);
        T GetDetached(Expression<Func<T, bool>> condition);
        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);
        void Save();
        bool SaveChanges();
    }
}
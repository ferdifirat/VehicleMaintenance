using System;
using VehicleMaintenance.Core.DataAccess.EntityFramework;
using VehicleMaintenance.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Logging;

namespace VehicleMaintenance.Core.DataAccess.UnitOfWork
{
    public class efUnitOfWork : IUnitOfWork
    {
        private readonly DbContext _context;
        private IDbContextTransaction _transation;
        private bool _disposed;
        public efUnitOfWork(DbContext context)
        {
            _context = context;
        }

        public IEntityRepository<T> GetRepository<T>() where T : class, IEntity, new()
        {
            return new efRepositoryBase<T>(_context);
        }
        public bool BeginNewTransaction()
        {
            var result = false;
            try
            {
                _transation = _context.Database.BeginTransaction();
                result = true;
            }
            catch (Exception exp)
            {
            }

            return result;
        }
        public bool RollBackTransaction()
        {
            var result = false;
            try
            {
                _transation.Rollback();
                _transation = null;
                result = true;
            }
            catch (Exception exp)
            {
            }

            return result;
        }

        public bool Commit()
        {
            var result = false;
            var transaction = _transation != null ? _transation : _context.Database.BeginTransaction();
            try
            {
                using (transaction)
                {
                    _context.SaveChanges();
                    transaction.Commit();
                }
                result = true;
            }
            catch (Exception exp)
            {
                transaction.Rollback();
            }

            return result;
        }
        protected virtual void Dispose(bool disposing)
        {
            if (!this._disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            this._disposed = true;
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        public bool SaveChanges()
        {
            var result = false;
            try
            {
                _context.SaveChanges();
                Commit();
                result = true;
            }
            catch (Exception exp)
            {
            }

            return result;
        }
    }
}
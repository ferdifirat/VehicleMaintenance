using System;
using VehicleMaintenance.Core.Entities;

namespace VehicleMaintenance.Core.DataAccess
{
    public interface IUnitOfWork : IDisposable
    {
        IEntityRepository<T> GetRepository<T>() where T : class, IEntity, new();
        bool BeginNewTransaction();
        bool RollBackTransaction();
        bool SaveChanges();
        bool Commit();
    }
}
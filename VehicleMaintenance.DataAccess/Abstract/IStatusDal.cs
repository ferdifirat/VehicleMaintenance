using System;
using System.Collections.Generic;
using System.Text;
using VehicleMaintenance.Core.DataAccess;
using VehicleMaintenance.Entity.Concrete;

namespace VehicleMaintenance.DataAccess.Abstract
{
    public interface IStatusDal : IEntityRepository<Status>
    {
    }
}

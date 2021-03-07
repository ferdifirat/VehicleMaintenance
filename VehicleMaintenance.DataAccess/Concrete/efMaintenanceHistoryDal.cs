using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using VehicleMaintenance.Core.DataAccess.EntityFramework;
using VehicleMaintenance.DataAccess.Abstract;
using VehicleMaintenance.Entity.Concrete;


namespace VehicleMaintenance.DataAccess.Concrete
{
    public class efMaintenanceHistoryDal : efRepositoryBase<MaintenanceHistory>, IMaintenanceHistoryDal
    {
        public efMaintenanceHistoryDal(DbContext dbContext) : base(dbContext)
        {
        }
    }
}

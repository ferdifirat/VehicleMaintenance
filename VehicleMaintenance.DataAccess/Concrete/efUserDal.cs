using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using VehicleMaintenance.Core.DataAccess.EntityFramework;
using VehicleMaintenance.DataAccess.Abstract;
using VehicleMaintenance.Entity.Concrete;


namespace VehicleMaintenance.DataAccess.Concrete
{
    public class efUserDal : efRepositoryBase<User>, IUserDal
    {
        public efUserDal(DbContext dbContext) : base(dbContext)
        {
        }
    }
}

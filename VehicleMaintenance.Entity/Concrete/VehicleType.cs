using System;
using System.Collections.Generic;
using System.Text;
using VehicleMaintenance.Core.Entities;

namespace VehicleMaintenance.Entity.Concrete
{
    public class VehicleType : BaseEntity, IEntity
    {
        public string Name { get; set; }
    }
}

using VehicleMaintenance.Core.Entities;

namespace VehicleMaintenance.Entity.Concrete
{
    public class Vehicle : BaseEntity, IEntity
    {
        public string PlateNo { get; set; }
        public virtual User User { get; set; }
        public virtual VehicleType VehicleType { get; set; }
        public string Name { get; set; }


    }
}

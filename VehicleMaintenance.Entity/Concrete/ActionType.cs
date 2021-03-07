using VehicleMaintenance.Core.Entities;

namespace VehicleMaintenance.Entity.Concrete
{
    public class ActionType : BaseEntity, IEntity
    {
        public string Name { get; set; }
    }
}

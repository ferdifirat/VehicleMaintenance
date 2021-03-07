using VehicleMaintenance.Core.Entities;

namespace VehicleMaintenance.Entity.Concrete
{
    public class MaintenanceHistory : BaseEntity, IEntity
    {
        public virtual Maintenance Maintenance { get; set; }
        public virtual ActionType ActionType { get; set; }
        public string Text { get; set; }
    }
}

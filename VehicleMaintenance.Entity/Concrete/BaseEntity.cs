using System;

namespace VehicleMaintenance.Entity.Concrete
{
    public abstract class BaseEntity
    {
        public int ID { get; set; }
        public TimeSpan CreateDate { get; set; }
        public virtual User CreatedByUser { get; set; }
        public TimeSpan ModifyDate { get; set; }
        public int ModifiedBy { get; set; }
        public bool IsDeleted { get; set; } = false;

    }
}

using System;
using VehicleMaintenance.Core.Entities;

namespace VehicleMaintenance.Entity.Concrete
{
    public class Maintenance : BaseEntity, IEntity
    {
        public virtual Vehicle Vehicle { get; set; }
        public virtual User User { get; set; }
        public string Description { get; set; }
        public virtual PictureGroup PictureGroup { get; set; }
        public DateTime ExpectedTimeToFix { get; set; }
        public virtual User ResponsibleUser { get; set; }
        public string LocationLongitude { get; set; }
        public string LocationLatitude { get; set; }
        public virtual Status Status { get; set; }

    }
}

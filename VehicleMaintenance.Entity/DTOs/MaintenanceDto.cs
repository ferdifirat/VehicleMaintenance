using System;
using System.Collections.Generic;
using System.Text;
using VehicleMaintenance.Entity.Concrete;

namespace VehicleMaintenance.Entity.DTOs
{
    public class MaintenanceDto
    {
        public int ID { get; set; }
        public TimeSpan CreateDate { get; set; }
        public virtual User CreatedByUser { get; set; }
        public TimeSpan ModifyDate { get; set; }
        public int ModifiedBy { get; set; }
        public bool IsDeleted { get; set; } = false;
        public string Description { get; set; }
        public DateTime ExpectedTimeToFix { get; set; }
        public int ResponsibleUserID { get; set; }
        public string LocationLongitude { get; set; }
        public string LocationLatitude { get; set; }
        public virtual Vehicle Vehicle { get; set; }
        public virtual User User { get; set; }
        public virtual User ResponsibleUser { get; set; }
        public virtual PictureGroup PictureGroup { get; set; }
        public virtual Status Status { get; set; }
    }
}

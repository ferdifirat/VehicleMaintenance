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
        public virtual UserDto CreatedByUser { get; set; }
        public TimeSpan ModifyDate { get; set; }
        public int ModifiedBy { get; set; }
        public bool IsDeleted { get; set; } = false;
        public string Description { get; set; }
        public DateTime ExpectedTimeToFix { get; set; }
        public string LocationLongitude { get; set; }
        public string LocationLatitude { get; set; }
        public virtual VehicleDto Vehicle { get; set; }
        public int VehicleID { get; set; }
        public virtual UserDto User { get; set; }
        public int UserID { get; set; }
        public virtual UserDto ResponsibleUser { get; set; }
        public int ResponsibleUserID { get; set; }
        public virtual PictureGroupDto PictureGroup { get; set; }
        public int PictureGroupID { get; set; }
        public virtual StatusDto Status { get; set; }
        public int StatusID { get; set; }


        public MaintenanceDto Map(Concrete.Maintenance type)
        {
            this.ID = type.ID;
            this.CreateDate = type.CreateDate;
            this.CreatedByUser = new UserDto().Map(type.CreatedByUser);
            this.ModifyDate = type.ModifyDate;
            this.ModifiedBy = type.ModifiedBy;
            this.IsDeleted = type.IsDeleted;
            this.Description = type.Description;
            this.ExpectedTimeToFix = type.ExpectedTimeToFix;
            this.LocationLongitude = type.LocationLongitude;
            this.LocationLatitude = type.LocationLatitude;
            this.Vehicle = new VehicleDto().Map(type.Vehicle);
            this.User = new UserDto().Map(type.User);
            this.ResponsibleUser= new UserDto().Map(type.ResponsibleUser);
            this.PictureGroup= new PictureGroupDto().Map(type.PictureGroup);
            this.Status= new StatusDto().Map(type.Status);
            return this;
        }
    }
}

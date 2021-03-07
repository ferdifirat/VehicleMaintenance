using System;
using System.Collections.Generic;
using System.Text;
using VehicleMaintenance.Entity.Concrete;

namespace VehicleMaintenance.Entity.DTOs
{
    public class PictureGroupDto
    {
        public int ID { get; set; }

        public TimeSpan CreateDate { get; set; }
        public virtual UserDto CreatedByUser { get; set; }
        public TimeSpan ModifyDate { get; set; }
        public int ModifiedBy { get; set; }
        public bool IsDeleted { get; set; }
        public string PictureImage { get; set; }

        public PictureGroupDto Map(Concrete.PictureGroup type)
        {
            this.ID = type.ID;
            this.CreateDate = type.CreateDate;
            this.CreatedByUser = new UserDto().Map(type.CreatedByUser);
            this.ModifyDate = type.ModifyDate;
            this.ModifiedBy = type.ModifiedBy;
            this.IsDeleted = type.IsDeleted;
            this.PictureImage = type.PictureImage;

            return this;
        }
    }
}

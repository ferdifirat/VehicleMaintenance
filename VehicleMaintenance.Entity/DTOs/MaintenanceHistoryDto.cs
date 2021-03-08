using System;
using System.Collections.Generic;
using System.Text;
using VehicleMaintenance.Entity.Concrete;

namespace VehicleMaintenance.Entity.DTOs
{
    public class MaintenanceHistoryDto
    {
        public int ID { get; set; }

        public TimeSpan CreateDate { get; set; }
        public virtual UserDto CreatedByUser { get; set; }
        public TimeSpan ModifyDate { get; set; }
        public int ModifiedBy { get; set; }
        public bool IsDeleted { get; set; }
        public int ActionTypeID { get; set; }
        public int MaintenanceID { get; set; }
        public MaintenanceDto Maintenance { get; set; }
        public ActionTypeDto ActionType { get; set; }
        public string Text { get; set; }

        public MaintenanceHistoryDto Map(Concrete.MaintenanceHistory type)
        {
            this.ID = type.ID;
            this.CreateDate = type.CreateDate;
            this.CreatedByUser = new UserDto().Map(type.CreatedByUser);
            this.ModifyDate = type.ModifyDate;
            this.ModifiedBy = type.ModifiedBy;
            this.IsDeleted = type.IsDeleted;
            this.Maintenance = new MaintenanceDto().Map(type.Maintenance);
            this.ActionType = new ActionTypeDto().Map(type.ActionType);
            this.Text = type.Text;

            return this;
        }
    }
}

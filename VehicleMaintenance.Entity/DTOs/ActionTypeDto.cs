using System;
using System.Collections.Generic;
using System.Text;
using VehicleMaintenance.Entity.Concrete;

namespace VehicleMaintenance.Entity.DTOs
{
    public class AddActionTypeDto
    {
        public int ID { get; set; }
        public string Name { get; set; }


        
    }
    
    
    public class ActionTypeDto
    {
        public int ID { get; set; }
        public TimeSpan CreateDate { get; set; }
        public virtual UserDto CreatedByUser { get; set; }
        public TimeSpan ModifyDate { get; set; }
        public int ModifiedBy { get; set; }
        public bool IsDeleted { get; set; }

        public string Name { get; set; }


        public ActionTypeDto Map (Concrete.ActionType type)
        {
            this.ID = type.ID;
            this.CreateDate = type.CreateDate;
            this.CreatedByUser = new UserDto().Map(type.CreatedByUser);
            this.IsDeleted = type.IsDeleted;

            return this;

        }



    }
}

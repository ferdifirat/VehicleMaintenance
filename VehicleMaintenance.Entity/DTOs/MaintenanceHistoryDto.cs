using System;
using System.Collections.Generic;
using System.Text;
using VehicleMaintenance.Entity.Concrete;

namespace VehicleMaintenance.Entity.DTOs
{
    public class MaintenanceHistoryDto
    {
        public int ID { get; set; }
        public int MaintenanceID { get; set; }
        public int ActionTypeID { get; set; }
        public string text { get; set; }
    }
}

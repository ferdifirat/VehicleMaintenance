using System;
using System.Collections.Generic;
using System.Text;
using VehicleMaintenance.Entity.Concrete;

namespace VehicleMaintenance.Entity.DTOs
{
    public class VehicleDto
    {
        public int ID { get; set; }
        public string PlateNo { get; set; }
        public int UserID { get; set; }
        public int VehicleTypeId { get; set; }
        public string Name { get; set; }
    }
}

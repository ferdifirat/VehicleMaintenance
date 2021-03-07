using System;
using System.Collections.Generic;
using System.Text;

namespace VehicleMaintenance.Entity.DTOs
{
    public class TokenDto
    {
        public string Token { get; set; }
        public DateTime ExpireDate { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace VehicleMaintenance.Core.Entities
{
    public class VehicleMaintenanceOptions
    {
        public JwtOptions Jwt { get; set; }
    }

    public class JwtOptions
    {
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public int ExpireHours { get; set; }
        public int ExpireMinutes { get; set; }
        public int ExpireSeconds { get; set; }
        public string Key { get; set; }
    }
}

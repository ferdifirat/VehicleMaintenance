﻿using System;
using System.Collections.Generic;
using System.Text;

namespace VehicleMaintenance.Entity.DTOs
{
    public class RegisterDto
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
    }
}

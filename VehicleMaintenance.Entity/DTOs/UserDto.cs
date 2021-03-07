﻿using System;
using System.Collections.Generic;
using System.Text;
using VehicleMaintenance.Entity.Concrete;

namespace VehicleMaintenance.Entity.DTOs
{
    public class UserDto
    {
        public int ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string profilepicture { get; set; }


        public UserDto Map(Concrete.User user)
        {
            this.ID = user.ID;
            this.FirstName = user.FirstName;


            return this;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using VehicleMaintenance.Entity.Concrete;

namespace VehicleMaintenance.Entity.DTOs
{
    public class UserDto
    {
        public int ID { get; set; }
        public TimeSpan CreateDate { get; set; }
        public virtual UserDto CreatedByUser { get; set; }
        public TimeSpan ModifyDate { get; set; }
        public int ModifiedBy { get; set; }
        public bool IsDeleted { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string ProfilePicture { get; set; }


        public UserDto Map(Concrete.User user)
        {
            this.ID = user.ID;
            this.CreateDate = user.CreateDate;
            this.ModifiedBy = user.ModifiedBy;
            this.ModifyDate = user.ModifyDate;
            this.FirstName = user.FirstName;
            this.LastName = user.LastName;
            this.Address = user.Address;
            this.Email = user.Email;
            this.PhoneNumber = user.PhoneNumber;
            this.ProfilePicture = user.ProfilePicture;

            return this;
        }
    }


    public class UserListDto
    {
        public int ID { get; set; }
        public TimeSpan CreateDate { get; set; }
        public virtual User CreatedByUser { get; set; }
        public TimeSpan ModifyDate { get; set; }
        public int ModifiedBy { get; set; }
        public bool IsDeleted { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string ProfilePicture { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using VehicleMaintenance.Core.Service;
using VehicleMaintenance.Entity.DTOs;

namespace VehicleMaintenance.Business.Abstract
{
    public interface IUserService
    {
        ResponseDto Login(LoginDto login);
        ResponseDto Register(RegisterDto register);
        ResponseDto GetUsers();
        ResponseDto GetUser(int id);
        ResponseDto AddUser(UserDto user);
        ResponseDto UpdateUser(UserDto user);
        ResponseDto DeleteUser(int userId);
    }
}

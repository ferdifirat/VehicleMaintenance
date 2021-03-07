using System;
using System.Collections.Generic;
using System.Text;
using VehicleMaintenance.Entity.Concrete;
using VehicleMaintenance.Entity.DTOs;

namespace VehicleMaintenance.Business.Abstract
{
    public interface ITokenService
    {
        TokenDto CreateToken(User user);
        bool ValidToken(string token);
    }
}

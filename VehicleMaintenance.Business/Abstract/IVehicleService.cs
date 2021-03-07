using System;
using System.Collections.Generic;
using System.Text;
using VehicleMaintenance.Core.Service;
using VehicleMaintenance.Entity.DTOs;

namespace VehicleMaintenance.Business.Abstract
{
    public interface IVehicleService
    {
        ResponseDto AddVehicle(VehicleDto VehicleDto);
        ResponseDto UpdateVehicle(VehicleDto VehicleDto);
        ResponseDto DeleteVehicle(int id);
        ResponseDto GetVehicleById(int id);
        ResponseDto GetAllVehicle();
    }
}

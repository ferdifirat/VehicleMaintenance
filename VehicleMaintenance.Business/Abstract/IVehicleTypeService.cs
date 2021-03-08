using System;
using System.Collections.Generic;
using System.Text;
using VehicleMaintenance.Core.Service;
using VehicleMaintenance.Entity.DTOs;

namespace VehicleMaintenance.Business.Abstract
{
    public interface IVehicleTypeService
    {
        ResponseDto AddVehicleType(AddVehicleTypeDto vehicleTypeDto);
        ResponseDto UpdateVehicleType(VehicleTypeDto vehicleTypeDto);
        ResponseDto DeleteVehicleType(int id);
        ResponseDto GetVehicleTypeById(int id);
        ResponseDto GetAllVehicleType();
    }
}

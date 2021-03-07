using System;
using System.Collections.Generic;
using System.Text;
using VehicleMaintenance.Core.Service;
using VehicleMaintenance.Entity.DTOs;

namespace VehicleMaintenance.Business.Abstract
{
    public interface IMaintenanceService
    {
        ResponseDto AddMaintenance(MaintenanceDto maintenanceDto);
        ResponseDto UpdateMaintenance(MaintenanceDto maintenanceDto);
        ResponseDto DeleteMaintenance(int id);
        ResponseDto GetMaintenanceById(int id);
        ResponseDto GetAllMaintenance();
    }
}

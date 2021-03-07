using System;
using System.Collections.Generic;
using System.Text;
using VehicleMaintenance.Core.Service;
using VehicleMaintenance.Entity.DTOs;

namespace VehicleMaintenance.Business.Abstract
{
    public interface IMaintenanceHistoryService
    {
        ResponseDto AddMaintenanceHistory(MaintenanceHistoryDto maintenanceHistoryDto);
        ResponseDto UpdateMaintenanceHistory(MaintenanceHistoryDto maintenanceHistoryDto);
        ResponseDto DeleteMaintenanceHistory(int id);
        ResponseDto GetMaintenanceHistoryById(int id);
        ResponseDto GetAllMaintenanceHistory();
    }
}

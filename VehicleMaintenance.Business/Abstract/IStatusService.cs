using System;
using System.Collections.Generic;
using System.Text;
using VehicleMaintenance.Core.Service;
using VehicleMaintenance.Entity.DTOs;

namespace VehicleMaintenance.Business.Abstract
{
    public interface IStatusService
    {
        ResponseDto AddStatus(StatusDto statusDto);
        ResponseDto UpdateStatus(StatusDto statusDto);
        ResponseDto DeleteStatus(int id);
        ResponseDto GetStatusById(int id);
        ResponseDto GetAllStatus();
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using VehicleMaintenance.Core.Service;
using VehicleMaintenance.Entity.DTOs;

namespace VehicleMaintenance.Business.Abstract
{
    public interface IActionTypeService
    {
        ResponseDto AddActionType(ActionTypeDto actionTypeDto);
        ResponseDto UpdateActionType(ActionTypeDto actionTypeDto);
        ResponseDto DeleteActionType(int id);
        ResponseDto GetActionTypeById(int id);
        ResponseDto GetAllActionType();
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using VehicleMaintenance.Core.Service;
using VehicleMaintenance.Entity.DTOs;

namespace VehicleMaintenance.Business.Abstract
{
    public interface IPictureGroupService
    {
        ResponseDto AddPictureGroup(PictureGroupDto pictureGroupDto);
        ResponseDto UpdatePictureGroup(PictureGroupDto pictureGroupDto);
        ResponseDto DeletePictureGroup(int id);
        ResponseDto GetPictureGroupById(int id);
        ResponseDto GetAllPictureGroup();
    }
}

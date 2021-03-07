using System;
using System.Collections.Generic;
using System.Text;
using VehicleMaintenance.Business.Abstract;
using VehicleMaintenance.Core.DataAccess;
using VehicleMaintenance.Core.Service;
using VehicleMaintenance.DataAccess.Abstract;
using VehicleMaintenance.Entity.Concrete;
using VehicleMaintenance.Entity.DTOs;

namespace VehicleMaintenance.Business.Concrete
{
    public class MaintenanceManager : IMaintenanceService
    {
        private readonly IMaintenanceDal _maintenanceDal;
        private readonly IUnitOfWork _unitOfWork;
        public MaintenanceManager(IMaintenanceDal maintenanceDal, IUnitOfWork unitOfWork)
        {
            _maintenanceDal = maintenanceDal;
            _unitOfWork = unitOfWork;
        }
        public ResponseDto AddMaintenance(MaintenanceDto maintenanceDto)
        {
            var response = new ResponseDto();

            var maintenance = new Maintenance()
            {
                CreateDate = DateTime.Now.TimeOfDay,
                //CreatedBy = Userstatus,
                IsDeleted = false,
                //ModifiedBy = User,
                //ModifyDate = TimeSpan,
                Description = maintenanceDto.Description,
                ExpectedTimeToFix = maintenanceDto.ExpectedTimeToFix,
                LocationLatitude = maintenanceDto.LocationLatitude,
                LocationLongitude = maintenanceDto.LocationLongitude,
                PictureGroup = _unitOfWork.GetRepository<PictureGroup>().Get(p => p.ID == maintenanceDto.PictureGroupID),
                ResponsibleUser = _unitOfWork.GetRepository<User>().Get(p => p.ID == maintenanceDto.ResponsibleUserID),
                Status = _unitOfWork.GetRepository<Status>().Get(p => p.ID == maintenanceDto.StatusID),
                Vehicle = _unitOfWork.GetRepository<Vehicle>().Get(p => p.ID == maintenanceDto.VehicleID),
                User = _unitOfWork.GetRepository<User>().Get(p => p.ID == maintenanceDto.UserId),
            };

            _maintenanceDal.Add(maintenance);
            var savingStatus = _maintenanceDal.SaveChanges();

            if (!savingStatus)
            {
                response.IsSuccess = false;
                response.Message = "Kayıt esnasında bir hata oluştu. Lütfen daha sonra tekrar deneyiniz.";
            }

            return response;
        }

        public ResponseDto DeleteMaintenance(int id)
        {
            var response = new ResponseDto();

            var maintenance = _maintenanceDal.Get(p => p.ID == id);

            if (maintenance == null)
            {
                response.IsSuccess = false;
                response.Message = "Bakım bulunamadı.";
                return response;
            }

            //maintenance.ModifiedBy = "";
            maintenance.ModifyDate = DateTime.Now.TimeOfDay;
            maintenance.IsDeleted = true;
            _maintenanceDal.Update(maintenance);
            var savingMaintenance = _maintenanceDal.SaveChanges();

            if (!savingMaintenance)
            {
                response.IsSuccess = false;
                response.Message = "Bakım silinirken bir hata oluştu lütfen daha sonra tekrar deneyiniz.";

            }

            return response;
        }

        public ResponseDto GetAllMaintenance()
        {
            throw new NotImplementedException();
        }

        public ResponseDto GetMaintenanceById(int id)
        {
            throw new NotImplementedException();
        }

        public ResponseDto UpdateMaintenance(MaintenanceDto maintenanceDto)
        {
            var response = new ResponseDto();

            var existingMaintenance = _maintenanceDal.Get(p => p.ID == maintenanceDto.ID);

            if (existingMaintenance == null)
            {
                response.IsSuccess = false;
                response.Message = "Bakım bulunamadı.";
                return response;
            }

            existingMaintenance.Description = maintenanceDto.Description;
            existingMaintenance.ExpectedTimeToFix = maintenanceDto.ExpectedTimeToFix;
            existingMaintenance.LocationLatitude = maintenanceDto.LocationLatitude;
            existingMaintenance.LocationLongitude = maintenanceDto.LocationLongitude;
            existingMaintenance.PictureGroup = _unitOfWork.GetRepository<PictureGroup>().Get(p => p.ID == maintenanceDto.PictureGroupID);
            existingMaintenance.ResponsibleUser = _unitOfWork.GetRepository<User>().Get(p => p.ID == maintenanceDto.ResponsibleUserID);
            existingMaintenance.Status = _unitOfWork.GetRepository<Status>().Get(p => p.ID == maintenanceDto.StatusID);
            existingMaintenance.Vehicle = _unitOfWork.GetRepository<Vehicle>().Get(p => p.ID == maintenanceDto.VehicleID);
            existingMaintenance.User = _unitOfWork.GetRepository<User>().Get(p => p.ID == maintenanceDto.UserId);
            //existingMaintenance.ModifiedBy = User
            existingMaintenance.ModifyDate = DateTime.Now.TimeOfDay;

            _maintenanceDal.Update(existingMaintenance);
            var savingStatus = _maintenanceDal.SaveChanges();

            if (!savingStatus)
            {
                response.IsSuccess = false;
                response.Message = "Durum güncellenirken bir hata oluştu lütfen daha sonra tekrar deneyiniz.";
            }

            return response;
        }
    }
}

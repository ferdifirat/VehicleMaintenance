using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VehicleMaintenance.Business.Abstract;
using VehicleMaintenance.Core.DataAccess;
using VehicleMaintenance.Core.Service;
using VehicleMaintenance.DataAccess.Abstract;
using VehicleMaintenance.DataAccess.ValidationRules.ManuelValidations;
using VehicleMaintenance.Entity.Concrete;
using VehicleMaintenance.Entity.DTOs;

namespace VehicleMaintenance.Business.Concrete
{
    public class MaintenanceManager : IMaintenanceService
    {
        private readonly IMaintenanceDal _maintenanceDal;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserSessionService _userSessionService;
        public MaintenanceManager(IMaintenanceDal maintenanceDal, IUnitOfWork unitOfWork, IUserSessionService userSessionService)
        {
            _userSessionService = userSessionService;
            _maintenanceDal = maintenanceDal;
            _unitOfWork = unitOfWork;
        }
        public ResponseDto AddMaintenance(MaintenanceDto maintenanceDto)
        {
            var response = new ResponseDto();

            var validationResponse = ManuelValidations.MaintenanceValidation(maintenanceDto);
            if (!validationResponse.IsSuccess)
            {
                return validationResponse;
            }

            var maintenance = new Maintenance()
            {
                CreateDate = DateTime.Now.TimeOfDay,
                CreatedByUser = _unitOfWork.GetRepository<User>().Get(p => p.ID == _userSessionService.GetUserId()),
                IsDeleted = false,
                Description = maintenanceDto.Description,
                ExpectedTimeToFix = maintenanceDto.ExpectedTimeToFix,
                LocationLatitude = maintenanceDto.LocationLatitude,
                LocationLongitude = maintenanceDto.LocationLongitude,
                PictureGroup = _unitOfWork.GetRepository<PictureGroup>().Get(p => p.ID == maintenanceDto.PictureGroupID),
                ResponsibleUser = _unitOfWork.GetRepository<User>().Get(p => p.ID == maintenanceDto.ResponsibleUserID),
                Status = _unitOfWork.GetRepository<Status>().Get(p => p.ID == maintenanceDto.StatusID),
                Vehicle = _unitOfWork.GetRepository<Vehicle>().Get(p => p.ID == maintenanceDto.VehicleID),
                User = _unitOfWork.GetRepository<User>().Get(p => p.ID == maintenanceDto.UserID),
            };

            _maintenanceDal.Add(maintenance);
            var savingStatus = _maintenanceDal.SaveChanges();

            if (!savingStatus)
            {
                response.IsSuccess = false;
                response.Message = "Kayıt esnasında bir hata oluştu. Lütfen daha sonra tekrar deneyiniz.";
            }

            response.Data = new MaintenanceDto().Map(maintenance);
            return response;
        }

        public ResponseDto DeleteMaintenance(int id)
        {
            var response = new ResponseDto();

            var maintenance = _maintenanceDal.Get(p => p.ID == id && p.IsDeleted == false);

            if (maintenance == null)
            {
                response.IsSuccess = false;
                response.Message = "Bakım bulunamadı.";
                return response;
            }

            maintenance.ModifiedBy = _unitOfWork.GetRepository<User>().Get(p => p.ID == _userSessionService.GetUserId()).ID;
            maintenance.ModifyDate = DateTime.Now.TimeOfDay;
            maintenance.IsDeleted = true;
            _maintenanceDal.Update(maintenance);
            var savingMaintenance = _maintenanceDal.SaveChanges();

            if (!savingMaintenance)
            {
                response.IsSuccess = false;
                response.Message = "Bakım silinirken bir hata oluştu lütfen daha sonra tekrar deneyiniz.";

            }

            response.Data = new MaintenanceDto().Map(maintenance);
            return response;
        }

        public ResponseDto GetAllMaintenance()
        {
            var response = new ResponseDto();

            var maintenances = _maintenanceDal.GetList(x=> x.IsDeleted == false);

            if (maintenances == null || !maintenances.Any())
            {
                response.IsSuccess = false;
                response.Message = "Aksiyon tipi bulunamadı.";
                return response;
            }
            var maintenanceDtos = new List<MaintenanceDto>();

            foreach (var maintenance in maintenances)
            {

                maintenanceDtos.Add(new MaintenanceDto().Map(maintenance));
            }

            response.Data = maintenanceDtos;
            return response;
        }

        public ResponseDto GetMaintenanceById(int id)
        {
            var response = new ResponseDto();
            var maintenance = _maintenanceDal.Get(x => x.ID == id && x.IsDeleted == false);
            if (maintenance == null)
            {
                response.IsSuccess = false;
                response.Message = "Bakım bulunamadı.";
                return response;
            }

            response.Data = new MaintenanceDto().Map(maintenance);
            return response;
        }

        public ResponseDto UpdateMaintenance(MaintenanceDto maintenanceDto)
        {
            var response = new ResponseDto();

            var validationResponse = ManuelValidations.MaintenanceValidation(maintenanceDto);
            if (!validationResponse.IsSuccess)
            {
                return validationResponse;
            }

            var existingMaintenance = _maintenanceDal.Get(p => p.ID == maintenanceDto.ID && p.IsDeleted == false);

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
            existingMaintenance.User = _unitOfWork.GetRepository<User>().Get(p => p.ID == maintenanceDto.UserID);
            existingMaintenance.ModifiedBy = _unitOfWork.GetRepository<User>().Get(p => p.ID == _userSessionService.GetUserId()).ID;
            existingMaintenance.ModifyDate = DateTime.Now.TimeOfDay;

            _maintenanceDal.Update(existingMaintenance);
            var savingStatus = _maintenanceDal.SaveChanges();

            if (!savingStatus)
            {
                response.IsSuccess = false;
                response.Message = "Bakım güncellenirken bir hata oluştu. Lütfen daha sonra tekrar deneyiniz.";
            }

            response.Data = new MaintenanceDto().Map(existingMaintenance);
            return response;
        }
    }
}

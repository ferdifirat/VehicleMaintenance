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
    public class MaintenanceHistoryManager : IMaintenanceHistoryService
    {
        private readonly IMaintenanceHistoryDal _maintenanceHistoryDal;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserSessionService _userSessionService;
        public MaintenanceHistoryManager(IMaintenanceHistoryDal maintenanceHistoryDal, IUnitOfWork unitOfWork, IUserSessionService userSessionService)
        {
            _unitOfWork = unitOfWork;
            _userSessionService = userSessionService;
            _maintenanceHistoryDal = maintenanceHistoryDal;
        }

        public ResponseDto AddMaintenanceHistory(MaintenanceHistoryDto maintenanceHistoryDto)
        {
            var response = new ResponseDto();

            var validationResponse = ManuelValidations.MaintenanceHistoryValidation(maintenanceHistoryDto);
            if (!validationResponse.IsSuccess)
            {
                return validationResponse;
            }

            var existingMaintenanceHistory = _maintenanceHistoryDal.Get(x => x.Text == maintenanceHistoryDto.Text && maintenanceHistoryDto.IsDeleted == false);

            if (existingMaintenanceHistory != null)
            {
                response.IsSuccess = false;
                response.Message = "Aynı isme ait bir bakım mevcut";
                return response;
            }

            var maintenanceHistory = new MaintenanceHistory()
            {
                CreateDate = DateTime.Now.TimeOfDay,
                CreatedByUser = _unitOfWork.GetRepository<User>().Get(p => p.ID == _userSessionService.GetUserId()),
                IsDeleted = false,
                Text = existingMaintenanceHistory.Text,
                Maintenance = _unitOfWork.GetRepository<Maintenance>().Get(p => p.ID == existingMaintenanceHistory.Maintenance.ID),
                ActionType = _unitOfWork.GetRepository<ActionType>().Get(p => p.ID == existingMaintenanceHistory.ActionType.ID),
            };

            _maintenanceHistoryDal.Add(existingMaintenanceHistory);
            var savingActionType = _maintenanceHistoryDal.SaveChanges();

            if (!savingActionType)
            {
                response.IsSuccess = false;
                response.Message = "Kayıt esnasında bir hata oluştu lütfen daha sonra tekrar deneyiniz.";
            }

            response.Data = new MaintenanceHistoryDto().Map(existingMaintenanceHistory);
            return response;
        }

        public ResponseDto DeleteMaintenanceHistory(int id)
        {
            var response = new ResponseDto();

            var maintenanceHistory = _maintenanceHistoryDal.Get(p => p.ID == id && p.IsDeleted == false);

            if (maintenanceHistory == null)
            {
                response.IsSuccess = false;
                response.Message = "Bakım geçmişi bulunamadı.";
                return response;
            }

            maintenanceHistory.ModifiedBy = _unitOfWork.GetRepository<User>().Get(p => p.ID == _userSessionService.GetUserId()).ID;
            maintenanceHistory.ModifyDate = DateTime.Now.TimeOfDay;
            maintenanceHistory.IsDeleted = true;
            _maintenanceHistoryDal.Update(maintenanceHistory);
            var savingMaintenance = _maintenanceHistoryDal.SaveChanges();

            if (!savingMaintenance)
            {
                response.IsSuccess = false;
                response.Message = "Bakım geçmişi silinirken bir hata oluştu lütfen daha sonra tekrar deneyiniz.";

            }

            response.Data = new MaintenanceHistoryDto().Map(maintenanceHistory);
            return response;
        }

        public ResponseDto GetAllMaintenanceHistory()
        {
            var response = new ResponseDto();

            var maintenanceHistories = _maintenanceHistoryDal.GetList(p=> p.IsDeleted == false);

            if (maintenanceHistories == null || !maintenanceHistories.Any())
            {
                response.IsSuccess = false;
                response.Message = "Bakım geçmişi bulunamadı.";
                return response;
            }
            var maintenanceHistoryDtos = new List<MaintenanceHistoryDto>();

            foreach (var maintenanceHistory in maintenanceHistories)
            {
                maintenanceHistoryDtos.Add(new MaintenanceHistoryDto().Map(maintenanceHistory));
            }

            response.Data = maintenanceHistoryDtos;
            return response;
        }

        public ResponseDto GetMaintenanceHistoryById(int id)
        {
            var response = new ResponseDto();
            var maintenanceHistory = _maintenanceHistoryDal.Get(x => x.ID == id);
            if (maintenanceHistory == null)
            {
                response.IsSuccess = false;
                response.Message = "Bakım geçmişi bulunamadı.";
                return response;
            }

            response.Data = new MaintenanceHistoryDto().Map(maintenanceHistory);
            return response;
        }

        public ResponseDto UpdateMaintenanceHistory(MaintenanceHistoryDto dto)
        {
            var response = new ResponseDto();

            var validationResponse = ManuelValidations.MaintenanceHistoryValidation(dto);
            if (!validationResponse.IsSuccess)
            {
                return validationResponse;
            }

            var existingMaintenanceHistory = _maintenanceHistoryDal.Get(p => p.ID == dto.ID && p.IsDeleted == false);

            if (existingMaintenanceHistory == null)
            {
                response.IsSuccess = false;
                response.Message = "Bakım geçmişi bulunamadı.";
                return response;
            }

            existingMaintenanceHistory.Text = dto.Text;
            existingMaintenanceHistory.Maintenance = _unitOfWork.GetRepository<Maintenance>().Get(p => p.ID == dto.Maintenance.ID);
            existingMaintenanceHistory.ModifiedBy = _unitOfWork.GetRepository<User>().Get(p => p.ID == _userSessionService.GetUserId()).ID;
            existingMaintenanceHistory.ModifyDate = DateTime.Now.TimeOfDay;
            existingMaintenanceHistory.ActionType = _unitOfWork.GetRepository<ActionType>().Get(p => p.ID == dto.ActionType.ID);

            _maintenanceHistoryDal.Update(existingMaintenanceHistory);
            var savingStatus = _maintenanceHistoryDal.SaveChanges();

            if (!savingStatus)
            {
                response.IsSuccess = false;
                response.Message = "Bakım geçmişi güncellenirken bir hata oluştu. Lütfen daha sonra tekrar deneyiniz.";
            }

            response.Data = new MaintenanceHistoryDto().Map(existingMaintenanceHistory);
            return response;
        }
    }
}

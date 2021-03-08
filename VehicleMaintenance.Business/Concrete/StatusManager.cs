using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VehicleMaintenance.Business.Abstract;
using VehicleMaintenance.Core.DataAccess;
using VehicleMaintenance.Core.Service;
using VehicleMaintenance.DataAccess.Abstract;
using VehicleMaintenance.Entity.Concrete;
using VehicleMaintenance.Entity.DTOs;

namespace VehicleMaintenance.Business.Concrete
{
    public class StatusManager : IStatusService
    {
        private readonly IStatusDal _statusDal;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserSessionService _userSessionService;
        public StatusManager(IStatusDal statusDal, IUnitOfWork unitOfWork, IUserSessionService userSessionService)
        {
            _unitOfWork = unitOfWork;
            _userSessionService = userSessionService;
            _statusDal = statusDal;
        }
        public ResponseDto AddStatus(StatusDto statusDto)
        {
            var response = new ResponseDto();
            var existingStatus = _statusDal.Get(x => x.Name == statusDto.Name);

            if (existingStatus != null)
            {
                response.IsSuccess = false;
                response.Message = "Aynı durum mevcut";
                return response;
            }

            var status = new Status()
            {
                CreateDate = DateTime.Now.TimeOfDay,
                CreatedByUser = _unitOfWork.GetRepository<User>().Get(p => p.ID == _userSessionService.GetUserId()),
                IsDeleted = false,
                Name = statusDto.Name,
            };

            _statusDal.Add(status);
            var savingStatus = _statusDal.SaveChanges();

            if (!savingStatus)
            {
                response.IsSuccess = false;
                response.Message = "Kayıt esnasında bir hata oluştu. Lütfen daha sonra tekrar deneyiniz.";
            }

            response.Data = new StatusDto().Map(status);
            return response;
        }

        public ResponseDto DeleteStatus(int id)
        {
            var response = new ResponseDto();
            var status = _statusDal.Get(p => p.ID == id);

            if (status == null)
            {
                response.IsSuccess = false;
                response.Message = "Durum bulunamadı.";
                return response;
            }

            status.ModifiedBy = _unitOfWork.GetRepository<User>().Get(p => p.ID == _userSessionService.GetUserId()).ID;
            status.ModifyDate = DateTime.Now.TimeOfDay;
            status.IsDeleted = true;
            _statusDal.Update(status);
            var savingStatus = _statusDal.SaveChanges();

            if (!savingStatus)
            {
                response.IsSuccess = false;
                response.Message = "Durum silinirken bir hata oluştu. Lütfen daha sonra tekrar deneyiniz.";
            }

            response.Data = new StatusDto().Map(status);
            return response;
        }

        public ResponseDto GetAllStatus()
        {
            var response = new ResponseDto();

            var allStatus = _statusDal.GetList();

            if (allStatus == null || !allStatus.Any())
            {
                response.IsSuccess = false;
                response.Message = "Durum bulunamadı.";
                return response;
            }
            var statusDtos = new List<StatusDto>();

            foreach (var status in allStatus)
            {
                statusDtos.Add(new StatusDto().Map(status));
            }

            response.Data = statusDtos;
            return response;
        }

        public ResponseDto GetStatusById(int id)
        {
            var response = new ResponseDto();
            var status = _statusDal.Get(x => x.ID == id);
            if (status == null)
            {
                response.IsSuccess = false;
                response.Message = "Durum bulunamadı.";
                return response;
            }

            response.Data = new StatusDto().Map(status);
            return response;
        }

        public ResponseDto UpdateStatus(StatusDto statusDto)
        {
            var response = new ResponseDto();

            var existingStatus = _statusDal.Get(p => p.ID == statusDto.ID);

            if (existingStatus == null)
            {
                response.IsSuccess = false;
                response.Message = "Durum bulunamadı.";
                return response;
            }

            existingStatus.Name = statusDto.Name;
            existingStatus.ModifiedBy = _unitOfWork.GetRepository<User>().Get(p => p.ID == _userSessionService.GetUserId()).ID;
            existingStatus.ModifyDate = DateTime.Now.TimeOfDay;

            _statusDal.Update(existingStatus);
            var savingStatus = _statusDal.SaveChanges();

            if (!savingStatus)
            {
                response.IsSuccess = false;
                response.Message = "Durum güncellenirken bir hata oluştu lütfen daha sonra tekrar deneyiniz.";
            }

            response.Data = new StatusDto().Map(existingStatus);
            return response;
        }
    }
}

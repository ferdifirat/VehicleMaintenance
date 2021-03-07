using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VehicleMaintenance.Business.Abstract;
using VehicleMaintenance.Core.Service;
using VehicleMaintenance.DataAccess.Abstract;
using VehicleMaintenance.Entity.Concrete;
using VehicleMaintenance.Entity.DTOs;

namespace VehicleMaintenance.Business.Concrete
{
    public class StatusManager : IStatusService
    {
        private readonly IStatusDal _statusDal;
        public StatusManager(IStatusDal statusDal)
        {
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
                //CreatedBy = Userstatus,
                IsDeleted = false,
                //ModifiedBy = User,
                //ModifyDate = TimeSpan,
                Name = statusDto.Name,
            };

            _statusDal.Add(status);
            var savingStatus = _statusDal.SaveChanges();

            if (!savingStatus)
            {
                response.IsSuccess = false;
                response.Message = "Kayıt esnasında bir hata oluştu. Lütfen daha sonra tekrar deneyiniz.";
            }

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

            //status.ModifiedBy = Kullanıcı
            status.ModifyDate = DateTime.Now.TimeOfDay;
            status.IsDeleted = true;
            _statusDal.Update(status);
            var savingStatus = _statusDal.SaveChanges();

            if (!savingStatus)
            {
                response.IsSuccess = false;
                response.Message = "Durum silinirken bir hata oluştu. Lütfen daha sonra tekrar deneyiniz.";
            }

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
                var statusDto = new StatusDto()
                {
                    ID = status.ID,
                    Name= status.Name,
                };

                statusDtos.Add(statusDto);
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
                response.Message = "History bulunamadı.";
                return response;
            }

            var statusDto = new StatusDto()
            {
                ID = status.ID,
                Name = status.Name
            };

            response.Data = statusDto;
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
            //existingStatus.ModifiedBy = User
            existingStatus.ModifyDate = DateTime.Now.TimeOfDay;

            _statusDal.Update(existingStatus);
            var savingStatus = _statusDal.SaveChanges();

            if (!savingStatus)
            {
                response.IsSuccess = false;
                response.Message = "Durum güncellenirken bir hata oluştu lütfen daha sonra tekrar deneyiniz.";
            }

            return response;
        }
    }
}

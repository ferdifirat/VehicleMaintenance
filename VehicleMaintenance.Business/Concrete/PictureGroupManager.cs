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
    public class PictureGroupManager : IPictureGroupService
    {
        private readonly IPictureGroupDal _pictureGroupDal;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserSessionService _userSessionService;
        public PictureGroupManager(IPictureGroupDal pictureGroupDal, IUnitOfWork unitOfWork, IUserSessionService userSessionService)
        {
            _pictureGroupDal = pictureGroupDal;
            _userSessionService = userSessionService;
            _unitOfWork = unitOfWork;
        }
        public ResponseDto AddPictureGroup(PictureGroupDto pictureGroupDto)
        {
            var response = new ResponseDto();

            var validationResponse = ManuelValidations.PictureGroupValidation(pictureGroupDto);
            if (!validationResponse.IsSuccess)
            {
                return validationResponse;
            }

            var existingPictureGroup = _pictureGroupDal.Get(x => x.PictureImage == pictureGroupDto.PictureImage);

            if (existingPictureGroup != null)
            {
                response.IsSuccess = false;
                response.Message = "Aynı resim grubu mevcut";
                return response;
            }

            var pictureGroup = new PictureGroup()
            {
                CreateDate = DateTime.Now.TimeOfDay,
                CreatedByUser = _unitOfWork.GetRepository<User>().Get(p => p.ID == _userSessionService.GetUserId()),
                IsDeleted = false,
                PictureImage = pictureGroupDto.PictureImage
            };

            _pictureGroupDal.Add(pictureGroup);
            var savingPictureGroup = _pictureGroupDal.SaveChanges();

            if (!savingPictureGroup)
            {
                response.IsSuccess = false;
                response.Message = "Kayıt esnasında bir hata oluştu. Lütfen daha sonra tekrar deneyiniz.";
            }

            response.Data = new PictureGroupDto().Map(pictureGroup);
            return response;
        }

        public ResponseDto DeletePictureGroup(int id)
        {
            var response = new ResponseDto();
            var pictureGroup = _pictureGroupDal.Get(p => p.ID == id);

            if (pictureGroup == null)
            {
                response.IsSuccess = false;
                response.Message = "Resim grubu bulunamadı.";
                return response;
            }

            pictureGroup.ModifiedBy = _unitOfWork.GetRepository<User>().Get(p => p.ID == _userSessionService.GetUserId()).ID;
            pictureGroup.ModifyDate = DateTime.Now.TimeOfDay;
            pictureGroup.IsDeleted = true;
            _pictureGroupDal.Update(pictureGroup);
            var savingPictureGroup = _pictureGroupDal.SaveChanges();

            if (!savingPictureGroup)
            {
                response.IsSuccess = false;
                response.Message = "Resim grubu silinirken bir hata oluştu. Lütfen daha sonra tekrar deneyiniz.";
            }

            response.Data = new PictureGroupDto().Map(pictureGroup);
            return response;
        }

        public ResponseDto GetAllPictureGroup()
        {
            var response = new ResponseDto();

            var allPictureGroup = _pictureGroupDal.GetList();

            if (allPictureGroup == null || !allPictureGroup.Any())
            {
                response.IsSuccess = false;
                response.Message = "Resim grubu bulunamadı.";
                return response;
            }
            var pictureGroupDtos = new List<PictureGroupDto>();

            foreach (var pictureGroup in allPictureGroup)
            {
                pictureGroupDtos.Add(new PictureGroupDto().Map(pictureGroup));
            }

            response.Data = pictureGroupDtos;
            return response;
        }

        public ResponseDto GetPictureGroupById(int id)
        {
            var response = new ResponseDto();
            var pictureGroup = _pictureGroupDal.Get(x => x.ID == id);
            if (pictureGroup == null)
            {
                response.IsSuccess = false;
                response.Message = "Resim grubu bulunamadı.";
                return response;
            }

            response.Data = new PictureGroupDto().Map(pictureGroup);
            return response;
        }

        public ResponseDto UpdatePictureGroup(PictureGroupDto pictureGroupDto)
        {
            var response = new ResponseDto();

            var validationResponse = ManuelValidations.PictureGroupValidation(pictureGroupDto);
            if (!validationResponse.IsSuccess)
            {
                return validationResponse;
            }

            var existingPictureGroup = _pictureGroupDal.Get(p => p.ID == pictureGroupDto.ID);

            if (existingPictureGroup == null)
            {
                response.IsSuccess = false;
                response.Message = "Resim grubu bulunamadı.";
                return response;
            }

            existingPictureGroup.PictureImage = pictureGroupDto.PictureImage;
            existingPictureGroup.ModifiedBy = _unitOfWork.GetRepository<User>().Get(p => p.ID == _userSessionService.GetUserId()).ID;
            existingPictureGroup.ModifyDate = DateTime.Now.TimeOfDay;

            _pictureGroupDal.Update(existingPictureGroup);
            var savingStatus = _pictureGroupDal.SaveChanges();

            if (!savingStatus)
            {
                response.IsSuccess = false;
                response.Message = "Resim grubu güncellenirken bir hata oluştu lütfen daha sonra tekrar deneyiniz.";
            }

            response.Data = new PictureGroupDto().Map(existingPictureGroup);
            return response;
        }
    }
}

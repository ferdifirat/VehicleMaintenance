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
    public class PictureGroupManager : IPictureGroupService
    {
        private readonly IPictureGroupDal _pictureGroupDal;
        public PictureGroupManager(IPictureGroupDal pictureGroupDal)
        {
            _pictureGroupDal = pictureGroupDal;
        }
        public ResponseDto AddPictureGroup(PictureGroupDto pictureGroupDto)
        {
            var response = new ResponseDto();
            var existingPictureGroup = _pictureGroupDal.Get(x => x.PictureImage == pictureGroupDto.PictureImage);

            if (existingPictureGroup != null)
            {
                response.IsSuccess = false;
                response.Message = "Aynı resim grubu mevcut";
                return response;
            }

            var status = new PictureGroup()
            {
                CreateDate = DateTime.Now.TimeOfDay,
                //CreatedBy = Userstatus,
                IsDeleted = false,
                //ModifiedBy = User,
                //ModifyDate = TimeSpan,
                PictureImage = pictureGroupDto.PictureImage
            };

            _pictureGroupDal.Add(status);
            var savingPictureGroup = _pictureGroupDal.SaveChanges();

            if (!savingPictureGroup)
            {
                response.IsSuccess = false;
                response.Message = "Kayıt esnasında bir hata oluştu. Lütfen daha sonra tekrar deneyiniz.";
            }

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

            //pictureGroup.ModifiedBy = "";
            pictureGroup.ModifyDate = DateTime.Now.TimeOfDay;
            pictureGroup.IsDeleted = true;
            _pictureGroupDal.Update(pictureGroup);
            var savingPictureGroup = _pictureGroupDal.SaveChanges();

            if (!savingPictureGroup)
            {
                response.IsSuccess = false;
                response.Message = "Resim grubu silinirken bir hata oluştu. Lütfen daha sonra tekrar deneyiniz.";
            }

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
                var statusDto = new PictureGroupDto()
                {
                    ID = pictureGroup.ID,
                    PictureImage = pictureGroup.PictureImage,
                };

                pictureGroupDtos.Add(statusDto);
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

            var pictureGroupDto = new PictureGroupDto()
            {
                ID = pictureGroup.ID,
                PictureImage = pictureGroup.PictureImage
            };

            response.Data = pictureGroupDto;
            return response;
        }

        public ResponseDto UpdatePictureGroup(PictureGroupDto pictureGroupDto)
        {
            var response = new ResponseDto();

            var existingPictureGroup = _pictureGroupDal.Get(p => p.ID == pictureGroupDto.ID);

            if (existingPictureGroup == null)
            {
                response.IsSuccess = false;
                response.Message = "Resim grubu bulunamadı.";
                return response;
            }

            existingPictureGroup.PictureImage = pictureGroupDto.PictureImage;
            //existingStatus.ModifiedBy = User
            existingPictureGroup.ModifyDate = DateTime.Now.TimeOfDay;

            _pictureGroupDal.Update(existingPictureGroup);
            var savingStatus = _pictureGroupDal.SaveChanges();

            if (!savingStatus)
            {
                response.IsSuccess = false;
                response.Message = "Resim grubu güncellenirken bir hata oluştu lütfen daha sonra tekrar deneyiniz.";
            }

            return response;
        }
    }
}

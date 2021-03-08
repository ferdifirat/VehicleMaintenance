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
    public class VehicleTypeManager : IVehicleTypeService
    {
        private readonly IVehicleTypeDal _vehicleTypeDal;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserSessionService _userSessionService;
        public VehicleTypeManager(IVehicleTypeDal vehicleType, IUnitOfWork unitOfWork, IUserSessionService userSessionService)
        {
            _unitOfWork = unitOfWork;
            _userSessionService = userSessionService;
            _vehicleTypeDal = vehicleType;
        }
        public ResponseDto AddVehicleType(AddVehicleTypeDto vehicleTypeDto)
        {
            var response = new ResponseDto();
            var existingVehicleType = _vehicleTypeDal.Get(x => x.Name == vehicleTypeDto.Name);

            if (existingVehicleType != null)
            {
                response.IsSuccess = false;
                response.Message = "Aynı Araç Tipi mevcut";
                return response;
            }

            var vehicleType = new VehicleType()
            {
                CreateDate = DateTime.Now.TimeOfDay,
                CreatedByUser = _unitOfWork.GetRepository<User>().Get(p => p.ID == _userSessionService.GetUserId()),
                IsDeleted = false,
                Name = vehicleTypeDto.Name,
            };

            _vehicleTypeDal.Add(vehicleType);
            var savingVehicleType = _vehicleTypeDal.SaveChanges();

            if (!savingVehicleType)
            {
                response.IsSuccess = false;
                response.Message = "Kayıt esnasında bir hata oluştu. Lütfen daha sonra tekrar deneyiniz.";
            }

            response.Data = new VehicleTypeDto().Map(vehicleType);
            return response;
        }

        public ResponseDto DeleteVehicleType(int id)
        {
            var response = new ResponseDto();
            var vehicleType = _vehicleTypeDal.Get(p => p.ID == id);

            if (vehicleType == null)
            {
                response.IsSuccess = false;
                response.Message = "Araç Tipi bulunamadı.";
                return response;
            }

            vehicleType.ModifiedBy = _unitOfWork.GetRepository<User>().Get(p => p.ID == _userSessionService.GetUserId()).ID;
            vehicleType.ModifyDate = DateTime.Now.TimeOfDay;
            vehicleType.IsDeleted = true;
            _vehicleTypeDal.Update(vehicleType);
            var savingVehicleType = _vehicleTypeDal.SaveChanges();

            if (!savingVehicleType)
            {
                response.IsSuccess = false;
                response.Message = "Araç Tipi silinirken bir hata oluştu. Lütfen daha sonra tekrar deneyiniz.";
            }

            response.Data = new VehicleTypeDto().Map(vehicleType);
            return response;
        }

        public ResponseDto GetAllVehicleType()
        {
            var response = new ResponseDto();

            var allVehicleType = _vehicleTypeDal.GetList();

            if (allVehicleType == null || !allVehicleType.Any())
            {
                response.IsSuccess = false;
                response.Message = "Araç Tipi bulunamadı.";
                return response;
            }
            var vehicleTypeDtos = new List<VehicleTypeDto>();

            foreach (var vehicleType in allVehicleType)
            {
                vehicleTypeDtos.Add(new VehicleTypeDto().Map(vehicleType));
            }

            response.Data = vehicleTypeDtos;
            return response;
        }

        public ResponseDto GetVehicleTypeById(int id)
        {
            var response = new ResponseDto();
            var vehicleType = _vehicleTypeDal.Get(x => x.ID == id);
            if (vehicleType == null)
            {
                response.IsSuccess = false;
                response.Message = "Araç Tipi bulunamadı.";
                return response;
            }

            response.Data = new VehicleTypeDto().Map(vehicleType);
            return response;
        }

        public ResponseDto UpdateVehicleType(VehicleTypeDto vehicleTypeDto)
        {
            var response = new ResponseDto();

            var existingVehicleType = _vehicleTypeDal.Get(p => p.ID == vehicleTypeDto.ID);

            if (existingVehicleType == null)
            {
                response.IsSuccess = false;
                response.Message = "Durum bulunamadı.";
                return response;
            }

            existingVehicleType.Name = vehicleTypeDto.Name;
            existingVehicleType.ModifiedBy = _unitOfWork.GetRepository<User>().Get(p => p.ID == _userSessionService.GetUserId()).ID;
            existingVehicleType.ModifyDate = DateTime.Now.TimeOfDay;

            _vehicleTypeDal.Update(existingVehicleType);
            var savingVehicleType = _vehicleTypeDal.SaveChanges();

            if (!savingVehicleType)
            {
                response.IsSuccess = false;
                response.Message = "Araç Tipi güncellenirken bir hata oluştu lütfen daha sonra tekrar deneyiniz.";
            }

            response.Data = new VehicleTypeDto().Map(existingVehicleType);
            return response;
        }
    }
}

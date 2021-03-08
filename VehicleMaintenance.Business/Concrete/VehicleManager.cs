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
    public class VehicleManager : IVehicleService
    {
        private readonly IVehicleDal _vehicleDal;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserSessionService _userSessionService;
        public VehicleManager(IVehicleDal vehicleDal, IUnitOfWork unitOfWork, IUserSessionService userSessionService)
        {
            _vehicleDal = vehicleDal;
            _userSessionService = userSessionService;
            _unitOfWork = unitOfWork;
        }
        public ResponseDto AddVehicle(VehicleDto vehicleDto)
        {
            var response = new ResponseDto();

            var validationResponse = ManuelValidations.VehicleValidation(vehicleDto);
            if (!validationResponse.IsSuccess)
            {
                return validationResponse;
            }

            var existingVehicle = _vehicleDal.Get(x => x.Name == vehicleDto.Name);

            if (existingVehicle != null)
            {
                response.IsSuccess = false;
                response.Message = "Aynı araç sistemde mevcut.";
                return response;
            }

            var vehicle = new Vehicle()
            {
                CreateDate = DateTime.Now.TimeOfDay,
                CreatedByUser = _unitOfWork.GetRepository<User>().Get(p => p.ID == _userSessionService.GetUserId()),
                IsDeleted = false,
                VehicleType = _unitOfWork.GetRepository<VehicleType>().Get(p => p.ID == vehicleDto.VehicleTypeID),
                User = _unitOfWork.GetRepository<User>().Get(p => p.ID == vehicleDto.UserID),
                PlateNo = vehicleDto.PlateNo,
                Name = vehicleDto.Name,
            };

            _vehicleDal.Add(vehicle);
            var savingVehicle = _vehicleDal.SaveChanges();

            if (!savingVehicle)
            {
                response.IsSuccess = false;
                response.Message = "Kayıt esnasında bir hata oluştu. Lütfen daha sonra tekrar deneyiniz.";
            }
            response.Data = new VehicleDto().Map(vehicle);
            return response;
        }

        public ResponseDto DeleteVehicle(int id)
        {
            var response = new ResponseDto();
            var vehicle = _vehicleDal.Get(p => p.ID == id);

            if (vehicle == null)
            {
                response.IsSuccess = false;
                response.Message = "Araç Tipi bulunamadı.";
                return response;
            }

            vehicle.ModifiedBy = _unitOfWork.GetRepository<User>().Get(p => p.ID == _userSessionService.GetUserId()).ID;
            vehicle.ModifyDate = DateTime.Now.TimeOfDay;
            vehicle.IsDeleted = true;
            _vehicleDal.Update(vehicle);
            var savingVehicle = _vehicleDal.SaveChanges();

            if (!savingVehicle)
            {
                response.IsSuccess = false;
                response.Message = "Araç silinirken bir hata oluştu. Lütfen daha sonra tekrar deneyiniz.";
            }

            return response;
        }

        public ResponseDto GetAllVehicle()
        {
            var response = new ResponseDto();

            var vehicles = _vehicleDal.GetList();

            if (vehicles == null || !vehicles.Any())
            {
                response.IsSuccess = false;
                response.Message = "Araç bulunamadı.";
                return response;
            }
            var vehicleDtos = new List<VehicleDto>();

            foreach (var vehicle in vehicles)
            {
                vehicleDtos.Add(new VehicleDto().Map(vehicle));
            }

            response.Data = vehicleDtos;
            return response;
        }

        public ResponseDto GetVehicleById(int id)
        {
            var response = new ResponseDto();
            var vehicle = _vehicleDal.Get(x => x.ID == id && x.IsDeleted == false);
            if (vehicle == null)
            {
                response.IsSuccess = false;
                response.Message = "Aksiyon Tipi bulunamadı.";
                return response;
            }
            response.Data = new VehicleDto().Map(vehicle);
            return response;
        }

        public ResponseDto UpdateVehicle(VehicleDto vehicleDto)
        {
            var response = new ResponseDto();

            var validationResponse = ManuelValidations.VehicleValidation(vehicleDto);
            if (!validationResponse.IsSuccess)
            {
                return validationResponse;
            }

            var existingVehicle = _vehicleDal.Get(p => p.ID == vehicleDto.ID);

            if (existingVehicle == null)
            {
                response.IsSuccess = false;
                response.Message = "Araç bulunamadı.";
                return response;
            }

            existingVehicle.Name = vehicleDto.Name;
            existingVehicle.ModifiedBy = _unitOfWork.GetRepository<User>().Get(p => p.ID == _userSessionService.GetUserId()).ID;
            existingVehicle.ModifyDate = DateTime.Now.TimeOfDay;
            existingVehicle.VehicleType = _unitOfWork.GetRepository<VehicleType>().Get(p => p.ID == vehicleDto.VehicleTypeID);
            existingVehicle.User = _unitOfWork.GetRepository<User>().Get(p => p.ID == vehicleDto.UserID);
            existingVehicle.PlateNo = vehicleDto.PlateNo;

            _vehicleDal.Update(existingVehicle);
            var savingVehicleType = _vehicleDal.SaveChanges();

            if (!savingVehicleType)
            {
                response.IsSuccess = false;
                response.Message = "Araç güncellenirken bir hata oluştu lütfen daha sonra tekrar deneyiniz.";
            }

            response.Data = new VehicleDto().Map(existingVehicle);
            return response;
        }
    }
}

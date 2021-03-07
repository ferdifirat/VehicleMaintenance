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
    public class VehicleManager : IVehicleService
    {
        private readonly IVehicleDal _vehicleDal;
        private readonly IUnitOfWork _unitOfWork;
        public VehicleManager(IVehicleDal vehicleDal, IUnitOfWork unitOfWork)
        {
            _vehicleDal = vehicleDal;
            _unitOfWork = unitOfWork;
        }
        public ResponseDto AddVehicle(VehicleDto vehicleDto)
        {
            var response = new ResponseDto();
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
                //CreatedBy = Userstatus,
                IsDeleted = false,
                //ModifiedBy = User,
                //ModifyDate = TimeSpan,
                VehicleType = _unitOfWork.GetRepository<VehicleType>().Get(p => p.ID == vehicleDto.VehicleTypeId),
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

            //vehicle.ModifiedBy = Kullanıcı
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
            throw new NotImplementedException();
        }

        public ResponseDto GetVehicleById(int id)
        {
            throw new NotImplementedException();
        }

        public ResponseDto UpdateVehicle(VehicleDto vehicleDto)
        {
            var response = new ResponseDto();

            var existingVehicle = _vehicleDal.Get(p => p.ID == vehicleDto.ID);

            if (existingVehicle == null)
            {
                response.IsSuccess = false;
                response.Message = "Araç bulunamadı.";
                return response;
            }

            existingVehicle.Name = vehicleDto.Name;
            //existingVehicleType.ModifiedBy = User
            existingVehicle.ModifyDate = DateTime.Now.TimeOfDay;
            existingVehicle.VehicleType = _unitOfWork.GetRepository<VehicleType>().Get(p => p.ID == vehicleDto.VehicleTypeId);
            existingVehicle.User = _unitOfWork.GetRepository<User>().Get(p => p.ID == vehicleDto.UserID);
            existingVehicle.PlateNo = vehicleDto.PlateNo;

            _vehicleDal.Update(existingVehicle);
            var savingVehicleType = _vehicleDal.SaveChanges();

            if (!savingVehicleType)
            {
                response.IsSuccess = false;
                response.Message = "Araç güncellenirken bir hata oluştu lütfen daha sonra tekrar deneyiniz.";
            }

            return response;
        }
    }
}

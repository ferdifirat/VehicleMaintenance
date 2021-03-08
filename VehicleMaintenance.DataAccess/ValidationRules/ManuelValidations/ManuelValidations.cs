using System;
using System.Collections.Generic;
using System.Text;
using VehicleMaintenance.Core.Common.Constant;
using VehicleMaintenance.Core.Service;
using VehicleMaintenance.Core.Utilities;
using VehicleMaintenance.Entity.DTOs;

namespace VehicleMaintenance.DataAccess.ValidationRules.ManuelValidations
{
    public class ManuelValidations
    {
        public static ResponseDto ActionTypeValidation(ActionTypeDto request)
        {
            var response = new ResponseDto();

            if (String.IsNullOrEmpty(request.Name)) return Errors.ActionTypeNameRequired.JsonSerialize().JsonDeserialize<ResponseDto>();

            return response;
        }

        public static ResponseDto VehicleTypeValidation(VehicleTypeDto request)
        {
            var response = new ResponseDto();

            if (String.IsNullOrEmpty(request.Name)) return Errors.VehicleTypeNameRequired.JsonSerialize().JsonDeserialize<ResponseDto>();

            return response;
        }

        public static ResponseDto StatusValidation(StatusDto request)
        {
            var response = new ResponseDto();

            if (String.IsNullOrEmpty(request.Name)) return Errors.StatusNameRequired.JsonSerialize().JsonDeserialize<ResponseDto>();

            return response;
        }

        public static ResponseDto PictureGroupValidation(PictureGroupDto request)
        {
            var response = new ResponseDto();

            if (String.IsNullOrEmpty(request.PictureImage)) return Errors.PictureImageRequired.JsonSerialize().JsonDeserialize<ResponseDto>();

            return response;
        }

        public static ResponseDto VehicleValidation(VehicleDto request)
        {
            var response = new ResponseDto();

            if (String.IsNullOrEmpty(request.Name)) return Errors.VehicleNameRequired.JsonSerialize().JsonDeserialize<ResponseDto>();
            if (String.IsNullOrEmpty(request.PlateNo)) return Errors.PlateNoRequired.JsonSerialize().JsonDeserialize<ResponseDto>();
            if (request.UserID == 0) return Errors.UserIDRequired.JsonSerialize().JsonDeserialize<ResponseDto>();
            if (request.VehicleTypeID == 0) return Errors.VehicleTypeID.JsonSerialize().JsonDeserialize<ResponseDto>();
            if (request.UserID == 0) return Errors.UserIDRequired.JsonSerialize().JsonDeserialize<ResponseDto>();
            return response;
        }

        public static ResponseDto MaintenanceValidation(MaintenanceDto request)
        {
            var response = new ResponseDto();

            if (String.IsNullOrEmpty(request.Description)) return Errors.MaintenanceDescriptionRequired.JsonSerialize().JsonDeserialize<ResponseDto>();
            if (String.IsNullOrEmpty(request.LocationLatitude)) return Errors.LocationLatitudeRequired.JsonSerialize().JsonDeserialize<ResponseDto>();
            if (String.IsNullOrEmpty(request.LocationLongitude)) return Errors.LocationLongitudeRequired.JsonSerialize().JsonDeserialize<ResponseDto>();
            if (request.VehicleID == 0) return Errors.VehicleIDRequired.JsonSerialize().JsonDeserialize<ResponseDto>();
            if (request.UserID == 0) return Errors.UserIDRequired.JsonSerialize().JsonDeserialize<ResponseDto>();
            if (request.ResponsibleUserID == 0) return Errors.ResponsibleUserID.JsonSerialize().JsonDeserialize<ResponseDto>();
            if (request.StatusID == 0) return Errors.StatusID.JsonSerialize().JsonDeserialize<ResponseDto>();
            if (request.PictureGroupID == 0) return Errors.PictureGroupID.JsonSerialize().JsonDeserialize<ResponseDto>();
            if (request.ExpectedTimeToFix == new DateTime(1, 1, 1)) return Errors.ExpectedTimeToFix.JsonSerialize().JsonDeserialize<ResponseDto>();

            return response;
        }

        public static ResponseDto MaintenanceHistoryValidation(MaintenanceHistoryDto request)
        {
            var response = new ResponseDto();

            if (String.IsNullOrEmpty(request.Text)) return Errors.TextRequired.JsonSerialize().JsonDeserialize<ResponseDto>();
            if (request.ActionTypeID == 0) return Errors.ActionTypeIDRequired.JsonSerialize().JsonDeserialize<ResponseDto>();
            if (request.MaintenanceID == 0) return Errors.MaintenanceIDRequired.JsonSerialize().JsonDeserialize<ResponseDto>();

            return response;
        }
    }


}

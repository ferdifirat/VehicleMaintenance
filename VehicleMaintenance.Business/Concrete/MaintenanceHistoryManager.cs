using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VehicleMaintenance.Business.Abstract;
using VehicleMaintenance.Core.Service;
using VehicleMaintenance.DataAccess.Abstract;
using VehicleMaintenance.Entity.DTOs;

namespace VehicleMaintenance.Business.Concrete
{
    public class MaintenanceHistoryManager : IMaintenanceHistoryService
    {
        private readonly IMaintenanceHistoryDal _maintenanceHistoryDal;
        public MaintenanceHistoryManager(IMaintenanceHistoryDal maintenanceHistoryDal)
        {
            _maintenanceHistoryDal = maintenanceHistoryDal;
        }

        public ResponseDto AddMaintenanceHistory(MaintenanceHistoryDto maintenanceHistoryDto)
        {
            throw new NotImplementedException();
        }

        public ResponseDto DeleteMaintenanceHistory(int id)
        {
            throw new NotImplementedException();
        }

        public ResponseDto GetAllMaintenanceHistory()
        {
            var response = new ResponseDto();

            var maintenanceHistories = _maintenanceHistoryDal.GetList();

            if (maintenanceHistories == null || !maintenanceHistories.Any())
            {
                response.IsSuccess = false;
                response.Message = "History bulunamadı.";
                return response;
            }
            var maintenanceHistoryDtos = new List<MaintenanceHistoryDto>();

            foreach (var maintenanceHistory in maintenanceHistories)
            {
                var maintenanceDto = new MaintenanceHistoryDto()
                {
                    ID = maintenanceHistory.ID,
                    ActionTypeID = maintenanceHistory.ActionType.ID,
                    MaintenanceID = maintenanceHistory.Maintenance.ID,
                };

                maintenanceHistoryDtos.Add(maintenanceDto);
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
                response.Message = "History bulunamadı.";
                return response;
            }

            var maintenanceDto = new MaintenanceHistoryDto()
            {
                ID = maintenanceHistory.ID,
                ActionTypeID = maintenanceHistory.ActionType.ID,
                MaintenanceID = maintenanceHistory.Maintenance.ID,
            };

            response.Data = maintenanceDto;
            return response;
        }

        public ResponseDto UpdateMaintenanceHistory(MaintenanceHistoryDto maintenanceHistoryDto)
        {
            var response = new ResponseDto();

            return response;
        }
    }
}

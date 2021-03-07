using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VehicleMaintenance.Business.Abstract;
using VehicleMaintenance.Business.CustomExtensions;
using VehicleMaintenance.Entity.DTOs;

namespace VehicleMaintenance.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ServiceFilter(typeof(TokenAttribute))]
    public class MaintenanceHistoryController : ControllerBase
    {
         private readonly IMaintenanceHistoryService _maintenanceHistoryService;
        public MaintenanceHistoryController(IMaintenanceHistoryService maintenanceHistoryService)
        {
            _maintenanceHistoryService = maintenanceHistoryService;
        }

        [HttpGet]
        [Route("GetAllMaintenanceHistory")]
        public ActionResult GetAllMaintenanceHistory()
        {
            var response = _maintenanceHistoryService.GetAllMaintenanceHistory();

            return Ok(new { response });
        }

        [HttpGet]
        [Route("GetMaintenanceHistory/{id}")]
        public ActionResult GetMaintenanceHistory(int id)
        {
            var response = _maintenanceHistoryService.GetMaintenanceHistoryById(id);

            return Ok(new { response });
        }

        [HttpPost]
        [Route("AddMaintenanceHistory")]
        public ActionResult AddMaintenanceHistory(MaintenanceHistoryDto maintenanceHistory)
        {
            var response = _maintenanceHistoryService.AddMaintenanceHistory(maintenanceHistory);

            return Ok(new { response });
        }

        [HttpPost]
        [Route("UpdateMaintenanceHistory")]
        public ActionResult UpdateMaintenanceHistory(MaintenanceHistoryDto maintenanceHistory)
        {
            var response = _maintenanceHistoryService.UpdateMaintenanceHistory(maintenanceHistory);

            return Ok(new { response });
        }

        [HttpPost]
        [Route("DeleteMaintenanceHistory")]
        public ActionResult DeleteMaintenanceHistory(int id)
        {
            var response = _maintenanceHistoryService.DeleteMaintenanceHistory(id);

            return Ok(new { response });
        }
    }
}

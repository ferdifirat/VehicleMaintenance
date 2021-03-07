using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VehicleMaintenance.Business.Abstract;
using VehicleMaintenance.Entity.DTOs;

namespace VehicleMaintenance.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MaintenanceController : ControllerBase
    {
        private readonly IMaintenanceService _maintenanceService;
        public MaintenanceController(IMaintenanceService maintenanceService)
        {
            _maintenanceService = maintenanceService;
        }

        [HttpGet]
        [Route("GetAllMaintenance")]
        public ActionResult GetAllMaintenance()
        {
            var response = _maintenanceService.GetAllMaintenance();

            return Ok(new { response });
        }

        [HttpGet]
        [Route("GetMaintenance/{id}")]
        public ActionResult GetMaintenance(int id)
        {
            var response = _maintenanceService.GetMaintenanceById(id);

            return Ok(new { response });
        }

        [HttpPost]
        [Route("AddMaintenance")]
        public ActionResult AddMaintenance(MaintenanceDto maintenance)
        {
            var response = _maintenanceService.AddMaintenance(maintenance);

            return Ok(new { response });
        }

        [HttpPost]
        [Route("UpdateMaintenance")]
        public ActionResult UpdateMaintenance(MaintenanceDto maintenance)
        {
            var response = _maintenanceService.UpdateMaintenance(maintenance);

            return Ok(new { response });
        }

        [HttpPost]
        [Route("DeleteMaintenance")]
        public ActionResult DeleteMaintenance(int id)
        {
            var response = _maintenanceService.DeleteMaintenance(id);

            return Ok(new { response });
        }
    }
}

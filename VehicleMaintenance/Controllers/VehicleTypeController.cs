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
    public class VehicleTypeController : ControllerBase
    {
        private readonly IVehicleTypeService _vehicleTypeService;
        public VehicleTypeController(IVehicleTypeService vehicleTypeService)
        {
            _vehicleTypeService = vehicleTypeService;
        }

        [HttpGet]
        [Route("GetAllVehicleType")]
        public ActionResult GetAllVehicleType()
        {
            var response = _vehicleTypeService.GetAllVehicleType();

            return Ok(new { response });
        }

        [HttpGet]
        [Route("GetVehicleType/{id}")]
        public ActionResult GetVehicleType(int id)
        {
            var response = _vehicleTypeService.GetVehicleTypeById(id);

            return Ok(new { response });
        }

        [HttpPost]
        [Route("AddVehicleType")]
        public ActionResult AddVehicleType(AddVehicleTypeDto vehicleType)
        {
            var response = _vehicleTypeService.AddVehicleType(vehicleType);

            return Ok(new { response });
        }

        [HttpPost]
        [Route("UpdateVehicleType")]
        public ActionResult UpdateVehicleType(VehicleTypeDto vehicleType)
        {
            var response = _vehicleTypeService.UpdateVehicleType(vehicleType);

            return Ok(new { response });
        }

        [HttpPost]
        [Route("DeleteVehicleType")]
        public ActionResult DeleteVehicleType(int id)
        {
            var response = _vehicleTypeService.DeleteVehicleType(id);

            return Ok(new { response });
        }

    }
}

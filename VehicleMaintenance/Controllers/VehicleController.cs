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
    public class VehicleController : ControllerBase
    {
        private readonly IVehicleService _vehicleService;
        public VehicleController(IVehicleService vehicleService)
        {
            _vehicleService = vehicleService;
        }

        [HttpGet]
        [Route("GetAllVehicle")]
        public ActionResult GetAllVehicle()
        {
            var response = _vehicleService.GetAllVehicle();

            return Ok(new { response });
        }

        [HttpGet]
        [Route("GetVehicle/{id}")]
        public ActionResult GetVehicle(int id)
        {
            var response = _vehicleService.GetVehicleById(id);

            return Ok(new { response });
        }

        [HttpPost]
        [Route("AddVehicle")]
        public ActionResult AddVehicle(VehicleDto vehicle)
        {
            var response = _vehicleService.AddVehicle(vehicle);

            return Ok(new { response });
        }

        [HttpPost]
        [Route("UpdateVehicle")]
        public ActionResult UpdateVehicle(VehicleDto vehicle)
        {
            var response = _vehicleService.UpdateVehicle(vehicle);

            return Ok(new { response });
        }

        [HttpPost]
        [Route("DeleteVehicle")]
        public ActionResult DeleteVehicle(int id)
        {
            var response = _vehicleService.DeleteVehicle(id);

            return Ok(new { response });
        }
    }
}

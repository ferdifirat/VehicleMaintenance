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
    public class StatusController : ControllerBase
    {
        private readonly IStatusService _statusService;
        public StatusController(IStatusService statusService)
        {
            _statusService = statusService;
        }

        [HttpGet]
        [Route("GetAllStatus")]
        public ActionResult GetAllStatus()
        {
            var response = _statusService.GetAllStatus();

            return Ok(new { response });
        }

        [HttpGet]
        [Route("GetStatus")]
        public ActionResult GetStatus(int id)
        {
            var response = _statusService.GetStatusById(id);

            return Ok(new { response });
        }

        [HttpPost]
        [Route("AddStatus")]
        public ActionResult AddStatus(StatusDto status)
        {
            var response = _statusService.AddStatus(status);

            return Ok(new { response });
        }

        [HttpPost]
        [Route("UpdateStatus")]
        public ActionResult UpdateStatus(StatusDto status)
        {
            var response = _statusService.UpdateStatus(status);

            return Ok(new { response });
        }

        [HttpPost]
        [Route("DeleteStatus")]
        public ActionResult DeleteStatus(int id)
        {
            var response = _statusService.DeleteStatus(id);

            return Ok(new { response });
        }

    }
}

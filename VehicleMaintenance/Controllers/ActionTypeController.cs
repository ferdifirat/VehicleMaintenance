using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VehicleMaintenance.Business.Abstract;
using VehicleMaintenance.Business.CustomExtensions;
using VehicleMaintenance.Entity.DTOs;

namespace VehicleActionType.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ServiceFilter(typeof(TokenAttribute))]
    public class ActionTypeController : ControllerBase
    {
        private readonly IActionTypeService _actionTypeService;
        public ActionTypeController(IActionTypeService actionTypeService)
        {
            _actionTypeService = actionTypeService;
        }

        [HttpGet]
        [Route("GetAllActionType")]
        public ActionResult GetAllActionType()
        {
            var response = _actionTypeService.GetAllActionType();

            return Ok(new { response });
        }

        [HttpGet]
        [Route("GetActionType/{id}")]
        public ActionResult GetActionType(int id)
        {
            var response = _actionTypeService.GetActionTypeById(id);

            return Ok(new { response });
        }

        [HttpPost]
        [Route("AddActionType")]
        public ActionResult AddActionType(ActionTypeDto actionType)
        {
            var response = _actionTypeService.AddActionType(actionType);

            return Ok(new { response });
        }

        [HttpPost]
        [Route("UpdateActionType")]
        public ActionResult UpdateActionType(ActionTypeDto actionType)
        {
            var response = _actionTypeService.UpdateActionType(actionType);

            return Ok(new { response });
        }

        [HttpPost]
        [Route("DeleteActionType/{id}")]
        public ActionResult DeleteActionType(int id)
        {
            var response = _actionTypeService.DeleteActionType(id);

            return Ok(new { response });
        }
    }
}

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
    public class PictureGroupController : ControllerBase
    {
        private readonly IPictureGroupService _pictureGroupService;
        public PictureGroupController(IPictureGroupService pictureGroupService)
        {
            _pictureGroupService = pictureGroupService;
        }

        [HttpGet]
        [Route("GetAllPictureGroup")]
        public ActionResult GetAllPictureGroup()
        {
            var response = _pictureGroupService.GetAllPictureGroup();

            return Ok(new { response });
        }

        [HttpGet]
        [Route("GetPictureGroup/{id}")]
        public ActionResult GetPictureGroup(int id)
        {
            var response = _pictureGroupService.GetPictureGroupById(id);

            return Ok(new { response });
        }

        [HttpPost]
        [Route("AddPictureGroup")]
        public ActionResult AddPictureGroup(PictureGroupDto pictureGroup)
        {
            var response = _pictureGroupService.AddPictureGroup(pictureGroup);

            return Ok(new { response });
        }

        [HttpPost]
        [Route("UpdatePictureGroup")]
        public ActionResult UpdatePictureGroup(PictureGroupDto pictureGroup)
        {
            var response = _pictureGroupService.UpdatePictureGroup(pictureGroup);

            return Ok(new { response });
        }

        [HttpPost]
        [Route("DeletePictureGroup/{id}")]
        public ActionResult DeletePictureGroup(int id)
        {
            var response = _pictureGroupService.DeletePictureGroup(id);

            return Ok(new { response });
        }
    }
}

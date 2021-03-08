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
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        [Route("GetUsers")]
        public ActionResult GetUsers()
        {
            var response = _userService.GetUsers();

            return Ok(new { response });
        }

        [HttpGet]
        [Route("GetUser/{id}")]
        public ActionResult GetUser(int id)
        {
            var response = _userService.GetUser(id);

            return Ok(new { response });
        }


        [HttpPost]
        [Route("UpdateUser")]
        public ActionResult UpdateUser(UserDto userDto)
        {
            var response = _userService.UpdateUser(userDto);

            return Ok(new { response });
        }

        [HttpPost]
        [Route("DeleteUser/{id}")]
        public ActionResult DeleteUser(int id)
        {
            var response = _userService.DeleteUser(id);

            return Ok(new { response });
        }
    }
}

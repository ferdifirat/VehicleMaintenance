using Microsoft.AspNetCore.Mvc;
using VehicleMaintenance.Business.Abstract;
using VehicleMaintenance.Entity.DTOs;

namespace VehicleMaintenance.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;

        public AuthController(IUserService userService)
        {
            _userService = userService;
        }


        [HttpPost]
        [Route("Login")]
        public IActionResult Login(LoginDto dto)
        {
            var response = _userService.Login(dto);

            return Ok(new { response });

        }

        [HttpPost]
        [Route("Register")]
        public IActionResult Register(RegisterDto dto)
        {
            var response = _userService.Register(dto);

            return Ok(new { response });

        }

    }
}

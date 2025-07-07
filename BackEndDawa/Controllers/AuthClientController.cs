using BackEndDawa.Models;
using BackEndDawa.Services.Application;
using BackEndDawa.Services.Ports;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BackEndDawa.Controllers
{
    [Route("api/[controller]")]
    [AllowAnonymous]
    [ApiController]
    public class AuthClientController : ControllerBase
    {
        private readonly IUserClient _service;
        private readonly ServiceUtility _utility;

        public AuthClientController(IUserClient service, ServiceUtility utility)
        {
            _service = service;
            _utility = utility;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] User user)
        {
            try
            {
                var userFound = await _service.LoginUser(user);

                if (userFound != null)
                {
                    return Ok(new
                    {
                        isSucces = true,
                        token = _utility.GenerateUserJwtToken(userFound, "client")
                    });
                }

                return Ok(new { isSucces = false, token = "" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new { isSucces = false, message = ex.Message });
            }
        }

        [HttpPost("register/{password}")]
        public async Task<IActionResult> Register([FromBody] Client client, string password)
        {
            try
            {
                var userClient = await _service.RegisterUser(client, password);
                return Ok(new { isSucces = true, result = userClient });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new { isSucces = false, message = ex.Message });
            }
        }
    }
}

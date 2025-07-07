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
    public class AuthCompanyController : ControllerBase
    {

        private readonly IUserCompany _service;
        private readonly ServiceUtility _utility;

        public AuthCompanyController(IUserCompany service, ServiceUtility utility)
        {
            _service = service;
            _utility = utility;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] User user)
        {
            try
            {
                //UserCompany
                var userFound = await _service.LoginUser(user);

                if (userFound != null)
                {
                    return StatusCode(StatusCodes.Status200OK, 
                        new {isSucces = true, token = _utility.GenerateUserJwtToken(userFound, "company")});
                }
                else
                {
                    return StatusCode(StatusCodes.Status200OK, new { isSucces = false, token = "" });
                }

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new {isSucces = false, mesagge = ex});

            }

        }


        [HttpPost("register/{passwoord}")]
        public async Task<IActionResult> Register([FromBody] Company company, string passwoord)
        {
            try
            {
                var userCompany = await _service.RegisterUser(company, passwoord);
                return StatusCode(StatusCodes.Status200OK, new { isSucces = true,  result = userCompany });

            } catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { isSucces = false, result = ex });
            }
        }
    }
}
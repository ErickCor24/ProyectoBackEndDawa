using BackEndDawa.Models;
using BackEndDawa.Services.Ports;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BackEndDawa.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class CompanyController : ControllerBase
    {

        private readonly ICompany _companyService;

        public CompanyController(ICompany companyService)
        {
            _companyService = companyService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var companies = await _companyService.GetAllCompaniesAsync();
                return StatusCode(StatusCodes.Status200OK, new {isSucces = true, result = companies});
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status200OK, new { isSucces = true, result = ex });
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var company = await _companyService.GetCompanyByIdAsync(id);
                return StatusCode(StatusCodes.Status200OK, new { isSucces = false, data = company });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status200OK, new { isSucces = false, data = ex });
            }
        }

        [HttpPut]
        public async Task<ActionResult<Company>> Put([FromBody] Company company)
        {
            try
            {
                var updatedCompany = await _companyService.UpdateCompanyAsync(company);
                return StatusCode(StatusCodes.Status200OK, new { isSucces = false, data = updatedCompany });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status200OK, new { isSucces = false, error = ex });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _companyService.DeletCompanyAsync(id);
                return StatusCode(StatusCodes.Status200OK, new { isSucces = true, data = $"Company with {id} has deleted"});
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status200OK, new { isSucces = false, data = ex });
            }
        }
    }
}

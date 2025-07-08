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
                return StatusCode(StatusCodes.Status200OK, new { isSucces = false, result = ex });
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var company = await _companyService.GetCompanyByIdAsync(id);
                return StatusCode(StatusCodes.Status200OK, new { isSucces = true, result = company });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status200OK, new { isSucces = false, result = ex });
            }
        }

        [HttpPut ("{id}")]
        public async Task<ActionResult<Company>> Put([FromBody] Company company, int id)
        {
            company.Id = id;
            try
            {
                var updatedCompany = await _companyService.UpdateCompanyAsync(company);
                return StatusCode(StatusCodes.Status200OK, new { isSucces = true, result = updatedCompany });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status200OK, new { isSucces = false, result = ex });
            }
        }

        [HttpGet ("byname")]
        public async Task<IActionResult> GetByName(string name)
        {
            try
            {
                var companies = await _companyService.GetCompanyByNameAsync(name);
                return StatusCode(StatusCodes.Status200OK, new { isSucces = true, result = companies });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status200OK, new { isSucces = false, result = ex });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _companyService.DeletCompanyAsync(id);
                return StatusCode(StatusCodes.Status200OK, new { isSucces = true, result = $"Company with {id} has deleted"});
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status200OK, new { isSucces = false, result = ex });
            }
        }
    }
}

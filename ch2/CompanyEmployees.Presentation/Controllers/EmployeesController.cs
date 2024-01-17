using CompanyEmployees.Presentation.ActionFilters;
using Microsoft.AspNetCore.Mvc;
using Service.Contracts;
using Shared.DataTransferObjects;
using Shared.RequestFeatures;
using System.Text.Json;

namespace CompanyEmployees.Presentation.Controllers;

[Route("api/companies/{companyId}/employees")]
[ApiController]
public class EmployeesController : ControllerBase
{
    private readonly IServiceManager _service;
    public EmployeesController(IServiceManager service)
        => _service = service;

    [HttpGet]
    public async Task<IActionResult> GetEmployeesFromCompany(Guid companyId, 
        [FromQuery] EmployeeParameters employeeParameters)
    {
        var pagedResult = await _service.EmployeeService.GetEmployeesAsync(companyId, employeeParameters, false);

        Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(pagedResult.metaData));

        return Ok(new
        {
           pagedResult.employees,
           pagedResult.metaData
        });
    }

    [HttpGet("{id:guid}", Name = "GetEmployeeForCompany")]
    public async Task<IActionResult> GetEmployeeForCompany(Guid companyId, Guid id)
    {
        var employee = await _service.EmployeeService.GetEmployeeAsync(companyId, id, false);
        return Ok(employee);
    }

    [HttpPost]
    [ServiceFilter(typeof(ValidationFilterAttribute))]
    public async Task<IActionResult> CreateEmployeeForCompany(Guid companyId, 
        [FromBody] EmployeeForCreationDto employee)
    {
        var employeeToReturn = await _service.EmployeeService.CreateEmployeeForCompanyAsync(companyId,
            employee, false);

        return CreatedAtRoute("GetEmployeeForCompany",
            new { companyId, id = employeeToReturn.Id }, employeeToReturn);
    }
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteEmployeeForCompany(Guid companyId, Guid id)
    {
        await _service.EmployeeService.DeleteEmployeeAsync(companyId, id, false);
        return NoContent();
    }

    [HttpPut("{id:guid}")]
    [ServiceFilter(typeof(ValidationFilterAttribute))]
    public async Task<IActionResult> UpdateEmployeeForCompany(Guid companyId, Guid id, [FromBody] EmployeeForUpdateDto employee)
    {
        await _service.EmployeeService.UpdateEmployeeForCompanyAsync(companyId, id, employee, false, true);
        return NoContent();
    }
}

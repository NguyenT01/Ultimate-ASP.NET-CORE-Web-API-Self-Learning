using Microsoft.AspNetCore.Mvc;
using Service.Contracts;
using Shared.DataTransferObjects;

namespace CompanyEmployees.Presentation.Controllers;

[Route("api/companies/{companyId}/employees")]
[ApiController]
public class EmployeesController : ControllerBase
{
    private readonly IServiceManager _service;
    public EmployeesController(IServiceManager service)
        => _service = service;

    [HttpGet]
    public IActionResult GetEmployeesFromCompany(Guid companyId)
    {
        var employees = _service.EmployeeService.GetEmployees(companyId, false);

        return Ok(employees);
    }

    [HttpGet("{id:guid}", Name = "GetEmployeeForCompany")]
    public IActionResult GetEmployeeForCompany(Guid companyId, Guid id)
    {
        var employee = _service.EmployeeService.GetEmployee(companyId, id, false);
        return Ok(employee);
    }

    [HttpPost]
    public IActionResult CreateEmployeeForCompany(Guid companyId, 
        [FromBody] EmployeeForCreationDto employee)
    {
        if (employee is null)
            return BadRequest("EmployeeForCreationDto is null");

        // Gọi ModelState.IsValid để tùy chỉnh các việc thực hiện sau khi Validate Model
        if (!ModelState.IsValid)
            return UnprocessableEntity(ModelState);

        var employeeToReturn = _service.EmployeeService.CreateEmployeeForCompany(companyId,
            employee, false);

        return CreatedAtRoute("GetEmployeeForCompany",
            new { companyId, id = employeeToReturn.Id }, employeeToReturn);
    }
    [HttpDelete("{id:guid}")]
    public IActionResult DeleteEmployeeForCompany(Guid companyId, Guid id)
    {
        _service.EmployeeService.DeleteEmployee(companyId, id, false);

        return NoContent();
    }

    [HttpPut("{id:guid}")]
    public IActionResult UpdateEmployeeForCompany(Guid companyId, Guid id, [FromBody] EmployeeForUpdateDto employee)
    {
        if (employee is null)
            return BadRequest("EmployeeForUpdateDto is null");
        if (!ModelState.IsValid)
            return UnprocessableEntity(ModelState);

        _service.EmployeeService.UpdateEmployeeForCompany(companyId, id, employee, false, true);
        return NoContent();
    }
}

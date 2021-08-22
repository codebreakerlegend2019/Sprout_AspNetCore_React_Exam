using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sprout.Exam.DataAccess.Commands;
using Sprout.Exam.DataAccess.Dtos;
using Sprout.Exam.DataAccess.Queries;
using System.Linq;
using System.Threading.Tasks;

namespace Sprout.Exam.WebApp.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public EmployeesController(IMediator mediator)
        {
            _mediator = mediator;
        }
        /// <summary>
        /// Refactor this method to go through proper layers and fetch from the DB.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var employees = await _mediator.Send(new GetEmployeesQuery());
            
            if(!employees.Any())
            {
                return NoContent();
            }

            return Ok(employees);
        }

        /// <summary>
        /// Refactor this method to go through proper layers and fetch from the DB.
        /// </summary>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var employee = await _mediator.Send(new GetEmployeeByIdQuery(id));

            if(employee == null)
            {
                return NotFound();
            }

            return Ok(employee);
        }

        /// <summary>
        /// Refactor this method to go through proper layers and update changes to the DB.
        /// </summary>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> Put([FromRoute] int id, EmployeeEditDto dto)
        {
            var employee = await _mediator.Send(new GetEmployeeByIdQuery(id));

            if (employee == null)
            {
                return NotFound();
            }

            var updateEmployeeRequest = await _mediator.Send(new EditEmployeeCommand(employee, dto));

            return StatusCode(updateEmployeeRequest.StatusCode, updateEmployeeRequest);
        }

        /// <summary>
        /// Refactor this method to go through proper layers and insert employees to the DB.
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Post(EmployeeAddDto dto)
        {
            var addEmployeeRequest = await _mediator.Send(new AddEmployeeCommand(dto));

            return StatusCode(addEmployeeRequest.StatusCode, addEmployeeRequest);
        }


        /// <summary>
        /// Refactor this method to go through proper layers and perform soft deletion of an employee to the DB.
        /// </summary>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var employee = await _mediator.Send(new GetEmployeeByIdQuery(id));

            if (employee == null)
            {
                return NotFound();
            }

            var deleteEmployeeRequest = await _mediator.Send(new DeleteEmployeeCommand(employee));

            return StatusCode(deleteEmployeeRequest.StatusCode, deleteEmployeeRequest);
        }



        /// <summary>
        /// Refactor this method to go through proper layers and use Factory pattern
        /// </summary>
        /// <param name="id"></param>
        /// <param name="absentDays"></param>
        /// <param name="workedDays"></param>
        /// <returns></returns>
        [HttpPost("{id}/{absentdays}/{workedDays}/calculate")]
        public async Task<IActionResult> Calculate([FromRoute] int id, [FromRoute] decimal absentDays, [FromRoute] decimal workedDays)
        {
            var employeee = await _mediator.Send(new GetEmployeeByIdQuery(id));

            if(employeee == null)
            {
                return NotFound();
            }

            var computeRequest = await _mediator.Send(new ComputeSalaryCommand(employeee, absentDays, workedDays));

            return Ok(computeRequest);

        }

    }
}

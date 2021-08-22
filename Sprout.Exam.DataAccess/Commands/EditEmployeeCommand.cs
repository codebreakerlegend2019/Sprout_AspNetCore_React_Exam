using MediatR;
using Sprout.Exam.Common.ResultHandlingDto;
using Sprout.Exam.DataAccess.Dtos;
using Sprout.Exam.Models;

namespace Sprout.Exam.DataAccess.Commands
{
    public record EditEmployeeCommand(Employee OldValue, EmployeeEditDto NewValue):IRequest<ActionResult>;
}

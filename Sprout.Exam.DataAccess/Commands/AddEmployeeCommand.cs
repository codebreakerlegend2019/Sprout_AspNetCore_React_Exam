using MediatR;
using Sprout.Exam.Common.ResultHandlingDto;
using Sprout.Exam.DataAccess.Dtos;

namespace Sprout.Exam.DataAccess.Commands
{
    public record AddEmployeeCommand(EmployeeAddDto Dto):IRequest<ActionResult>;
}

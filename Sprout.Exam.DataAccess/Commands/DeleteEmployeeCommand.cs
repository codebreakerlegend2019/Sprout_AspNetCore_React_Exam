using MediatR;
using Sprout.Exam.Common.ResultHandlingDto;
using Sprout.Exam.Models;

namespace Sprout.Exam.DataAccess.Commands
{
    public record DeleteEmployeeCommand(Employee Employee):IRequest<ActionResult>;
}
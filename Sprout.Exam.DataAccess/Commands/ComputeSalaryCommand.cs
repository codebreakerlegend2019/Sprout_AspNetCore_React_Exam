using MediatR;
using Sprout.Exam.DataAccess.Dtos;
using Sprout.Exam.Models;

namespace Sprout.Exam.DataAccess.Commands
{
    public record ComputeSalaryCommand(Employee Employee, decimal Absences, decimal WorkedDays):IRequest<EmployeeSalaryComputationDto>;
}

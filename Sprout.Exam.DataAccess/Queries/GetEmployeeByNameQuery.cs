using MediatR;
using Sprout.Exam.Models;

namespace Sprout.Exam.DataAccess.Queries
{
    public record GetEmployeeByNameQuery(string Name):IRequest<Employee>;
}

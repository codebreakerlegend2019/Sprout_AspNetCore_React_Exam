using MediatR;
using Sprout.Exam.Models;

namespace Sprout.Exam.DataAccess.Queries
{
    public record GetEmployeeByIdQuery(int Id):IRequest<Employee>;
}

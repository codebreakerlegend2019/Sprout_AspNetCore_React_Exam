using MediatR;
using Sprout.Exam.Models;

namespace Sprout.Exam.DataAccess.Queries
{
    public record GetEmployeeByTinQuery(string Tin):IRequest<Employee>;
}

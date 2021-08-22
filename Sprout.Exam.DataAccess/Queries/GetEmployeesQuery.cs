using MediatR;
using Sprout.Exam.Models;
using System.Collections.Generic;

namespace Sprout.Exam.DataAccess.Queries
{
    public record GetEmployeesQuery():IRequest<List<Employee>>;
}

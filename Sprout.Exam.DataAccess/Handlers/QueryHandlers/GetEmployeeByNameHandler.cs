using MediatR;
using Microsoft.EntityFrameworkCore;
using Sprout.Exam.DataAccess.Contracts;
using Sprout.Exam.DataAccess.Queries;
using Sprout.Exam.Models;
using System.Threading;
using System.Threading.Tasks;

namespace Sprout.Exam.DataAccess.Handlers.QueryHandlers
{
    public class GetEmployeeByNameHandler : IRequestHandler<GetEmployeeByNameQuery, Employee>
    {
        private readonly IEmployeeContext _context;

        public GetEmployeeByNameHandler(IEmployeeContext context)
        {
            _context = context;
        }
        public async Task<Employee> Handle(GetEmployeeByNameQuery request, CancellationToken cancellationToken)
        {
            return await _context.Employees.FirstOrDefaultAsync(e => !e.IsDeleted && e.FullName.ToLower() == request.Name.ToLower());
        }
    }
}

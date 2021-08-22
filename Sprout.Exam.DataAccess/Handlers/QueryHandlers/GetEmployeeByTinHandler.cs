using MediatR;
using Microsoft.EntityFrameworkCore;
using Sprout.Exam.DataAccess.Contracts;
using Sprout.Exam.DataAccess.Queries;
using Sprout.Exam.Models;
using System.Threading;
using System.Threading.Tasks;

namespace Sprout.Exam.DataAccess.Handlers.QueryHandlers
{
    public class GetEmployeeByTinHandler : IRequestHandler<GetEmployeeByTinQuery, Employee>
    {
        private readonly IEmployeeContext _context;

        public GetEmployeeByTinHandler(IEmployeeContext context)
        {
            _context = context;
        }
        public async Task<Employee> Handle(GetEmployeeByTinQuery request, CancellationToken cancellationToken)
        {
            return await _context.Employees.FirstOrDefaultAsync(e => !e.IsDeleted && e.Tin.Trim().ToLower() == request.Tin.Trim().ToLower());
        }
    }
}

using MediatR;
using Microsoft.EntityFrameworkCore;
using Sprout.Exam.DataAccess.Contracts;
using Sprout.Exam.DataAccess.Queries;
using Sprout.Exam.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Sprout.Exam.DataAccess.Handlers.QueryHandlers
{
    public class GetEmployeesHandler : IRequestHandler<GetEmployeesQuery, List<Employee>>
    {
        private readonly IEmployeeContext _context;

        public GetEmployeesHandler(IEmployeeContext context)
        {
            _context = context;
        }
        public async Task<List<Employee>> Handle(GetEmployeesQuery request, CancellationToken cancellationToken)
        {
            return await _context.Employees
                .Where(e => !e.IsDeleted)
                .ToListAsync();
        }
    }
}

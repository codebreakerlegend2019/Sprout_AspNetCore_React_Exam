using AutoMapper;
using MediatR;
using Sprout.Exam.Common.Enums;
using Sprout.Exam.Common.ResultHandlingDto;
using Sprout.Exam.DataAccess.Commands;
using Sprout.Exam.DataAccess.Contracts;
using System.Threading;
using System.Threading.Tasks;

namespace Sprout.Exam.DataAccess.Handlers.CommandHandlers
{
    public class DeleteEmployeeHandler : IRequestHandler<DeleteEmployeeCommand, ActionResult>
    {
        private readonly IEmployeeContext _context;
        private readonly IMapper _mapper;

        public DeleteEmployeeHandler(IEmployeeContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<ActionResult> Handle(DeleteEmployeeCommand request, CancellationToken cancellationToken)
        {
            request.Employee.IsDeleted = true;

            return await _context.SaveAllChangesAsync(CommandType.DELETE);
        }
    }
}

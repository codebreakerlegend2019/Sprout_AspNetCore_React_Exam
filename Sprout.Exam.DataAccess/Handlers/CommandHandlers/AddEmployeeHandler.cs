using AutoMapper;
using MediatR;
using Sprout.Exam.Common.Constants;
using Sprout.Exam.Common.Enums;
using Sprout.Exam.Common.ResultHandlingDto;
using Sprout.Exam.DataAccess.Commands;
using Sprout.Exam.DataAccess.Contracts;
using Sprout.Exam.DataAccess.Queries;
using Sprout.Exam.Models;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Sprout.Exam.DataAccess.Handlers.CommandHandlers
{
    public class AddEmployeeHandler : IRequestHandler<AddEmployeeCommand, ActionResult>
    {
        private readonly IEmployeeContext _context;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public AddEmployeeHandler(IEmployeeContext context, IMapper mapper,
            IMediator mediator) 
        {
            _context = context;
            _mapper = mapper;
            _mediator = mediator;
        }
        public async Task<ActionResult> Handle(AddEmployeeCommand request, CancellationToken cancellationToken)
        {
            var isEmployeeAlreadyExisted = await _mediator.Send(new GetEmployeeByNameQuery(request.Dto.FullName))!= null;

            if(isEmployeeAlreadyExisted)
            {
                return new ActionResult(ValidationMessages.EMPLOYEE_ALREADY_EXISTED, HttpStatusCode.Ambiguous);
            }

            var isEmployeeTinAlreadyExisted = await _mediator.Send(new GetEmployeeByTinQuery(request.Dto.Tin)) != null;

            if (isEmployeeTinAlreadyExisted)
            {
                return new ActionResult(ValidationMessages.EMPLOYEE_TIN_ALEADY_EXISTED, HttpStatusCode.Ambiguous);
            }

            var employee = _mapper.Map<Employee>(request.Dto);

            _context.Employees.Add(employee);

            return await  _context.SaveAllChangesAsync(CommandType.ADD);
        }
    }
}

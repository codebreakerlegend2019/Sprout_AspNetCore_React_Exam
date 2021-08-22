using AutoMapper;
using MediatR;
using Sprout.Exam.Common.Constants;
using Sprout.Exam.Common.Enums;
using Sprout.Exam.Common.ResultHandlingDto;
using Sprout.Exam.DataAccess.Commands;
using Sprout.Exam.DataAccess.Contracts;
using Sprout.Exam.DataAccess.Queries;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Sprout.Exam.DataAccess.Handlers.CommandHandlers
{
    public class EditEmployeeHandler : IRequestHandler<EditEmployeeCommand, ActionResult>
    {
        private readonly IEmployeeContext _context;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public EditEmployeeHandler(IEmployeeContext context, IMapper mapper,
            IMediator mediator)
        {
            _context = context;
            _mapper = mapper;
            _mediator = mediator;
        }
        public async Task<ActionResult> Handle(EditEmployeeCommand request, CancellationToken cancellationToken)
        {
            var oldValue = request.OldValue;
            var newValue = request.NewValue;

            if (oldValue.FullName.Trim() != newValue.FullName)
            {
                var isEmployeeAlreadyExisted = await _mediator.Send(new GetEmployeeByNameQuery(newValue.FullName)) != null;

                if (isEmployeeAlreadyExisted)
                {
                    return new ActionResult(ValidationMessages.EMPLOYEE_ALREADY_EXISTED, HttpStatusCode.Ambiguous);
                }
            }

            if (oldValue.Tin.Trim() != newValue.Tin.Trim())
            {
                var isEmployeeTinAlreadyExisted = await _mediator.Send(new GetEmployeeByNameQuery(newValue.Tin)) != null;

                if (isEmployeeTinAlreadyExisted)
                {
                    return new ActionResult(ValidationMessages.EMPLOYEE_TIN_ALEADY_EXISTED, HttpStatusCode.Ambiguous);
                }

                newValue.Tin = newValue.Tin.Trim();
            }

            _mapper.Map(newValue, oldValue);

            return await _context.SaveAllChangesAsync(CommandType.UPDATE);
        }
    }
}

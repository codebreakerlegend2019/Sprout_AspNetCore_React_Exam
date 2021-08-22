using FakeItEasy;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Sprout.Exam.Common.Constants;
using Sprout.Exam.DataAccess.Commands;
using Sprout.Exam.DataAccess.Queries;
using Sprout.Exam.Models;
using Sprout.Exam.WebApp.Controllers;
using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Sprout.Exam.WebApp.Test.EmployeeTests
{
    public class DeleteEmployeeTests
    {

        [Fact]
        public async void Failed_To_Delete_Employee_Due_To_NotFound()
        {
            var mediator = A.Fake<IMediator>();

            var employeeId = 1;

            Employee employeeToBeDeleted = null;

            var cancellationToken = new CancellationToken(false);

            A.CallTo(() => mediator.Send(new GetEmployeeByIdQuery(employeeId), cancellationToken))
                .Returns(Task.FromResult(employeeToBeDeleted));

            var controller = new EmployeesController(mediator);

            var actionResult = await controller.Delete(employeeId);

            var result = actionResult as NotFoundResult;

            Assert.Equal(404, result.StatusCode);
        }

        [Fact]
        public async void SuccessFully_Updated_Employee()
        {
            var mediator = A.Fake<IMediator>();

            var cancellationToken = new CancellationToken(false);

            var employeeId = 1;

            var employeeToBeDeleted = new Employee
            {
                Id = employeeId,
                FullName = "John Doe",
                Birthdate = new DateTime(),
                EmployeeTypeId = 1,
                Tin = "123-123-123"
            };

            A.CallTo(() => mediator.Send(new GetEmployeeByIdQuery(employeeId), cancellationToken))
                .Returns(Task.FromResult(employeeToBeDeleted));

            A.CallTo(() => mediator.Send(new DeleteEmployeeCommand(employeeToBeDeleted), cancellationToken))
                .Returns(Task.FromResult(new Common.ResultHandlingDto.ActionResult(ValidationMessages.SAVE_SUCCESSFULLY, HttpStatusCode.OK)));

            var controller = new EmployeesController(mediator);

            var actionResult = await controller.Delete(employeeId);

            var result = actionResult as ObjectResult;

            Assert.Equal(200, result.StatusCode);
        }
    }
}

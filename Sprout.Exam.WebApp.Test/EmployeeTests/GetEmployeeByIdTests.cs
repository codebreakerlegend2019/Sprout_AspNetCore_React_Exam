using FakeItEasy;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Sprout.Exam.DataAccess.Queries;
using Sprout.Exam.Models;
using Sprout.Exam.WebApp.Controllers;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Sprout.Exam.WebApp.Test.EmployeeTests
{
    public class GetEmployeeByIdTests
    {
        [Fact]
        public async void Failed_Due_To_NotFound()
        {
            var mediator = A.Fake<IMediator>();

            var cancellationToken = new CancellationToken(false);

            var employeeId = 1;

            Employee employee = null;

            A.CallTo(() => mediator.Send(new GetEmployeeByIdQuery(employeeId), cancellationToken))
                .Returns(Task.FromResult(employee));

            var controller = new EmployeesController(mediator);

            var actionResult = await controller.GetById(employeeId);

            var result = actionResult as NotFoundResult;

            Assert.Equal(404, result.StatusCode);
        }

        [Fact]
        public async void Success_Fetch_Employee()
        {
            var mediator = A.Fake<IMediator>();

            var cancellationToken = new CancellationToken(false);

            var employeeId = 1;

            var fakeEmployee = A.Fake<Employee>();

            A.CallTo(() => mediator.Send(new GetEmployeeByIdQuery(employeeId), cancellationToken))
                .Returns(Task.FromResult(fakeEmployee));

            var controller = new EmployeesController(mediator);

            var actionResult = await controller.GetById(employeeId);

            var result = actionResult as OkObjectResult;

            Assert.Equal(200, result.StatusCode);
        }
    }
}

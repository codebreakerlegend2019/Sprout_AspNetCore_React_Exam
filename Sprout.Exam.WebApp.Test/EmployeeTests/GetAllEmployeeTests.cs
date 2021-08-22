using FakeItEasy;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Sprout.Exam.DataAccess.Queries;
using Sprout.Exam.Models;
using Sprout.Exam.WebApp.Controllers;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Sprout.Exam.WebApp.Test.EmployeeTests
{
    public class GetAllEmployeeTests
    {

        [Fact]
        public async void Failed_Due_To_No_Content()
        {
            var mediator = A.Fake<IMediator>();

            var cancellationToken = new CancellationToken(false);
            
            var employees = new List<Employee>();

            A.CallTo(() => mediator.Send(new GetEmployeesQuery(), cancellationToken))
                .Returns(Task.FromResult(employees));

            var controller = new EmployeesController(mediator);

            var actionResult = await controller.Get();

            var result = actionResult as NoContentResult;

            Assert.Equal(204, result.StatusCode);
        }

        [Fact]
        public async void Success_Fetch_Employees()
        {
            var mediator = A.Fake<IMediator>();

            var cancellationToken = new CancellationToken(false);

            var fakeEmployeeCount = 6;

            var fakeEmployees = A.CollectionOfDummy<Employee>(fakeEmployeeCount).ToList();

            A.CallTo(() => mediator.Send(new GetEmployeesQuery(), cancellationToken))
                .Returns(Task.FromResult(fakeEmployees));

            var controller = new EmployeesController(mediator);

            var actionResult = await controller.Get();

            var result = actionResult as OkObjectResult;

            var returnedEmployees = result.Value as List<Employee>;

            Assert.Equal(fakeEmployeeCount,returnedEmployees.Count);
        }
    }
}

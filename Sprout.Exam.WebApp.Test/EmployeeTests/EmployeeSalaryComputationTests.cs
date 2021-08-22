using FakeItEasy;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Sprout.Exam.DataAccess.Commands;
using Sprout.Exam.DataAccess.Dtos;
using Sprout.Exam.DataAccess.Queries;
using Sprout.Exam.Models;
using Sprout.Exam.WebApp.Controllers;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Sprout.Exam.WebApp.Test.EmployeeTests
{
    public class EmployeeSalaryComputationTests
    {

        [Fact]
        public async void Failed_To_Compute_Employee_Salary_Due_To_NotFound()
        {
            var mediator = A.Fake<IMediator>();

            var employeeId = 1;

            Employee employee = null;

            var cancellationToken = new CancellationToken(false);

            A.CallTo(() => mediator.Send(new GetEmployeeByIdQuery(employeeId), cancellationToken))
                .Returns(Task.FromResult(employee));

            var controller = new EmployeesController(mediator);

            var actionResult = await controller.Delete(employeeId);

            var result = actionResult as NotFoundResult;

            Assert.Equal(404, result.StatusCode);
        }

        [Fact]
        public async void Success_Computation_Regular_Employee_Salary()
        {
            var mediator = A.Fake<IMediator>();

            var employeeId = 1;

            decimal absences = 1;

            var regularEmployeeToBeComputed = new Employee
            {
                Id = employeeId,
                FullName = "John Doe",
                Birthdate = new DateTime(),
                EmployeeTypeId = (int) Common.Enums.EmployeeType.Regular,
                Tin = "123-123-123"
            };

            var cancellationToken = new CancellationToken(false);

            var dummyComputationResult = A.Fake <EmployeeSalaryComputationDto>();

            A.CallTo(() => mediator.Send(new GetEmployeeByIdQuery(employeeId), cancellationToken))
                .Returns(Task.FromResult(regularEmployeeToBeComputed));

            A.CallTo(() => mediator.Send(new ComputeSalaryCommand(regularEmployeeToBeComputed,absences,0), cancellationToken))
             .Returns(Task.FromResult(dummyComputationResult));

            var controller = new EmployeesController(mediator);

            var actionResult = await controller.Calculate(employeeId,absences,0);

            var result = actionResult as OkObjectResult;

            Assert.Equal(200, result.StatusCode);
        }

        [Fact]
        public async void Success_Computation_Contractual_Employee_Salary()
        {
            var mediator = A.Fake<IMediator>();


            var employeeId = 1;

            decimal workedDays = 15.5M;

            var contractualEmployeeToBeComputed = new Employee
            {
                Id = employeeId,
                FullName = "John Doe",
                Birthdate = new DateTime(),
                EmployeeTypeId = (int)Common.Enums.EmployeeType.Contractual,
                Tin = "123-123-123"
            };

            var cancellationToken = new CancellationToken(false);

            var dummyComputationResult = A.Fake<EmployeeSalaryComputationDto>();

            A.CallTo(() => mediator.Send(new GetEmployeeByIdQuery(employeeId), cancellationToken))
                .Returns(Task.FromResult(contractualEmployeeToBeComputed));

            A.CallTo(() => mediator.Send(new ComputeSalaryCommand(contractualEmployeeToBeComputed, 0, workedDays), cancellationToken))
             .Returns(Task.FromResult(dummyComputationResult));

            var controller = new EmployeesController(mediator);

            var actionResult = await controller.Calculate(employeeId, 0, workedDays);

            var result = actionResult as OkObjectResult;

            Assert.Equal(200, result.StatusCode);
        }
    }
}

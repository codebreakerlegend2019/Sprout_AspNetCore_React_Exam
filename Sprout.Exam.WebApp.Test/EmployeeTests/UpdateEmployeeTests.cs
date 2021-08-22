using FakeItEasy;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Sprout.Exam.Common.Constants;
using Sprout.Exam.DataAccess.Commands;
using Sprout.Exam.DataAccess.Dtos;
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
    public class UpdateEmployeeTests
    {

        [Fact]
        public async void Failed_To_Update_Employee_Due_To_NotFound()
        {
            var mediator = A.Fake<IMediator>();

            var employeeUpdatedInfo = new EmployeeEditDto
            {
                FullName = "John Doe",
                Birthdate = new DateTime(),
                EmployeeTypeId = 1,
                Tin = "123-123-123"
            };

            var employeeId = 1;

            Employee employeeToBeUpdated = null;

            var cancellationToken = new CancellationToken(false);

            A.CallTo(() => mediator.Send(new GetEmployeeByIdQuery(employeeId), cancellationToken))
                .Returns(Task.FromResult(employeeToBeUpdated));

            var controller = new EmployeesController(mediator);

            var actionResult = await controller.Put(employeeId,employeeUpdatedInfo);

            var result = actionResult as NotFoundResult;

            Assert.Equal(404, result.StatusCode);
        }

        [Fact]
        public async void Failed_To_Update_Employee_Due_Existing_Name()
        {
            var mediator = A.Fake<IMediator>();

            var cancellationToken = new CancellationToken(false);

            var employeeId = 1;

            var employeeToBeUpdated = new Employee
            {
                Id = employeeId,
                FullName = "John Doe",
                Birthdate = new DateTime(),
                EmployeeTypeId = 1,
                Tin = "123-123-123"
            };

            A.CallTo(() => mediator.Send(new GetEmployeeByIdQuery(employeeId), cancellationToken))
                .Returns(Task.FromResult(employeeToBeUpdated));
      
            var employeeUpdatedInfo = new EmployeeEditDto
            {
                FullName = "John Doe1",
                Birthdate = new DateTime(),
                EmployeeTypeId = 2,
                Tin = "123-123-1233"
            };

            A.CallTo(() => mediator.Send(new EditEmployeeCommand(employeeToBeUpdated,employeeUpdatedInfo), cancellationToken))
                .Returns(Task.FromResult(new Common.ResultHandlingDto.ActionResult(ValidationMessages.EMPLOYEE_ALREADY_EXISTED, HttpStatusCode.Ambiguous)));

            var controller = new EmployeesController(mediator);

            var actionResult = await controller.Put(employeeId, employeeUpdatedInfo);

            var result = actionResult as ObjectResult;

            Assert.Equal(300, result.StatusCode);
        }

        [Fact]
        public async void Failed_To_Update_Employee_Due_Existing_Tin()
        {
            var mediator = A.Fake<IMediator>();

            var cancellationToken = new CancellationToken(false);

            var employeeId = 1;

            var employeeToBeUpdated = new Employee
            {
                Id = employeeId,
                FullName = "John Doe",
                Birthdate = new DateTime(),
                EmployeeTypeId = 1,
                Tin = "123-123-123"
            };

            A.CallTo(() => mediator.Send(new GetEmployeeByIdQuery(employeeId), cancellationToken))
                .Returns(Task.FromResult(employeeToBeUpdated));

            var employeeUpdatedInfo = new EmployeeEditDto
            {
                FullName = "John Doe1",
                Birthdate = new DateTime(),
                EmployeeTypeId = 2,
                Tin = "123-123-12333333"
            };

            A.CallTo(() => mediator.Send(new EditEmployeeCommand(employeeToBeUpdated, employeeUpdatedInfo), cancellationToken))
                .Returns(Task.FromResult(new Common.ResultHandlingDto.ActionResult(ValidationMessages.EMPLOYEE_TIN_ALEADY_EXISTED, HttpStatusCode.Ambiguous)));

            var controller = new EmployeesController(mediator);

            var actionResult = await controller.Put(employeeId, employeeUpdatedInfo);

            var result = actionResult as ObjectResult;

            Assert.Equal(300, result.StatusCode);
        }

        [Fact]
        public async void SuccessFully_Updated_Employee()
        {
            var mediator = A.Fake<IMediator>();

            var cancellationToken = new CancellationToken(false);

            var employeeId = 1;

            var employeeToBeUpdated = new Employee
            {
                Id = employeeId,
                FullName = "John Doe",
                Birthdate = new DateTime(),
                EmployeeTypeId = 1,
                Tin = "123-123-123"
            };

            A.CallTo(() => mediator.Send(new GetEmployeeByIdQuery(employeeId), cancellationToken))
                .Returns(Task.FromResult(employeeToBeUpdated));

            var employeeUpdatedInfo = new EmployeeEditDto
            {
                FullName = "John Doe1",
                Birthdate = new DateTime(),
                EmployeeTypeId = 2,
                Tin = "123-123-1233-123"
            };

            A.CallTo(() => mediator.Send(new EditEmployeeCommand(employeeToBeUpdated, employeeUpdatedInfo), cancellationToken))
                .Returns(Task.FromResult(new Common.ResultHandlingDto.ActionResult(ValidationMessages.SAVE_SUCCESSFULLY, HttpStatusCode.OK)));

            var controller = new EmployeesController(mediator);

            var actionResult = await controller.Put(employeeId, employeeUpdatedInfo);

            var result = actionResult as ObjectResult;

            Assert.Equal(200, result.StatusCode);
        }
    }
}

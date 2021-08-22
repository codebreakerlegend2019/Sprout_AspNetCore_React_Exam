using FakeItEasy;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Sprout.Exam.Common.Constants;
using Sprout.Exam.DataAccess.Commands;
using Sprout.Exam.DataAccess.Dtos;
using Sprout.Exam.WebApp.Controllers;
using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Sprout.Exam.WebApp.Test.EmployeeTests
{
    public class AddingEmployeeTests
    {

        [Fact]
        public async  void Failed_To_Add_Employee_Due_Existing_Name()
        {
            var mediator = A.Fake<IMediator>();

            var employeeToBeAdded = new EmployeeAddDto
            {
                FullName = "John Doe",
                Birthdate = new DateTime(),
                EmployeeTypeId = 1,
                Tin = "123-123-123"
            };

            var cancellationToken = new CancellationToken(false);

            A.CallTo(() => mediator.Send(new AddEmployeeCommand(employeeToBeAdded), cancellationToken))
                .Returns(Task.FromResult(new Common.ResultHandlingDto.ActionResult(ValidationMessages.EMPLOYEE_ALREADY_EXISTED, HttpStatusCode.Ambiguous)));

            var controller = new EmployeesController(mediator);


            var actionResult = await controller.Post(employeeToBeAdded);

            var result = actionResult as ObjectResult;

            Assert.Equal(300,result.StatusCode);
        }


        [Fact]
        public async void Failed_To_Add_Employee_Due_Existing_Tin()
        {
            var mediator = A.Fake<IMediator>();

            var employeeToBeAdded = new EmployeeAddDto
            {
                FullName = "John Doe",
                Birthdate = new DateTime(),
                EmployeeTypeId = 1,
                Tin = "123-123-1233"
            };

            var cancellationToken = new CancellationToken(false);

            A.CallTo(() => mediator.Send(new AddEmployeeCommand(employeeToBeAdded), cancellationToken))
                .Returns(Task.FromResult(new Common.ResultHandlingDto.ActionResult(ValidationMessages.EMPLOYEE_TIN_ALEADY_EXISTED, HttpStatusCode.Ambiguous)));

            var controller = new EmployeesController(mediator);

            var actionResult = await controller.Post(employeeToBeAdded);

            var result = actionResult as ObjectResult;

            Assert.Equal(300, result.StatusCode);
        }

        [Fact]
        public async void Successfully_AddedEmployee()
        {
            var mediator = A.Fake<IMediator>();

            var employeeToBeAdded = new EmployeeAddDto
            {
                FullName = "John Doe",
                Birthdate = new DateTime(),
                EmployeeTypeId = 1,
                Tin = "123-123-123"
            };

            var cancellationToken = new CancellationToken(false);

            A.CallTo(() => mediator.Send(new AddEmployeeCommand(employeeToBeAdded), cancellationToken))
                .Returns(Task.FromResult(new Common.ResultHandlingDto.ActionResult(ValidationMessages.SAVE_SUCCESSFULLY, HttpStatusCode.Created)));

            var controller = new EmployeesController(mediator);

            var actionResult = await controller.Post(employeeToBeAdded);

            var result = actionResult as ObjectResult;

            Assert.Equal(201, result.StatusCode);
        }
    }
}

using MediatR;
using Sprout.Exam.Common.Constants;
using Sprout.Exam.Common.Enums;
using Sprout.Exam.DataAccess.Commands;
using Sprout.Exam.DataAccess.Dtos;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Sprout.Exam.DataAccess.Handlers.CommandHandlers
{
    public class ComputeSalaryHandler : IRequestHandler<ComputeSalaryCommand, EmployeeSalaryComputationDto>
    {
        public Task<EmployeeSalaryComputationDto> Handle(ComputeSalaryCommand request, CancellationToken cancellationToken)
        {
            var employeeType = (EmployeeType)request.Employee.EmployeeTypeId;
            switch (employeeType)
            {
                case EmployeeType.Regular:
                    decimal regularEmployeeSalary = ComputeForRegularEmployees(request.Absences);
                    return Task.FromResult(new EmployeeSalaryComputationDto
                    {
                        FullName = request.Employee.FullName,
                        ComputedValue = regularEmployeeSalary,
                        ComputedValueInText = regularEmployeeSalary.ToString("#,##0.00")
            });
                    break;
                     
                case EmployeeType.Contractual:
                    decimal contractualEmployeeSalary = ComputeForContractualEmployees(request.WorkedDays);
                    return Task.FromResult(new EmployeeSalaryComputationDto
                    {
                        FullName = request.Employee.FullName,
                        ComputedValue = contractualEmployeeSalary,
                        ComputedValueInText = contractualEmployeeSalary.ToString("#,##0.00")
                    });
                    break;
            }

            return Task.FromResult(new EmployeeSalaryComputationDto
            {
                FullName = request.Employee.FullName,
                ComputedValue = 0
            });
        }

        private decimal ComputeForRegularEmployees(decimal absences)
        {
            var perDaySalary = (ComputationConstants.REGULAR_EMPLOYEE_MONTHLY_SALARY / ComputationConstants.REGULAR_EMPLOYEE_TOTAL_WORKING_DAYS);
            var taxAmount = (ComputationConstants.REGULAR_EMPLOYEE_MONTHLY_SALARY * ComputationConstants.TAX);
            var totalMonthlySalary = ComputationConstants.REGULAR_EMPLOYEE_MONTHLY_SALARY - perDaySalary - taxAmount;
            var totalAbsentDeduction = absences * perDaySalary;
            return decimal.Round(totalMonthlySalary - totalAbsentDeduction,2);
        }
        private decimal ComputeForContractualEmployees(decimal workedDays)
        {
            return ComputationConstants.CONTRACTUAL_EMPLOYEE_DAY_RATE * workedDays;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sprout.Exam.DataAccess.Dtos
{
    public class EmployeeSalaryComputationDto
    {
        public string FullName { get; set; }
        public string ComputedValueInText { get; set; }
        public decimal ComputedValue { get; set; }
    }
}

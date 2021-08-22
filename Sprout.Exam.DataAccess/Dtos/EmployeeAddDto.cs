using System;

namespace Sprout.Exam.DataAccess.Dtos
{
    public class EmployeeAddDto
    {
        public string FullName { get; set; }
        public DateTime Birthdate { get; set; }
        public string Tin { get; set; }
        public int EmployeeTypeId { get; set; }
    }
}

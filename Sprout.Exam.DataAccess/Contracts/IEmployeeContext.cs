using Microsoft.EntityFrameworkCore;
using Sprout.Exam.Common.Enums;
using Sprout.Exam.Common.ResultHandlingDto;
using System.Threading.Tasks;

namespace Sprout.Exam.DataAccess.Contracts
{
    public interface IEmployeeContext
    {
        DbSet<Models.EmployeeType> EmployeeTypes { get; set; }
        DbSet<Models.Employee> Employees { get; set; }
        Task<ActionResult> SaveAllChangesAsync(CommandType commandType);
    }
}

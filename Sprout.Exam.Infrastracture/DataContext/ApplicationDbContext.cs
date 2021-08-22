using IdentityServer4.EntityFramework.Options;
using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Sprout.Exam.Common.Constants;
using Sprout.Exam.Common.Enums;
using Sprout.Exam.Common.ResultHandlingDto;
using Sprout.Exam.DataAccess.Contracts;
using Sprout.Exam.Infrastracture.BaseApplicationUser;
using Sprout.Exam.Models;
using System.Net;
using System.Threading.Tasks;

namespace Sprout.Exam.Infrastracture
{
    public class ApplicationDbContext : ApiAuthorizationDbContext<ApplicationUser>,IEmployeeContext
    {
        public ApplicationDbContext(
            DbContextOptions options,
            IOptions<OperationalStoreOptions> operationalStoreOptions) : base(options, operationalStoreOptions)
        {

        }

        public DbSet<Models.EmployeeType> EmployeeTypes { get; set; }
        public DbSet<Employee> Employees { get; set; }

        public async Task<ActionResult> SaveAllChangesAsync(CommandType commandType)
        {
            var httpStatusCode = (commandType == CommandType.UPDATE || commandType ==  CommandType.DELETE) 
                ? HttpStatusCode.OK : HttpStatusCode.Created;

            var hasChanges = await SaveChangesAsync() > 0;

            if(hasChanges)
            {
                return new ActionResult(ValidationMessages.SAVE_SUCCESSFULLY, httpStatusCode);
            }

            return new ActionResult(ValidationMessages.NOTHING_SAVE, HttpStatusCode.BadRequest);
        }
    }
}

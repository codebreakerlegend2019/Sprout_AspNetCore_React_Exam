using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Sprout.Exam.DataAccess;
using Sprout.Exam.DataAccess.Contracts;
using Sprout.Exam.Infrastracture.BaseApplicationUser;

namespace Sprout.Exam.Infrastracture.Extensions
{
    public static class ServiceExtensions
    {
        public static void MainExtension(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDatabase(configuration);
            services.AddMapper();
            services.AddMediatRExtension();
        }

        private static void AddDatabase(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddScoped<IEmployeeContext, ApplicationDbContext>();

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
            configuration.GetConnectionString("DefaultConnection")));

            //services.AddDatabaseDeveloperPageExceptionFilter();

            services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddEntityFrameworkStores<ApplicationDbContext>();

            services.AddIdentityServer()
                .AddApiAuthorization<ApplicationUser, ApplicationDbContext>();
        }
        private static void AddMapper(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(DataAccessEntryPoint).Assembly);
        }
        private static void AddMediatRExtension(this IServiceCollection services)
        {
            services.AddMediatR(typeof(DataAccessEntryPoint).Assembly);
        }
    }
}

using _4Create.Application.Persistance;
using _4Create.Infrastructure.Persistence;
using _4Create.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace _4Create.Infrastructure
{
	public static class Bootstraper
	{
		public static IServiceCollection AddInfrastructureLayer(this IServiceCollection services, IConfiguration configuration)
		{
			var connectionString = configuration.GetConnectionString("DefaultConnection");
			services.AddDbContext<MySqlDbContext>(options => options.UseMySQL(connectionString));

			services.AddScoped<IMySqlDbContext>(provider => provider.GetService<MySqlDbContext>());

			services.AddTransient<IDataTimeService, DataTimeService>();
			services.AddTransient<ICompanyService, CompanyService>();
			services.AddTransient<IEmployeeService, EmployeeService>();
			services.AddTransient<ISystemLogService, SystemLogService>();

			return services;
		}
	}
}

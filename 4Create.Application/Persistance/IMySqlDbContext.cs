using _4Create.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace _4Create.Application.Persistance
{
	public interface IMySqlDbContext
	{
		DbSet<Employee> Employees { get; set; }
		DbSet<Company> Companies { get; set; }
		DbSet<Title> Titles { get; set; }
		DbSet<EmployeeCompanyValue> EmployeeCompanyValues { get; set; }
		DbSet<SystemLog> SystemLogs { get; set; }

		Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default);
	}
}

using _4Create.Application.Interfaces;
using _4Create.Application.Persistance;
using _4Create.Domain.Models;
using _4Create.Infrastructure.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Security.AccessControl;
namespace _4Create.Infrastructure.Persistence
{
	public class MySqlDbContext : DbContext, IMySqlDbContext
	{
		private readonly ICurrentUserService _currentUserService;
		private readonly IDataTimeService _timeService;
		private readonly ISystemLogService _systemLogService;
		public MySqlDbContext(
			IDataTimeService timeService,
			ICurrentUserService currentUserService,
			DbContextOptions<MySqlDbContext> options,
			ISystemLogService systemLogService) : base(options)
		{
			_timeService = timeService;
			_currentUserService = currentUserService;
			_systemLogService = systemLogService;
		}

		public DbSet<Employee> Employees { get; set; }
		public DbSet<Company> Companies { get; set; }
		public DbSet<Title> Titles { get; set; }

		public DbSet<EmployeeCompanyValue> EmployeeCompanyValues { get; set; }
		public DbSet<SystemLog> SystemLogs { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.ApplyConfiguration(new TitleConfiguration());

			modelBuilder.Entity<EmployeeCompanyValue>()
				.HasKey(ec => new { ec.EmployeeId, ec.CompanyId });

			modelBuilder.Entity<Employee>()
						.HasMany(e => e.EmployeeCompanyValues)
						.WithOne(ec => ec.Employee)
						.HasForeignKey(ec => ec.EmployeeId);

			modelBuilder.Entity<Company>()
				.HasMany(c => c.EmployeeCompanyValues)
				.WithOne(ec => ec.Company)
				.HasForeignKey(ec => ec.CompanyId);

			base.OnModelCreating(modelBuilder);
		}

		public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
		{
			foreach (var entry in ChangeTracker.Entries<BaseEntity>().Where(x => x.State == EntityState.Added || x.State == EntityState.Modified).ToList()) 
			{
				switch(entry.State)
				{
					case EntityState.Added:
						entry.Entity.CreatedAt = _timeService.Now;
						entry.Entity.CreatedBy = _currentUserService.UserEmail;
						List<SystemLog> systemLogs = _systemLogService.LogEventInfoAsync(entry, EventTypeEnum.Create);
						if(systemLogs.Any())
							SystemLogs.AddRange(systemLogs);
						break;

					case EntityState.Modified:
						entry.Entity.UpdatedAt = _timeService.Now;
						entry.Entity.UpdatedBy = _currentUserService.UserEmail;
						systemLogs = _systemLogService.LogEventInfoAsync(entry, EventTypeEnum.Update);
						if (systemLogs.Any())
							SystemLogs.AddRange(systemLogs);
						break;
				}
			}
			return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
		}

	}
}

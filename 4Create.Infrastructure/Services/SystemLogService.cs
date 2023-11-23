using _4Create.Application.Interfaces;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _4Create.Infrastructure.Services
{
	public class SystemLogService : ISystemLogService
	{
		public List<SystemLog> LogEventInfoAsync(EntityEntry<BaseEntity>? entry, EventTypeEnum eventType)
		{
			var entityName = entry.Entity.GetType().Name;
			var entityId = entry.Property("Id").CurrentValue;
			List<string> attributes = new();
			List<SystemLog> systemLogs = new();

			foreach (var property in entry.OriginalValues.Properties)
			{
				var original = entry.OriginalValues[property];
				var current = entry.CurrentValues[property];

				if (eventType == EventTypeEnum.Create && current is not null)
					attributes.Add($"{property.Name}: {current}");

				else if (!object.Equals(original, current))
					attributes.Add($"{property.Name}: {original} -> {current}");

				else
					break;
			}

			if (attributes.Any())
			{
				var systemLog = new SystemLog
				{
					ResourceType = entityName,
					EntityTypeId = (int)entityId,
					ResourceAttributes = string.Join(',', attributes),
					Event = eventType.GetDescription(),
					Comment = $"The {entityName.ToLower()} with ID {entityId} was {eventType.GetDescription()}d.",
					CreatedAt = DateTime.UtcNow,
					CreatedBy = "sys"
				};
				systemLogs.Add(systemLog);
			}

			return systemLogs;
		}
	}
}

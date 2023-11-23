using _4Create.Domain.Enums;
using _4Create.Domain.Models;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _4Create.Application.Interfaces
{
	public interface ISystemLogService
	{
		List<SystemLog> LogEventInfoAsync(EntityEntry<BaseEntity>? entry, EventTypeEnum eventType);
	}
}

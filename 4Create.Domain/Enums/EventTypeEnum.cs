using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _4Create.Domain.Enums
{
	public enum EventTypeEnum
	{
		[Description("Create")]
		Create,
		[Description("Update")]
		Update,
		[Description("Delete")]
		Delete
	}
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _4Create.Domain.Enums
{
	public enum TitleTypeEnum
	{
		[Description("Developer")]
		Developer = 1,
		[Description("Manager")]
		Manager = 2,
		[Description("Tester")]
		Tester = 3
	}
}

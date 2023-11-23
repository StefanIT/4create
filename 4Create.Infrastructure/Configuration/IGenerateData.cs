using _4Create.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _4Create.Infrastructure.Configuration
{
	public interface IGenerateData<T> where T : class
	{
		IEnumerable<T> Generate();
	}
}

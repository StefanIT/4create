using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace _4Create.Application.Models
{
	public class ErrorResponse
	{
        public HttpStatusCode Code { get; set; }
		public IEnumerable<ErrorModel> Errors { get; set; } = new List<ErrorModel>();
    }

	public class ErrorModel
	{
        public string Name { get; set; }
		public string Message { get; set; }
	}
}

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace _4Create.Application.Models
{
	public class ResponseModel
	{
		public static ResponseModel Create<TValue>(TValue result)
		{
			return new ResponseModel<TValue>(HttpStatusCode.OK, result);
		}
	}
	public class ResponseModel<TValue> : ResponseModel
	{
		public ResponseModel(HttpStatusCode statusCode, TValue result)
		{
			Status = statusCode;
			Result = result;
		}

		public HttpStatusCode Status { get; }
		public TValue Result { get; }
		public DateTimeOffset ResponseDateTime { get; set; } = DateTimeOffset.UtcNow;
	}
}

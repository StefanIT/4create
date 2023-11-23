using _4Create.Application.Interfaces;
using _4Create.Application.Persistance;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace _4Create.Application
{
	public static class Bootstraper
	{
		public static IServiceCollection AddApplicationLayer(this IServiceCollection services)
		{
			var assembly = Assembly.GetExecutingAssembly();

			services.AddValidatorsFromAssembly(assembly);
			services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(assembly));

			return services;
		}
	}
}

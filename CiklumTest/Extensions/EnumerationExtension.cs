
using System;
using System.Linq;
using CiklumTest.Providers;
using CiklumTest.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace CiklumTest.Extensions
{
	public static class EnumerationExtension
	{
		public static string Description(this Enum value)
		{ 
			var field = value.GetType().GetField(value.ToString());
			var attributes = field.GetCustomAttributes(false);

			dynamic displayAttribute = null;

			if (attributes.Any())
			{
				displayAttribute = attributes.ElementAt(0);
			}
            
			return displayAttribute?.Description ?? "Description Not Found";
		}
	}
}
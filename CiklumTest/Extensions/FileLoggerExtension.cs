using System;
using CiklumTest.Providers;
using CiklumTest.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace CiklumTest.Extensions
{
	public static class FileLoggerExtension
	{
		public static ILoggerFactory AddFile(this ILoggerFactory factory, string filePath)
		{
			factory.AddProvider(new FileLoggerProvider(filePath));
			return factory;
		}
	}
}

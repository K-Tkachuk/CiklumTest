using System;
using System.Collections.Generic;
using System.Text;
using CiklumTest.Helpers;
using Microsoft.Extensions.Logging;

namespace CiklumTest.Providers
{
	public class FileLoggerProvider : ILoggerProvider
	{
		string path;
		public FileLoggerProvider(string _path)
		{
			path = _path;
		}
		public ILogger CreateLogger(string categoryName)
		{
			return new FileLogger(path);
		}

		public void Dispose()
		{
		}
	}
}
